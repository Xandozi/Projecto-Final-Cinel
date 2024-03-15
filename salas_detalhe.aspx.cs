using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class salas_detalhe : System.Web.UI.Page
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
                List<Salas> sala = Salas.Ler_Sala(Convert.ToInt32(Request.QueryString["cod_sala"]));

                if (sala.Count > 0)
                {
                    lbl_cod_sala.Text = sala[0].cod_sala.ToString();
                    lbl_cod_sala.Font.Bold = true;

                    lbl_nome_sala.Text = sala[0].nome_sala.ToString();
                    lbl_nome_sala.Font.Bold = true;

                    lbl_data_criacao.Text = sala[0].data_criacao.ToShortDateString();
                    lbl_data_criacao.Font.Bold = true;

                    lbl_ultimo_update.Text = sala[0].ultimo_update.ToString();
                    lbl_ultimo_update.Font.Bold = true;
                }
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"editar_sala.aspx?cod_sala={Convert.ToInt32(Request.QueryString["cod_sala"])}");
        }

        protected void btn_confirm_delete_Click(object sender, EventArgs e)
        {
            if (Salas.Delete_Sala(Convert.ToInt32(Request.QueryString["cod_sala"])))
            {
                lbl_mensagem.Text = "Sala apagada com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem.Text = "Sala está inserida num/em horário/os logo não pode ser apagada.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(2000).fadeOut('slow', function() { window.location.href = 'modulos.aspx'; });", true);
        }
    }
}