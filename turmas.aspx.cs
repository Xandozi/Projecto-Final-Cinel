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
            //if (Session["logged"] != "yes")
            //{
            //    Response.Redirect("login.aspx");
            //}
            //else if (!Validation.Check_IsStaff(Session["username"].ToString()))
            //{
            //    Response.Redirect("personal_zone.aspx");
            //}
            //else
            //{
            ddl_curso.Items.Insert(0, "Todos");
            ddl_regime.Items.Insert(0, "Todos");
            ddl_estado.Items.Insert(0, "Todos");
                BindData();
            //}
        }

        protected void btn_previous_turmas_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            BindData();
        }

        protected void btn_next_turmas_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            BindData();
        }

        private void BindData()
        {
            //string sort_username = "";
            //string email = tb_email.Text;
            //string username = tb_username.Text;
            //int perfil = 0;
            //int cod_user = 0;
            //int estado = Convert.ToInt32(ddl_estado.SelectedValue);

            //if (ddl_perfil.SelectedIndex != 0)
            //    perfil = Convert.ToInt32(ddl_perfil.SelectedValue);

            //if (tb_cod_user.Text != "")
            //    cod_user = Convert.ToInt32(tb_cod_user.Text);

            //if (ddl_sort_username.SelectedIndex != 0)
            //    sort_username = ddl_sort_username.SelectedValue;

            //DateTime data_inicio = DateTime.MinValue;
            //DateTime data_fim = DateTime.Today;

            //if (DateTime.TryParse(tb_data_inicio.Text, out DateTime inicio))
            //    data_inicio = inicio;

            //if (DateTime.TryParse(tb_data_fim.Text, out DateTime fim))
            //    data_fim = fim;

            //string inicio_formatado = data_inicio.ToString("yyyy-MM-dd");
            //string fim_formatado = data_fim.ToString("yyyy-MM-dd");

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Turmas.Ler_TurmasAll(); ;
            pagedData.AllowPaging = true;
            pagedData.PageSize = 24;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_turmas.DataSource = pagedData;
            rpt_turmas.DataBind();

            btn_previous_turmas.Enabled = !pagedData.IsFirstPage;
            btn_previous_turmas_top.Enabled = !pagedData.IsFirstPage;
            btn_next_turmas.Enabled = !pagedData.IsLastPage;
            btn_next_turmas_top.Enabled = !pagedData.IsLastPage;
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

        protected void btn_aplicar_filtros_Click(object sender, EventArgs e)
        {

        }
    }
}