using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class formadores : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    Turmas.Check_Estado_Turmas();

                    ddl_curso.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_regime.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_regime.Items.Insert(0, new ListItem("Todos", "0"));
                    ddl_estado_inscricao.Items.Insert(0, new ListItem("Todos", "0"));

                    ddl_curso.SelectedIndex = 0;
                    ddl_regime.SelectedIndex = 0;
                    ddl_regime.SelectedIndex = 0;
                    ddl_estado_inscricao.SelectedIndex = 0;
                }

                BindData();
            }
        }

        private void BindData()
        {
            string nome_turma = tb_nome_turma.Text;
            string nome_formando = tb_nome_formador.Text;
            int cod_curso = Convert.ToInt32(ddl_curso.SelectedValue);
            int cod_regime = Convert.ToInt32(ddl_regime.SelectedValue);
            string ordenacao_nome_turma = ddl_ordem_nome_turma.SelectedValue;
            string ordenacao_nome_formando = ddl_ordem_nome_formador.SelectedValue;
            int cod_inscricao_situacao = Convert.ToInt32(ddl_estado_inscricao.SelectedValue);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Formadores.Ler_Formadores(nome_turma, nome_formando, cod_curso, cod_regime, ordenacao_nome_turma, ordenacao_nome_formando, cod_inscricao_situacao);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 9;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_formadores.DataSource = pagedData;
            rpt_formadores.DataBind();

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
    }
}