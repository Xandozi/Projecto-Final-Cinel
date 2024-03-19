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
            Response.Redirect("logout.aspx", false);
        }

        protected void btn_previous_formadores_Click(object sender, EventArgs e)
        {

        }

        protected void btn_next_formadores_Click(object sender, EventArgs e)
        {

        }

        protected void btn_previous_formandos_Click(object sender, EventArgs e)
        {

        }

        protected void btn_next_formandos_Click(object sender, EventArgs e)
        {

        }

        protected void lb_avaliacoes_Click(object sender, EventArgs e)
        {

        }

        protected void lb_horario_Click(object sender, EventArgs e)
        {

        }

        protected void lb_notas_Click(object sender, EventArgs e)
        {

        }

        protected void rpt_formandos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rpt_formadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}