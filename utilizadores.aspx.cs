using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class utilizadores : System.Web.UI.Page
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
                    ddl_perfil.Items.Insert(0, "Todos");
                }
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
            string sort_username = "";
            string email = tb_email.Text;
            string username = tb_username.Text;
            int perfil = 0;
            int cod_user = 0;
            int estado = Convert.ToInt32(ddl_estado.SelectedValue);

            if (ddl_perfil.SelectedIndex != 0)
                perfil = Convert.ToInt32(ddl_perfil.SelectedValue);

            if (tb_cod_user.Text != "")
                cod_user = Convert.ToInt32(tb_cod_user.Text);

            if (ddl_sort_username.SelectedIndex != 0)
                sort_username = ddl_sort_username.SelectedValue;

            DateTime data_inicio = DateTime.MinValue;
            DateTime data_fim = DateTime.Today;

            if (DateTime.TryParse(tb_data_inicio.Text, out DateTime inicio))
                data_inicio = inicio;

            if (DateTime.TryParse(tb_data_fim.Text, out DateTime fim))
                data_fim = fim;

            string inicio_formatado = data_inicio.ToString("yyyy-MM-dd");
            string fim_formatado = data_fim.ToString("yyyy-MM-dd");

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Users.Ler_UsersAll(username, perfil, email, inicio_formatado, fim_formatado, cod_user, sort_username, estado);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 24;
            pagedData.CurrentPageIndex = PageNumber;

            rpt_users.DataSource = pagedData;
            rpt_users.DataBind();

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

            int index = ddl_perfil.SelectedIndex;

            ddl_perfil.Items.Clear();
            ddl_perfil.Items.Insert(0, "Todos");
            ddl_perfil.DataBind();
            perfis.DataBind();
            ddl_perfil.SelectedIndex = index;
        }
    }
}