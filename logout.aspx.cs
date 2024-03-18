using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.Remove("logged");
            Session.RemoveAll();
            Session["logged"] = "logout";
            if (Request.QueryString["msg"] == "yesemail")
            {
                Response.Redirect("login.aspx?msg=yesemail");
            }
            else if (Request.QueryString["msg"] == "erroemail")
            {
                Response.Redirect("login.aspx?msg=erroemail");
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}