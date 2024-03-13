using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class editar_sala : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    List<Salas> sala = Salas.Ler_Sala(Convert.ToInt32(Request.QueryString["cod_sala"]));

                    if (sala.Count > 0)
                    {
                        lbl_cod_sala.Text = sala[0].cod_sala.ToString();
                        lbl_cod_sala.Font.Bold = true;

                        tb_designacao.Text = sala[0].nome_sala.ToString();
                        tb_designacao.Font.Bold = true;

                        lbl_data_criacao.Text = sala[0].data_criacao.ToShortDateString();
                        lbl_data_criacao.Font.Bold = true;

                        lbl_ultimo_update.Text = sala[0].ultimo_update.ToString();
                        lbl_ultimo_update.Font.Bold = true;
                    }

                }
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            if (Salas.Editar_Sala(Convert.ToInt32(Request.QueryString["cod_sala"]), tb_designacao.Text, DateTime.Now))
            {
                Response.Redirect($"salas_detalhe.aspx?cod_sala={Convert.ToInt32(Request.QueryString["cod_sala"])}");
            }
            else
            {
                lbl_mensagem.Text = "Nome da sala já existe!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
        }
    }
}