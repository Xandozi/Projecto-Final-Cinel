using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class horarios_detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);

            if (!Page.IsPostBack)
            {
                modulos.SelectCommand = $"select Modulos.cod_modulo, Modulos.nome_modulo, Modulos.cod_ufcd, Modulos.duracao from Modulos " +
                                        $"join Cursos_Modulos on Cursos_Modulos.cod_modulo = Modulos.cod_modulo " +
                                        $"join Cursos on Cursos.cod_curso = Cursos_Modulos.cod_curso " +
                                        $"join Turmas on Turmas.cod_curso = Cursos.cod_curso " +
                                        $"where Turmas.cod_turma = {cod_turma}";

                ddl_modulo.DataBind();
            }

            List<Formadores> formador = Formadores.Check_Formador_Modulo(cod_turma, Convert.ToInt32(ddl_modulo.SelectedValue));
            lbl_nome_formador.Text = formador[0].nome_completo;
            hf_cod_formador.Value = formador[0].cod_formador.ToString();

            //if (Session["logged"] != "yes")
            //{
            //    Response.Redirect("login.aspx");
            //}
            //else if (!Validation.Check_IsStaff(Session["username"].ToString()))
            //{
            //    Response.Redirect("personal_zone.aspx");
            //}
            //else
            //{
            //List<Horarios> horario_turma = Horarios.Ler_Horario_Turma(Convert.ToInt32(Request.QueryString["cod_turma"]));

            //if (horario_turma.Count > 0)
            //{
            //    lbl_cod_ufcd.Text = horario_turma[0].cod_ufcd.ToString();
            //    lbl_cod_ufcd.Font.Bold = true;

            //    lbl_nome_modulo.Text = horario_turma[0].nome_modulo.ToString();
            //    lbl_nome_modulo.Font.Bold = true;

            //    lbl_duracao.Text = horario_turma[0].duracao.ToString();
            //    lbl_duracao.Font.Bold = true;

            //    lbl_data_criacao.Text = horario_turma[0].data_criacao.ToShortDateString();
            //    lbl_data_criacao.Font.Bold = true;

            //    lbl_ultimo_update.Text = horario_turma[0].ultimo_update.ToString();
            //    lbl_ultimo_update.Font.Bold = true;

            //    if (modulo[0].ativo)
            //    {
            //        lbl_estado.Text = "Ativo";
            //        lbl_estado.ForeColor = System.Drawing.Color.Green;
            //        lbl_estado.Font.Bold = true;
            //    }
            //    else if (!modulo[0].ativo)
            //    {
            //        lbl_estado.Text = "Inativo";
            //        lbl_estado.ForeColor = System.Drawing.Color.Red;
            //        lbl_estado.Font.Bold = true;
            //    }
            //}
            //    }
        }
    }
}