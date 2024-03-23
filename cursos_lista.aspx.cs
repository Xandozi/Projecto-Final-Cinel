using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class cursos_lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddl_area.Items.Insert(0, new ListItem("Todas", "0"));
            }

            BindData();
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
            int cod_area = Convert.ToInt32(ddl_area.SelectedValue);
            string sort_cod_qualificacao = "";
            int cod_qualificacao;
            string duracao = ddl_duracao.SelectedValue;

            if (ddl_cod_ufcd.SelectedIndex == 0)
                sort_cod_qualificacao = "";
            else if (ddl_cod_ufcd.SelectedIndex != 0)
                sort_cod_qualificacao = ddl_cod_ufcd.SelectedValue;

            DateTime data_inicio = DateTime.MinValue;
            DateTime data_fim = DateTime.Today;

            if (DateTime.TryParse(tb_data_inicio.Text, out DateTime inicio))
                data_inicio = inicio;

            if (DateTime.TryParse(tb_data_fim.Text, out DateTime fim))
                data_fim = fim;

            if (tb_cod_qualificacao.Text == "")
                cod_qualificacao = 0;
            else
                cod_qualificacao = Convert.ToInt32(tb_cod_qualificacao.Text);

            string inicio_formatado = data_inicio.ToString("yyyy-MM-dd");
            string fim_formatado = data_fim.ToString("yyyy-MM-dd");

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Cursos.Ler_CursosAll(cod_area, tb_designacao.Text, duracao, inicio_formatado, fim_formatado, cod_qualificacao, sort_cod_qualificacao, 1);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 24;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_cursos.DataSource = pagedData;
            rpt_cursos.DataBind();

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

        protected void btn_aplicar_filtros_Click(object sender, EventArgs e)
        {
            filterForm.Style["display"] = "block";
        }
    }
}