using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class criar_modulo : System.Web.UI.Page
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
        }

        protected void btn_criar_Click(object sender, EventArgs e)
        {
            if (Modulos.Inserir_Modulo(Convert.ToInt32(tb_cod_ufcd.Text), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), DateTime.Today) == 1)
            {
                lbl_mensagem.Text = "Módulo criado com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";
                tb_cod_ufcd.Text = "";
                tb_designacao.Text = "";
                tb_duracao.Text = "";
            }
            else
            {
                lbl_mensagem.Text = "Módulo já existe!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
        }
    }
}