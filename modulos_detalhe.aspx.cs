using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class modulos_detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (!Validation.Check_IsSuperAdmin(Session["username"].ToString()))
            {
                if (!Validation.Check_IsStaff(Session["username"].ToString()))
                {
                    Response.Redirect("personal_zone.aspx");
                }
            }
            else
            {
                List<Modulos> modulo = Modulos.Ler_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"]));

                if (modulo.Count > 0)
                {
                    lbl_cod_ufcd.Text = modulo[0].cod_ufcd.ToString();
                    lbl_cod_ufcd.Font.Bold = true;

                    lbl_nome_modulo.Text = modulo[0].nome_modulo.ToString();
                    lbl_nome_modulo.Font.Bold = true;

                    lbl_duracao.Text = modulo[0].duracao.ToString();
                    lbl_duracao.Font.Bold = true;

                    lbl_data_criacao.Text = modulo[0].data_criacao.ToShortDateString();
                    lbl_data_criacao.Font.Bold = true;

                    lbl_ultimo_update.Text = modulo[0].ultimo_update.ToString();
                    lbl_ultimo_update.Font.Bold = true;
                }
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"editar_modulo.aspx?cod_modulo={Convert.ToInt32(Request.QueryString["cod_modulo"])}");
        }

        protected void btn_apagar_Click(object sender, EventArgs e)
        {
            if (Modulos.Delete_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"])))
            {
                lbl_mensagem.Text = "Módulo apagado com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem.Text = "Módulo está inserido num CET TPSI, não pode ser apagado enquanto não for removido do mesmo.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow', function() { window.location.href = 'modulos.aspx'; });", true);
        }
    }
}