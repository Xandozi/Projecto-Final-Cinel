using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class turmas_cancelar_formandos : System.Web.UI.Page
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

                    int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);
                    List<Turmas> lst_turma = Turmas.Ler_Turma(cod_turma);

                    lbl_nome_turma.Text = lst_turma[0].nome_turma;
                    lbl_nome_turma.Font.Bold = true;

                    cod_turma_hidden.Value = cod_turma.ToString();

                    List<Formandos> lst_formandos = Formandos.Ler_Formandos_Turma(lst_turma[0].cod_turma);

                    lstb_formandos_turma.DataSource = lst_formandos;
                    lstb_formandos_turma.DataTextField = "nome_completo";
                    lstb_formandos_turma.DataValueField = "cod_formando";
                    lstb_formandos_turma.DataBind();
                }
            }
        }

        protected void btn_add_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_turma.SelectedIndex != -1)
            {
                Formandos formando_adicionado = new Formandos
                {
                    nome_completo = lstb_formandos_turma.SelectedItem.ToString(),
                    cod_formando = Convert.ToInt32(lstb_formandos_turma.SelectedValue)
                };

                ListItem index_formando_removido = lstb_formandos_turma.SelectedItem;
                lstb_formandos_turma.Items.Remove(index_formando_removido);
                lstb_formandos_desistentes.Items.Add(new ListItem(formando_adicionado.nome_completo, formando_adicionado.cod_formando.ToString()));

                lbl_mensagem_formandos.Text = "Formando adicionado como desistente com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels4", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder adicionar.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels5", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }

        protected void btn_remove_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_desistentes.SelectedIndex != -1)
            {
                Formandos formando_removido = new Formandos
                {
                    nome_completo = lstb_formandos_turma.SelectedItem.ToString(),
                    cod_formando = Convert.ToInt32(lstb_formandos_turma.SelectedValue)
                };

                ListItem index_formando_removido = lstb_formandos_turma.SelectedItem;
                lstb_formandos_desistentes.Items.Remove(index_formando_removido);
                lstb_formandos_turma.Items.Add(new ListItem(formando_removido.nome_completo, formando_removido.cod_formando.ToString()));

                lbl_mensagem_formandos.Text = "Formando removido como desistente com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels6", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder remover.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels7", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }

        protected void btn_salvar_alteracoes_Click(object sender, EventArgs e)
        {
            List<Formandos> lst_formandos = new List<Formandos>();

            foreach (ListItem item in lstb_formandos_desistentes.Items)
            {
                int cod_inscricao = Extract.Cod_Inscricao_Formando(Convert.ToInt32(item.Value));

                Formandos formando = new Formandos
                {
                    nome_completo = item.Text.Trim(),
                    cod_formando = Convert.ToInt32(item.Value),
                    cod_inscricao = cod_inscricao
                };

                lst_formandos.Add(formando);
            }

            foreach (Formandos formando in lst_formandos)
                Formandos.Alterar_Estado_Formando(formando.cod_formando);

            lstb_formandos_desistentes.Items.Clear();
            lstb_formandos_turma.Items.Clear();
            lbl_mensagem_formandos.Text = "";
            lbl_mensagem_formandos.CssClass = "";

            lbl_mensagem.Text = "Estado dos formandos foram alterados com sucesso! Será redirecionado para a página da Turma novamente.";
            lbl_mensagem.CssClass = "alert alert-success";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeoutRedirect", "setTimeout(function() { $('#" + lbl_mensagem.ClientID + "').fadeOut('slow', function() { window.location.href = 'turmas_detalhe.aspx?cod_turma=" + Request.QueryString["cod_turma"] + "'; }); }, 5000);", true);
        }
    }
}