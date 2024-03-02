using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class Cinel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string DetermineLoginButtonText()
        {
            if (Request.Url.AbsolutePath.ToLower() == "/login.aspx" && Session["logged"] != "yes")
            {
                return "Registar";
            }
            else if (Request.Url.AbsolutePath.ToLower() == "/register.aspx" && Session["logged"] != "yes")
            {
                return "Login";
            }
            else if (Session["logged"] != null)
            {
                return $"{Session["username"]}";
            }
            else
            {
                return "Login";
            }
        }

        // Função para determinar se aparece para onde irá ser feito o redirect ao carregar no botão
        protected string DetermineLoginRedirect()
        {
            if (Request.Url.AbsolutePath.ToLower() == "/login.aspx" && Session["logged"] != "yes")
            {
                return "register.aspx";
            }
            else if (Request.Url.AbsolutePath.ToLower() == "/register.aspx" && Session["logged"] != "yes")
            {
                return "login.aspx";
            }
            else if (Session["logged"] != null)
            {
                return "personal_zone.aspx";
            }
            else
            {
                return "login.aspx";
            }
        }

        protected void btn_user_Click(object sender, EventArgs e)
        {
            Response.Redirect(DetermineLoginRedirect());
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {

        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}