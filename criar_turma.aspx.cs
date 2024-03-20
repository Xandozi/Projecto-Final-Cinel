using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class criar_turma : System.Web.UI.Page
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
                ddl_curso.Items.Insert(0, "---");
                ddl_regime.Items.Insert(0, "---");
            }
            //    }
        }

        protected void ddl_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_curso.SelectedIndex != 0)
            {
                List<Formadores> lst_formadores = Formadores.Ler_FormadoresAll(Convert.ToInt32(ddl_curso.SelectedValue));
                List<string> lst_formadores_string = new List<string>();

                List<Modulos> lst_modulos = Modulos.Ler_ModulosAll_Curso(Convert.ToInt32(ddl_curso.SelectedValue));
                List<string> lst_modulos_string = new List<string>();

                List<Formandos> lst_formandos = Formandos.Ler_FormandosAll(Convert.ToInt32(ddl_curso.SelectedValue));
                List<string> lst_formandos_string = new List<string>();

                foreach (Formadores formador in lst_formadores)
                {
                    string nome = formador.nome_proprio + " " + formador.apelido;

                    lst_formadores_string.Add(nome);
                }

                foreach (Modulos modulo in lst_modulos)
                {
                    string nome = modulo.cod_ufcd + " | " + modulo.nome_modulo;

                    lst_modulos_string.Add(nome);
                }

                foreach (Formandos formando in lst_formandos)
                {
                    string nome = formando.nome_proprio + " " + formando.apelido;

                    lst_formandos_string.Add(nome);
                }

                lstb_formadores.DataSource = lst_formadores_string;
                lstb_formadores.DataBind();

                lstb_modulos.DataSource = lst_modulos_string;
                lstb_modulos.DataBind();

                lstb_formandos_legiveis.DataSource = lst_formandos_string;
                lstb_formandos_legiveis.DataBind();
            }
            else if (ddl_curso.SelectedIndex == 0)
            {
                lstb_formadores.Items.Clear();
                lstb_modulos.Items.Clear();
                lstb_formandos_legiveis.Items.Clear();
                lstb_formandos_turma.Items.Clear();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (lstb_formadores.SelectedIndex != -1 && lstb_modulos.SelectedIndex != -1)
            {
                string nome_formador = lstb_formadores.SelectedValue;
                string[] partes_modulo = lstb_modulos.SelectedValue.Split('|');
                int cod_ufcd = Convert.ToInt32(partes_modulo[0]);

                int duracao_modulo = Modulos.Check_Duracao_Modulo(cod_ufcd);
                int horas_totais_formador = 0;

                foreach (ListItem formador_modulo in lstb_formadores_modulos.Items)
                {
                    if (formador_modulo.ToString().Contains(nome_formador))
                    {
                        string[] partes = formador_modulo.ToString().Split('|');
                        int cod_ufcd_formador = Convert.ToInt32(partes[1]);
                        horas_totais_formador += Modulos.Check_Duracao_Modulo(cod_ufcd_formador);
                    }
                }

                if (horas_totais_formador + duracao_modulo > 200)
                {
                    lbl_mensagem_formadores_modulos.Text = "Formador ultrapassará o límite de 200h por CET. Não pode adicionar mais.";
                    lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeDangerAlert", "$('.alert.alert-danger').delay(5000).fadeOut('slow');", true);
                }
                else
                {
                    string formador_modulo = nome_formador + " | " + partes_modulo[0] + " | " + partes_modulo[1];
                    lstb_formadores_modulos.Items.Add(formador_modulo);
                    lstb_modulos.Items.RemoveAt(lstb_modulos.SelectedIndex);

                    lbl_mensagem_formadores_modulos.Text = "Formador e módulo adicionados com sucesso.";
                    lbl_mensagem_formadores_modulos.CssClass = "alert alert-success";

                    Check_Horas_Totais();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlerts", "$('.alert.alert-danger, .alert.alert-success').delay(2000).fadeOut('slow');", true);
                }
            }
            else
            {
                lbl_mensagem_formadores_modulos.Text = "Tem de escolher um formador e um módulo antes de adicionar ao curso.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlerts", "$('.alert.alert-danger, .alert.alert-success').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {
            if (lstb_formadores_modulos.SelectedIndex != -1)
            {
                string[] partes = lstb_formadores_modulos.SelectedValue.ToString().Split('|');
                partes[0].Trim();
                int duracao_modulo_removido = Modulos.Check_Duracao_Modulo(Convert.ToInt32(partes[1]));

                lstb_formadores_modulos.Items.RemoveAt(lstb_formadores_modulos.SelectedIndex);

                bool contem = false;
                foreach (ListItem formador in lstb_formadores.Items)
                {
                    if (formador.Text.Trim() == partes[0].Trim())
                    {
                        contem = true;
                        break;
                    }
                }

                if (!contem)
                    lstb_formadores.Items.Add(partes[0]);

                lstb_modulos.Items.Add(partes[1] + " | " + partes[2]);

                Check_Horas_Totais();

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";
            }
            else
            {
                lbl_mensagem_formadores_modulos.Text = "Tem de selecionar um formador e módulo inserido antes de poder remover.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";
            }
        }

        protected void lstb_formadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check_Horas_Totais();
        }

        private void Check_Horas_Totais()
        {
            string nome_formador = lstb_formadores.SelectedValue;
            int horas_totais_formador = 0;

            foreach (ListItem formador_modulo in lstb_formadores_modulos.Items)
            {
                if (formador_modulo.ToString().Contains(nome_formador))
                {
                    string[] partes = formador_modulo.ToString().Split('|');
                    int cod_ufcd_formador = Convert.ToInt32(partes[1]);
                    horas_totais_formador += Modulos.Check_Duracao_Modulo(cod_ufcd_formador);
                }
            }

            lbl_horas_totais_formador.Text = horas_totais_formador.ToString() + " horas de um máximo de 200 horas.";
            lbl_horas_totais_formador.CssClass = "alert alert-info";

            if (horas_totais_formador >= 200)
            {
                lstb_formadores.Items.RemoveAt(lstb_formadores.SelectedIndex);
                lbl_mensagem_formadores_modulos.Text = "Formador atingiu as 200h, foi removido da lista.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-success";

                lbl_horas_totais_formador.Text = "";
                lbl_horas_totais_formador.CssClass = "";
            }
        }

        protected void btn_add_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_legiveis.SelectedIndex != -1)
            {
                lstb_formandos_turma.Items.Add(lstb_formandos_legiveis.SelectedValue);
                lstb_formandos_legiveis.Items.RemoveAt(lstb_formandos_legiveis.SelectedIndex);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder adicionar.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";
            }
        }

        protected void btn_remove_formandos_Click(object sender, EventArgs e)
        {
            if (lstb_formandos_turma.SelectedIndex != -1)
            {
                lstb_formandos_legiveis.Items.Add(lstb_formandos_turma.SelectedValue);
                lstb_formandos_turma.Items.RemoveAt(lstb_formandos_turma.SelectedIndex);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder remover.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";
            }
        }

        protected void btn_criar_turma_Click(object sender, EventArgs e)
        {
            //DateTime data_inicio;
            //int cod_curso = Convert.ToInt32(ddl_curso.SelectedValue);
            //int cod_regime = Convert.ToInt32(ddl_regime.SelectedValue);

            //if (DateTime.TryParse(tb_data_inicio.Text, out data_inicio))
            //{
            //    if ((ddl_curso.SelectedIndex != 0 || ddl_curso.SelectedIndex != -1) && (ddl_regime.SelectedIndex != 0 || ddl_regime.SelectedIndex != -1) && lstb_formadores.Items.Count == 0 && lstb_modulos.Items.Count == 0 && lstb_formandos_turma.Items.Count >= 5 && data_inicio > DateTime.Today) 
            //    {
            //        Turmas.Inserir_Turma(cod_curso, data_inicio, cod_regime, )
            //    }
            //}
        }
    }
}