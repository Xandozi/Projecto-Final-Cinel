using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class turmas_detalhe : System.Web.UI.Page
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
                Turmas.Check_Estado_Turmas();

                int cod_turma = Convert.ToInt32(Request.QueryString["cod_turma"]);
                List<Turmas> lst_turma = Turmas.Ler_Turma(cod_turma);
                List<Formadores_Modulos> formadores_modulos = lst_turma[0].formadores_modulos;
                List<Formandos> formandos = lst_turma[0].formandos;

                lbl_nome_turma.Text = lst_turma[0].nome_turma;
                lbl_nome_turma.Font.Bold = true;

                lbl_cod_turma.Text = lst_turma[0].cod_turma.ToString();
                lbl_cod_turma.Font.Bold = true;

                lbl_nome_curso.Text = lst_turma[0].nome_curso;
                lbl_nome_curso.Font.Bold = true;

                lbl_regime.Text = lst_turma[0].regime;
                lbl_regime.Font.Bold = true;

                lbl_duracao.Text = lst_turma[0].duracao_curso.ToString();
                lbl_duracao.Font.Bold = true;

                lbl_data_inicio.Text = lst_turma[0].data_inicio.ToShortDateString();
                lbl_data_inicio.Font.Bold = true;

                lbl_data_fim.Text = lst_turma[0].data_fim.ToShortDateString();
                lbl_data_fim.Font.Bold = true;

                lbl_estado.Text = lst_turma[0].estado;
                lbl_estado.Font.Bold = true;

                lbl_formadores_modulos.Font.Bold = true;
                lstb_formadores_modulos.DataSource = formadores_modulos;
                lstb_formadores_modulos.DataTextField = "formador_modulo";
                lstb_formadores_modulos.DataValueField = "cod_formador_modulo";
                lstb_formadores_modulos.DataBind();

                lbl_formandos.Font.Bold = true;
                lstb_formandos.DataSource = formandos;
                lstb_formandos.DataTextField = "nome_completo";
                lstb_formandos.DataValueField = "cod_formando";
                lstb_formandos.DataBind();

                List<Formandos> lst_formandos = Formandos.Ler_FormandosAll(lst_turma[0].cod_curso);

                lstb_formandos_legiveis.DataSource = lst_formandos;
                lstb_formandos_legiveis.DataTextField = "nome_completo";
                lstb_formandos_legiveis.DataValueField = "cod_formando";
                lstb_formandos_legiveis.DataBind();


            }
        }

        protected void btn_add_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_legiveis.SelectedIndex != -1)
            {
                Formandos formando_adicionado = new Formandos
                {
                    nome_completo = lstb_formandos_legiveis.SelectedItem.ToString(),
                    cod_formando = Convert.ToInt32(lstb_formandos_legiveis.SelectedValue)
                };

                ListItem index_formando_removido = lstb_formandos_legiveis.SelectedItem;
                lstb_formandos_legiveis.Items.Remove(index_formando_removido);
                lstb_formandos_turma.Items.Add(new ListItem(formando_adicionado.nome_completo, formando_adicionado.cod_formando.ToString()));

                lbl_mensagem_formandos.Text = "Formando adicionado à turma com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels4", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder adicionar.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels5", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }

        protected void btn_remove_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_turma.SelectedIndex != -1)
            {
                Formandos formando_removido = new Formandos
                {
                    nome_completo = lstb_formandos_turma.SelectedItem.ToString(),
                    cod_formando = Convert.ToInt32(lstb_formandos_turma.SelectedValue)
                };

                ListItem index_formando_removido = lstb_formandos_turma.SelectedItem;
                lstb_formandos_turma.Items.Remove(index_formando_removido);
                lstb_formandos_legiveis.Items.Add(new ListItem(formando_removido.nome_completo, formando_removido.cod_formando.ToString()));

                lbl_mensagem_formandos.Text = "Formando removido da turma com sucesso.";
                lbl_mensagem_formandos.CssClass = "alert alert-success";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels6", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder remover.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels7", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_gravar_alteracoes_formandos_Click(object sender, EventArgs e)
        {
            List<Formandos> lst_formandos = new List<Formandos>();

            foreach (ListItem item in lstb_formandos_turma.Items)
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
                Turmas.Inserir_Formandos_Turma(formando.cod_formando, Convert.ToInt32(lbl_cod_turma.Text), formando.cod_inscricao);
        }
    }
}