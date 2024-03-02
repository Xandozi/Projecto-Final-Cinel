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
            // Variáveis de sessão limpas ao clicar em logout
            Session.Clear();
            Session.Abandon();
            // Redirecionamento caso provenha de depois de mudar o email
            if (Request.QueryString["msg"] == "yesemail")
            {
                Response.Redirect("login.aspx?msg=yesemail");
            }
            Response.Redirect("login.aspx");
        }
    }
}