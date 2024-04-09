using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class horarios_detalhe : System.Web.UI.Page
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
                string nome_turma = Request.QueryString["nome_turma"];

                lbl_nome_turma.Text = nome_turma;
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);
            string nome_turma = Request.QueryString["nome_turma"];
            string regime = Request.QueryString["regime"];

            Response.Redirect($"editar_horario.aspx?cod_turma={cod_turma}&nome_turma={nome_turma}&regime={regime}");
        }
    }
}