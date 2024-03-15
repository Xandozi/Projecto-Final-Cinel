using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class criar_sala : System.Web.UI.Page
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
        }

        protected void btn_criar_Click(object sender, EventArgs e)
        {
            if (Salas.Inserir_Sala(tb_designacao.Text, DateTime.Today))
            {
                lbl_mensagem.Text = "Sala criada com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";
                tb_designacao.Text = "";
            }
            else
            {
                lbl_mensagem.Text = "Sala já existe!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
        }
    }
}