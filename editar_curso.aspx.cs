using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class editar_curso : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    List<Cursos> curso = Cursos.Ler_Curso(Convert.ToInt32(Request.QueryString["cod_curso"]));

                    if (curso.Count > 0)
                    {
                        tb_cod_qualificacao.Text = curso[0].cod_qualificacao.ToString();
                        tb_cod_qualificacao.Font.Bold = true;

                        tb_designacao.Text = curso[0].nome_curso.ToString();
                        tb_designacao.Font.Bold = true;

                        lb_ufcds.DataSource = Cursos.Ler_Curso_UFCD(Convert.ToInt32(Request.QueryString["cod_curso"]));

                        lbl_duracao_curso.Text = curso[0].duracao_curso.ToString();
                        lbl_duracao_curso.Font.Bold = true;

                        tb_duracao.Text = curso[0].duracao_estagio.ToString();
                        tb_duracao.Font.Bold = true;

                        lbl_data_criacao.Text = curso[0].data_criacao.ToShortDateString();
                        lbl_data_criacao.Font.Bold = true;

                        lbl_ultimo_update.Text = curso[0].ultimo_update.ToString();
                        lbl_ultimo_update.Font.Bold = true;
                    }

                }
            }
        }

        protected void btn_add_ufcd_Click(object sender, EventArgs e)
        {
            string modulo = Modulos.Extract_CodUFCD_Nome_Modulo(Convert.ToInt32(tb_cod_ufcd.Text));
            bool existe = false;

            foreach (ListItem item in lb_ufcds.Items)
            {
                if (item.Value == modulo)
                {
                    existe = true;
                    break;
                }
            }

            if (!existe && Modulos.Check_ifExists_Modulo(Convert.ToInt32(tb_cod_ufcd.Text)))
            {
                lb_ufcds.Items.Add(modulo);
                lbl_mensagem.Text = "Módulo inserido na lista com sucesso.";
                lbl_mensagem.CssClass = "alert alert-success";
            }
            else if (existe)
            {
                lbl_mensagem.Text = "Módulo já foi inserido no curso.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (!Modulos.Check_ifExists_Modulo(Convert.ToInt32(tb_cod_ufcd.Text)))
            {
                lbl_mensagem.Text = "Módulo não existe na base de dados.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(3000).fadeOut('slow');", true);
        }

        protected void btn_remove_ufcd_Click(object sender, EventArgs e)
        {
            if (lb_ufcds.SelectedIndex != -1)
            {
                ListItem selectedUFCD = lb_ufcds.SelectedItem;

                lb_ufcds.Items.Remove(selectedUFCD);

                lbl_mensagem.Text = "Módulo removido da lista com sucesso.";
                lbl_mensagem.CssClass = "alert alert-success";
            }
            else
            {
                lbl_mensagem.Text = "Seleccione uma UFCD antes de remover.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(3000).fadeOut('slow');", true);
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            List<int> ufcds = new List<int>();

            foreach (ListItem item in lb_ufcds.Items)
            {
                string[] parts = item.Text.Split('-');

                if (parts.Length >= 2 && int.TryParse(parts[0].Trim(), out int cod_ufcd))
                {
                    ufcds.Add(cod_ufcd);
                }
            }

            if (ufcds.Count > 0)
            {
                foreach (ListItem item in lb_ufcds.Items)
                {
                    int value;
                    if (int.TryParse(item.Value, out value))
                        ufcds.Add(value);
                }

                if (Cursos.Editar_Curso(Convert.ToInt32(Request.QueryString["cod_curso"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_qualificacao.Text), DateTime.Now.ToString(), ufcds) == 1)
                {
                    Response.Redirect($"cursos_detalhe.aspx?cod_curso={Convert.ToInt32(Request.QueryString["cod_curso"])}");
                }
                else if (Cursos.Editar_Curso(Convert.ToInt32(Request.QueryString["cod_curso"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_qualificacao.Text), DateTime.Now.ToString(), ufcds) == 2)
                {
                    lbl_mensagem.Text = "Designação do curso já existe!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else if (Cursos.Editar_Curso(Convert.ToInt32(Request.QueryString["cod_curso"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_qualificacao.Text), DateTime.Now.ToString(), ufcds) == 3)
                {
                    lbl_mensagem.Text = "Código Qualificação do curso já existe!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else
                {
                    lbl_mensagem.Text = "O curso já tem uma turma inicializada, logo as UFCDs não podem ser editadas mas a restante informação do curso foi alterada.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
            }
            else
            {
                lbl_mensagem.Text = "Tem de seleccionar pelo menos 1 UFCD.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
        }
    }
}