using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class personal_zone_inscricoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btn_logout2_Click(object sender, EventArgs e)
        {

        }

        protected void btn_change_pw_Click(object sender, EventArgs e)
        {

        }

        protected void btn_change_username_Click(object sender, EventArgs e)
        {

        }

        protected void btn_change_email_Click(object sender, EventArgs e)
        {

        }
    }
}