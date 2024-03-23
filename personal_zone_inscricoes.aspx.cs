using Projeto_Final.Classes;
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
            else
            {
                if (Session["perfil"].ToString().Contains("Formador"))
                {
                    card_formadores.Visible = true;
                }
                else
                {
                    card_formadores.Visible = false;
                }

                if (Session["perfil"].ToString().Contains("Formando"))
                {
                    card_formandos.Visible = true;
                }
                else
                {
                    card_formandos.Visible = false;
                }
                BindData_Formandos();
                BindData_Formadores();
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
            int cod_inscricao = 0;
            int cod_turma = 0;

            Response.Redirect($"avaliacoes.aspx?cod_inscricao={cod_inscricao}&cod_turma={cod_turma}");
        }

        protected void lb_horario_Click(object sender, EventArgs e)
        {
            int cod_turma = 0;

            Response.Redirect($"horarios.aspx?cod_turma={cod_turma}");
        }

        protected void lb_notas_Click(object sender, EventArgs e)
        {
            int cod_inscricao = 0;
            int cod_turma = 0;

            Response.Redirect($"notas.aspx?cod_inscricao={cod_inscricao}&cod_turma={cod_turma}");
        }

        protected void rpt_formandos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rpt_formadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        private void BindData_Formadores()
        {
            int cod_user = Convert.ToInt32(Session["cod_user"]);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Formadores.Ler_Inscricoes_Formador(cod_user);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 10;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_formadores.DataSource = pagedData;
            rpt_formadores.DataBind();

            btn_previous_formadores.Enabled = !pagedData.IsFirstPage;
            btn_previous_formadores_top.Enabled = !pagedData.IsFirstPage;
            btn_next_formadores.Enabled = !pagedData.IsLastPage;
            btn_next_formadores_top.Enabled = !pagedData.IsLastPage;
        }

        private void BindData_Formandos()
        {
            int cod_user = Convert.ToInt32(Session["cod_user"]);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Formandos.Ler_Inscricoes_Formando(cod_user);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 10;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_formandos.DataSource = pagedData;
            rpt_formandos.DataBind();

            btn_previous_formandos.Enabled = !pagedData.IsFirstPage;
            btn_previous_formandos_top.Enabled = !pagedData.IsFirstPage;
            btn_next_formandos.Enabled = !pagedData.IsLastPage;
            btn_next_formandos_top.Enabled = !pagedData.IsLastPage;
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }
    }
}