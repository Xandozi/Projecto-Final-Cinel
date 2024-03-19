using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class turmas : System.Web.UI.Page
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

            }
        }

        protected void btn_previous_turmas_Click(object sender, EventArgs e)
        {

        }

        protected void btn_next_turmas_Click(object sender, EventArgs e)
        {

        }

        protected void rpt_turmas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}