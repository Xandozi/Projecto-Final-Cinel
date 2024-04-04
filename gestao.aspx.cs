using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class gestao : System.Web.UI.Page
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
                BindData_Formadores();
                BindData_Formandos();

                if (!Page.IsPostBack)
                {
                    bool bool_rpt_formadores = Check_Repeaters(rpt_formadores);
                    bool bool_rpt_formandos = Check_Repeaters(rpt_formandos);

                    card_formadores.Visible = bool_rpt_formadores;
                    card_formandos.Visible = bool_rpt_formandos;
                }
            }
        }

        protected bool Check_Repeaters(Repeater repeater)
        {
            return repeater.Items.Count > 0;
        }

        private void BindData_Formadores()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Inscricoes.GetFormadores_Por_Validar();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 12;
            pagedData.CurrentPageIndex = PageNumber_Formadores;

            rpt_formadores.DataSource = pagedData;
            rpt_formadores.DataBind();

            btn_previous_formadores.Enabled = !pagedData.IsFirstPage;
            btn_previous_formadores_top.Enabled = !pagedData.IsFirstPage;
            btn_next_formadores.Enabled = !pagedData.IsLastPage;
            btn_next_formadores_top.Enabled = !pagedData.IsLastPage;
        }

        private void BindData_Formandos()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Inscricoes.GetFormandos_Por_Validar();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 12;
            pagedData.CurrentPageIndex = PageNumber_Formandos;

            rpt_formandos.DataSource = pagedData;
            rpt_formandos.DataBind();

            btn_previous_formandos.Enabled = !pagedData.IsFirstPage;
            btn_previous_formandos_top.Enabled = !pagedData.IsFirstPage;
            btn_next_formandos.Enabled = !pagedData.IsLastPage;
            btn_next_formandos_top.Enabled = !pagedData.IsLastPage;
        }

        public int PageNumber_Formandos
        {
            get
            {
                if (ViewState["PageNumber_Formandos"] != null)
                    return Convert.ToInt32(ViewState["PageNumber_Formandos"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber_Formandos"] = value;
            }
        }

        public int PageNumber_Formadores
        {
            get
            {
                if (ViewState["PageNumber_Formadores"] != null)
                    return Convert.ToInt32(ViewState["PageNumber_Formadores"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber_Formadores"] = value;
            }
        }

        protected void btn_previous_formadores_Click(object sender, EventArgs e)
        {
            PageNumber_Formadores -= 1;
            BindData_Formadores();
        }

        protected void btn_next_formadores_Click(object sender, EventArgs e)
        {
            PageNumber_Formadores += 1;
            BindData_Formadores();
        }

        protected void btn_previous_formandos_Click(object sender, EventArgs e)
        {
            PageNumber_Formandos -= 1;
            BindData_Formandos();
        }

        protected void btn_next_formandos_Click(object sender, EventArgs e)
        {
            PageNumber_Formandos += 1;
            BindData_Formandos();
        }

        protected void rpt_formandos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Projeto_Final.Classes.Inscricoes formando = (Projeto_Final.Classes.Inscricoes)e.Item.DataItem;
                string cod_user = formando.cod_user.ToString() + "-" + formando.cod_inscricao.ToString() + "-6";

                LinkButton lb_validar_formandos = (LinkButton)e.Item.FindControl("lb_validar_formandos");
                lb_validar_formandos.CommandArgument = cod_user;

                LinkButton lb_revogar_formandos = (LinkButton)e.Item.FindControl("lb_revogar_formandos");
                lb_revogar_formandos.CommandArgument = cod_user;
            }
        }

        protected void rpt_formadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Projeto_Final.Classes.Inscricoes formador = (Projeto_Final.Classes.Inscricoes)e.Item.DataItem;
                string cod_user_inscricao = formador.cod_user.ToString() + "-" + formador.cod_inscricao.ToString() + "-7";

                LinkButton lb_validar_formadores = (LinkButton)e.Item.FindControl("lb_validar_formadores");
                lb_validar_formadores.CommandArgument = cod_user_inscricao;

                LinkButton lb_revogar_formadores = (LinkButton)e.Item.FindControl("lb_revogar_formadores");
                lb_revogar_formadores.CommandArgument = cod_user_inscricao;
            }
        }

        protected void lb_validar_formadores_Click(object sender, EventArgs e)
        {
            LinkButton lb_validar_formadores = (LinkButton)sender;
            string cod_user_inscricao = lb_validar_formadores.CommandArgument;

            string[] partes = cod_user_inscricao.Split('-');

            string cod_user = partes[0];
            string cod_inscricao = partes[1];
            string cod_situacao = partes[2];

            if (Inscricoes.Validar_Inscricao(Convert.ToInt32(cod_user), Convert.ToInt32(cod_inscricao), Convert.ToInt32(cod_situacao)))
            {
                lbl_mensagem_formadores.Text = "Formador validado com sucesso.";
                lbl_mensagem_formadores.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem_formadores.Text = "Erro ao validar o formador.";
                lbl_mensagem_formadores.CssClass = "alert alert-danger";
            }

            BindData_Formadores();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem_formadores.ClientID + "').delay(5000).fadeOut('slow');", true);
        }

        protected void lb_revogar_formadores_Click(object sender, EventArgs e)
        {
            LinkButton lb_revogar_formandores = (LinkButton)sender;
            string cod_user_inscricao = lb_revogar_formandores.CommandArgument;

            string[] partes = cod_user_inscricao.Split('-');

            string cod_user = partes[0];
            string cod_inscricao = partes[1];

            if (Inscricoes.Revogar_Inscricao(Convert.ToInt32(cod_user), Convert.ToInt32(cod_inscricao)))
            {
                lbl_mensagem_formadores.Text = "Inscrição de formador revogada com sucesso.";
                lbl_mensagem_formadores.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem_formadores.Text = "Erro ao revogar a inscrição.";
                lbl_mensagem_formadores.CssClass = "alert alert-danger";
            }

            BindData_Formadores();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem_formadores.ClientID + "').delay(5000).fadeOut('slow');", true);
        }

        protected void lb_validar_formandos_Click(object sender, EventArgs e)
        {
            LinkButton lb_validar_formandos = (LinkButton)sender;
            string cod_user_inscricao = lb_validar_formandos.CommandArgument;

            string[] partes = cod_user_inscricao.Split('-');

            string cod_user = partes[0];
            string cod_inscricao = partes[1];
            string cod_situacao = partes[2];

            if (Inscricoes.Validar_Inscricao(Convert.ToInt32(cod_user), Convert.ToInt32(cod_inscricao), Convert.ToInt32(cod_situacao)))
            {
                lbl_mensagem_formandos.Text = "Formando validado com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem_formandos.Text = "Erro ao validar o formando.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";
            }

            BindData_Formandos();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(5000).fadeOut('slow');", true);
        }

        protected void lb_revogar_formandos_Click(object sender, EventArgs e)
        {
            LinkButton lb_revogar_formandos = (LinkButton)sender;
            string cod_user_inscricao = lb_revogar_formandos.CommandArgument;

            string[] partes = cod_user_inscricao.Split('-');

            string cod_user = partes[0];
            string cod_inscricao = partes[1];

            if (Inscricoes.Revogar_Inscricao(Convert.ToInt32(cod_user), Convert.ToInt32(cod_inscricao)))
            {
                lbl_mensagem_formandos.Text = "Inscrição de formando revogada com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem_formandos.Text = "Erro ao revogar a inscrição.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";
            }

            BindData_Formandos();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(5000).fadeOut('slow');", true);
        }
    }
}