using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class dados_estatisticos : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    Estatistica estatistica = new Estatistica();

                    // Set total completed courses
                    lblTotalCompletedCourses.Text = estatistica.TotalCursosTerminados().ToString();

                    // Set total ongoing courses
                    lblTotalOngoingCourses.Text = estatistica.TotalCursosDecorrer().ToString();

                    // Set total trainees in courses
                    lblTotalTrainees.Text = estatistica.TotalFormandosEmTurmas().ToString();

                    // Set top 10 trainers
                    rptTopTrainers.DataSource = Estatistica.GetTop10Formadores();
                    rptTopTrainers.DataBind();

                    rpt_cursos_area.DataSource = Estatistica.TotalCursosPorArea();
                    rpt_cursos_area.DataBind();
                }
            }
        }
    }
}