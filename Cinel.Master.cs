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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (!Page.IsPostBack)
            {
                this.DataBind();
            }
        }
        protected string DetermineColumnClass()
        {
            if (Session["logged"] == "yes")
            {
                return "col-lg-12";
            }
            else
            {
                return "col-lg-9";
            }
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
                return $"<i class='fas fa-user'></i> {Session["username"]}";
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
            Response.Redirect("logout.aspx", false);
        }

        protected string IsActivePage(string pageName)
        {
            // Get the current page URL
            string currentPage = Request.Url.AbsolutePath.ToLower();

            // Check if the current page URL matches the provided URL
            if (currentPage.EndsWith(pageName.ToLower()))
            {
                // Return "active" if the current page matches the provided URL
                return "active";
            }
            else
            {
                // Return an empty string if the current page doesn't match the provided URL
                return "";
            }
        }
    }
}