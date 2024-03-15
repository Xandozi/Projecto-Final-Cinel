using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class criar_curso : System.Web.UI.Page
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
        }

        protected void btn_add_ufcd_Click(object sender, EventArgs e)
        {
            string modulo = Modulos.Extract_CodUFCD_Nome_Modulo(Convert.ToInt32(tb_cod_ufcd.Text));
            bool existe = false;

            foreach (ListItem item in lb_selected_ufcds.Items)
            {
                if (item.Value == modulo)
                {
                    existe = true;
                    break;
                }
            }

            if (!existe && Modulos.Check_ifExists_Modulo(Convert.ToInt32(tb_cod_ufcd.Text)))
            {
                lb_selected_ufcds.Items.Add(modulo);
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
            if (lb_selected_ufcds.SelectedIndex != -1)
            {
                ListItem selectedUFCD = lb_selected_ufcds.SelectedItem;

                lb_selected_ufcds.Items.Remove(selectedUFCD);

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

        protected void btn_criar_Click(object sender, EventArgs e)
        {
            List<int> ufcds = new List<int>();

            foreach (ListItem item in lb_selected_ufcds.Items)
            {
                string[] parts = item.Text.Split('-');

                if (parts.Length >= 2 && int.TryParse(parts[0].Trim(), out int cod_ufcd))
                {
                    ufcds.Add(cod_ufcd);
                }
            }

            if (ufcds.Count > 0)
            {
                if (Cursos.Inserir_Curso(Convert.ToInt32(tb_cod_qualificacao.Text), tb_designacao.Text, Convert.ToInt32(tb_duracao_estagio.Text), DateTime.Today, ufcds) == 1)
                {
                    lbl_mensagem.Text = "Curso criado com sucesso!";
                    lbl_mensagem.CssClass = "alert alert-success";
                    tb_cod_qualificacao.Text = "";
                    tb_cod_ufcd.Text = "";
                    tb_designacao.Text = "";
                    tb_duracao_estagio.Text = "";
                    lb_selected_ufcds.Items.Clear();
                }
                else if (Cursos.Inserir_Curso(Convert.ToInt32(tb_cod_qualificacao.Text), tb_designacao.Text, Convert.ToInt32(tb_duracao_estagio.Text), DateTime.Today, ufcds) == 2)
                {
                    lbl_mensagem.Text = "Código de qualificação já existe na base de dados!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
                else
                {
                    lbl_mensagem.Text = "Designação do curso já existe na base de dados!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                }
            }
            else
            {
                lbl_mensagem.Text = "Tem de selecionar pelo menos 1 UFCD.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(3000).fadeOut('slow');", true);
        }
    }
}