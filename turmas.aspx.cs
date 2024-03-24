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
                if (!Page.IsPostBack)
                {
                    ddl_curso.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_regime.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_estado.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_area.Items.Insert(0, new ListItem("Todos", "0"));

                    ddl_curso.SelectedIndex = 0;
                    ddl_regime.SelectedIndex = 0;
                    ddl_estado.SelectedIndex = 0;
                    ddl_area.SelectedIndex = 0;

                    tb_cod_qualificacao.Text = "0";
                }

                BindData();
            //    }
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
            string nome_turma = tb_nome_turma.Text;
            int cod_qualificacao = Convert.ToInt32(tb_cod_qualificacao.Text);
            int cod_curso = Convert.ToInt32(ddl_curso.SelectedValue);
            int cod_regime = Convert.ToInt32(ddl_regime.SelectedValue);
            int cod_area = Convert.ToInt32(ddl_area.SelectedValue);
            string ordenacao_nome_turma = ddl_sort_nome_turma.SelectedValue;
            string ordenacao_cod_qualificacao = ddl_sort_cod_qualificacao.SelectedValue;
            string duracao = ddl_duracao.SelectedValue;
            int cod_turma_estado = Convert.ToInt32(ddl_estado.SelectedValue);

            DateTime inicio_data_inicio = DateTime.MinValue;
            DateTime fim_data_inicio = DateTime.Today;

            DateTime inicio_data_fim = DateTime.MinValue;
            DateTime fim_data_fim = DateTime.Today;

            if (DateTime.TryParse(tb_data_inicio_inicio.Text, out DateTime inicio))
                inicio_data_inicio = inicio;

            if (DateTime.TryParse(tb_data_fim_inicio.Text, out DateTime fim))
                fim_data_inicio = fim;

            if (DateTime.TryParse(tb_data_inicio_fim.Text, out DateTime inicio2))
                inicio_data_fim = inicio2;

            if (DateTime.TryParse(tb_data_fim_inicio.Text, out DateTime fim2))
                fim_data_fim = fim2;

            string inicio_inicio_formatado = inicio_data_inicio.ToString("yyyy-MM-dd");
            string fim_inicio_formatado = fim_data_inicio.ToString("yyyy-MM-dd");

            string inicio_fim_formatado = inicio_data_fim.ToString("yyyy-MM-dd");
            string fim_fim_formatado = fim_data_fim.ToString("yyyy-MM-dd");

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Turmas.Ler_TurmasAll(nome_turma, cod_qualificacao, cod_curso, cod_regime, inicio_inicio_formatado, fim_inicio_formatado, inicio_fim_formatado, fim_fim_formatado, cod_area, ordenacao_nome_turma, ordenacao_cod_qualificacao, duracao, cod_turma_estado);
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
            filterForm.Style["display"] = "block";
        }
    }
}