using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class editar_turma : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    List<Turmas> turma = Turmas.Ler_Turma(Convert.ToInt32(Request.QueryString["cod_turma"]));

                    if (turma.Count > 0)
                    {
                        lbl_nome_turma.Text = turma[0].nome_turma;
                        lbl_nome_turma.Font.Bold = true;

                        lbl_cod_turma.Text = turma[0].cod_turma.ToString();
                        lbl_cod_turma.Font.Bold = true;

                        lbl_nome_curso.Text = turma[0].nome_curso;
                        lbl_nome_curso.Font.Bold = true;

                        ddl_regime.SelectedValue = turma[0].cod_regime.ToString();

                        lbl_duracao.Text = turma[0].duracao_curso.ToString() + " horas";
                        lbl_duracao.Font.Bold = true;

                        lbl_data_inicio.Text = turma[0].data_inicio.ToShortDateString();
                        lbl_data_inicio.Font.Bold = true;

                        lbl_data_fim.Text = turma[0].data_fim.ToShortDateString();
                        lbl_data_fim.Font.Bold = true;

                        ddl_estado.SelectedValue = turma[0].cod_turmas_estado.ToString();
                        ViewState["cod_turmas_estado"] = turma[0].cod_turmas_estado;
                    }

                }
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);
            int cod_regime = Convert.ToInt32(ddl_regime.SelectedValue);
            int cod_estado = Convert.ToInt32(ddl_estado.SelectedValue);

            if ((Convert.ToInt32(ViewState["cod_turmas_estado"]) == 3 || Convert.ToInt32(ViewState["cod_turmas_estado"]) == 4) && (cod_estado == 2 || cod_estado == 1))
            {
                lbl_mensagem.Text = "Não pode voltar a iniciar uma turma após ter terminado.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
            }
            else
            {
                Turmas.Editar_Turma(cod_turma, cod_regime, cod_estado);

                lbl_mensagem.Text = "Turma editada com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
            }
        }
    }
}