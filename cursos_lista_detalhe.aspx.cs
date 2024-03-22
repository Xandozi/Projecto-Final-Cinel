using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class cursos_lista_detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Cursos> curso = Cursos.Ler_Curso(Convert.ToInt32(Request.QueryString["cod_curso"]));

            if (curso.Count > 0)
            {
                lbl_area.Text = curso[0].area;
                lbl_area.Font.Bold = true;

                lbl_cod_qualificacao.Text = curso[0].cod_qualificacao.ToString();
                lbl_cod_qualificacao.Font.Bold = true;

                lbl_nome_curso.Text = curso[0].nome_curso.ToString();
                lbl_nome_curso.Font.Bold = true;

                lbl_duracao_curso.Text = curso[0].duracao_curso.ToString() + " horas";
                lbl_duracao_curso.Font.Bold = true;

                lb_ufcd.DataSource = Cursos.Ler_Curso_UFCD(Convert.ToInt32(Request.QueryString["cod_curso"]));

                lbl_duracao_estagio.Text = curso[0].duracao_estagio.ToString() + " horas";
                lbl_duracao_estagio.Font.Bold = true;

                lbl_data_criacao.Text = curso[0].data_criacao.ToShortDateString();
                lbl_data_criacao.Font.Bold = true;

                lbl_ultimo_update.Text = curso[0].ultimo_update.ToString();
                lbl_ultimo_update.Font.Bold = true;
            }
        }

        protected void btn_inscrever_formando_Click(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                lbl_mensagem.Text = "Precisa de estar autenticado para poder inscrever-se. Será redirecionado para o login em 10 segundos.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow', function() { window.location.href = 'login.aspx'; });", true);
            }
            else if (!Users.Check_IsUserInformationComplete(Extract.Code(Session["username"].ToString())))
            {
                lbl_mensagem.Text = "Primeiro tem de completar as informações sobre si na sua Área Pessoal. Será redirecionado para a sua Área Pessoal em 10 segundos.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow', function() { window.location.href = 'personal_zone.aspx'; });", true);
            }
            else
            {
                if (Inscricoes.Insert_Inscricao(2, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 1)
                {
                    lbl_mensagem.Text = "Obrigado pela sua inscrição! Agora apenas tem de aguardar que a sua inscrição seja validada.";
                    lbl_mensagem.CssClass = "alert alert-success";
                }
                else if (Inscricoes.Insert_Inscricao(2, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 2)
                {
                    lbl_mensagem.Text = "Já tem uma inscrição a aguardar validação neste curso. Aguarde por favor.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else if (Inscricoes.Insert_Inscricao(2, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 3)
                {
                    lbl_mensagem.Text = "Já tem uma inscrição feita neste curso. Quando começar o curso será notificado.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else if (Inscricoes.Insert_Inscricao(2, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 0)
                {
                    lbl_mensagem.Text = "Está neste momento inscrito num curso idêntico que está a decorrer. Não pode inscrever-se noutro.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else if (Inscricoes.Insert_Inscricao(2, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 4)
                {
                    lbl_mensagem.Text = "Já está inscrito neste curso como formador! Não pode inscrever-se como formando.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else
                {
                    lbl_mensagem.Text = "Erro ao inscrever-se.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow');", true);
            }
        }

        protected void btn_inscrever_formador_Click(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                lbl_mensagem.Text = "Precisa de estar autenticado para poder inscrever-se. Será redirecionado para o login em 10 segundos.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow', function() { window.location.href = 'login.aspx'; });", true);
            }
            else if (!Users.Check_IsUserInformationComplete(Extract.Code(Session["username"].ToString())))
            {
                lbl_mensagem.Text = "Primeiro tem de completar as informações sobre si na sua Área Pessoal. Será redirecionado para a sua Área Pessoal em 10 segundos.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow', function() { window.location.href = 'personal_zone.aspx'; });", true);
            }
            else
            {
                if (Inscricoes.Insert_Inscricao(1, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 1)
                {
                    lbl_mensagem.Text = "Obrigado pela sua inscrição! Agora apenas tem de aguardar que a sua inscrição como formador seja validada pelo staff.";
                    lbl_mensagem.CssClass = "alert alert-success";
                }
                else if (Inscricoes.Insert_Inscricao(1, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 2)
                {
                    lbl_mensagem.Text = "Está inscrito como formando neste curso. Não pode fazer inscrição como formador.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else if (Inscricoes.Insert_Inscricao(1, Extract.Code(Session["username"].ToString()), Convert.ToInt32(Request.QueryString["cod_curso"]), DateTime.Today) == 0)
                {
                    lbl_mensagem.Text = "Já está inscrito como formador neste curso.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else
                {
                    lbl_mensagem.Text = "Erro ao inscrever-se.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(10000).fadeOut('slow');", true);
            }
        }
    }
}