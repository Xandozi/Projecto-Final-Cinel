using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class modulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (!Validation.Check_IsSuperAdmin(Session["username"].ToString()))
            {
                if (!Validation.Check_IsStaff(Session["username"].ToString()))
                {
                    Response.Redirect("personal_zone.aspx");
                }
            }
            else
            {
                BindData();
            }
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            BindData();
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            BindData();
        }
        private void BindData()
        {
            string sort_cod_ufcd = "";
            int duracao_modulo;
            int cod_ufcd;

            if (ddl_cod_ufcd.SelectedIndex == 0)
                sort_cod_ufcd = "";
            else if (ddl_cod_ufcd.SelectedIndex != 0)
                sort_cod_ufcd = ddl_cod_ufcd.SelectedValue;

            DateTime data_inicio = DateTime.MinValue;
            DateTime data_fim = DateTime.Today;

            if (DateTime.TryParse(tb_data_inicio.Text, out DateTime inicio))
                data_inicio = inicio;

            if (DateTime.TryParse(tb_data_fim.Text, out DateTime fim))
                data_fim = fim;

            if (tb_duracao.Text == "")
                duracao_modulo = 0;
            else
                duracao_modulo = Convert.ToInt32(tb_duracao.Text);

            if (tb_cod_ufcd.Text == "")
                cod_ufcd = 0;
            else
                cod_ufcd = Convert.ToInt32(tb_cod_ufcd.Text);

            string inicio_formatado = data_inicio.ToString("yyyy-MM-dd");
            string fim_formatado = data_fim.ToString("yyyy-MM-dd");

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Modulos.Ler_ModulosAll(tb_designacao.Text, duracao_modulo, inicio_formatado, fim_formatado, cod_ufcd, sort_cod_ufcd);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 24;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_modulos.DataSource = pagedData;
            rpt_modulos.DataBind();

            btn_previous.Enabled = !pagedData.IsFirstPage;
            btn_previous_top.Enabled = !pagedData.IsFirstPage;
            btn_next.Enabled = !pagedData.IsLastPage;
            btn_next_top.Enabled = !pagedData.IsLastPage;
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