using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class turmas_detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (!Validation.Check_IsStaff(Session["username"].ToString()))
            {
                Response.Redirect("personal_zone.aspx");
            }
            else
            {
                Turmas.Check_Estado_Turmas();

                int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);
                List<Turmas> lst_turma = Turmas.Ler_Turma(cod_turma);
                List<Formadores_Modulos> formadores_modulos = lst_turma[0].formadores_modulos;
                List<Formandos> formandos = lst_turma[0].formandos;

                lbl_nome_turma.Text = lst_turma[0].nome_turma;
                lbl_nome_turma.Font.Bold = true;

                lbl_cod_turma.Text = lst_turma[0].cod_turma.ToString();
                lbl_cod_turma.Font.Bold = true;

                lbl_nome_curso.Text = lst_turma[0].nome_curso;
                lbl_nome_curso.Font.Bold = true;

                lbl_regime.Text = lst_turma[0].regime;
                lbl_regime.Font.Bold = true;

                lbl_duracao.Text = lst_turma[0].duracao_curso.ToString() + " horas";
                lbl_duracao.Font.Bold = true;

                lbl_data_inicio.Text = lst_turma[0].data_inicio.ToShortDateString();
                lbl_data_inicio.Font.Bold = true;

                lbl_data_fim.Text = lst_turma[0].data_fim.ToShortDateString();
                lbl_data_fim.Font.Bold = true;

                lbl_estado.Text = lst_turma[0].estado;
                lbl_estado.Font.Bold = true;

                lbl_formadores_modulos.Font.Bold = true;
                lstb_formadores_modulos.DataSource = formadores_modulos;
                lstb_formadores_modulos.DataTextField = "formador_modulo";
                lstb_formadores_modulos.DataValueField = "cod_formador_modulo";
                lstb_formadores_modulos.DataBind();

                lbl_formandos.Font.Bold = true;
                lstb_formandos.DataSource = formandos;
                lstb_formandos.DataTextField = "nome_completo";
                lstb_formandos.DataValueField = "cod_formando";
                lstb_formandos.DataBind();
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }
    }
}