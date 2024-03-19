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
                    ddl_curso.Items.Insert(0, "---");
                    ddl_regime.Items.Insert(0, "---");
                }
            }
        }

        protected void ddl_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ddl_curso.SelectedIndex;

            ddl_curso.Items.Clear();
            ddl_curso.Items.Insert(0, "---");
            ddl_curso.DataBind();
            curso.DataBind();
            ddl_curso.SelectedIndex = index;

            if (ddl_curso.SelectedIndex != 0)
            {
                List<Formadores> lst_formadores = Formadores.Ler_FormadoresAll();
                List<string> lst_formadores_string = new List<string>();

                List<Modulos> lst_modulos = Modulos.Ler_ModulosAll_Curso(Convert.ToInt32(ddl_curso.SelectedValue));
                List<string> lst_modulos_string = new List<string>();

                foreach (Formadores formador in lst_formadores)
                {
                    string nome = formador.nome_proprio + " " + formador.apelido;

                    lst_formadores_string.Add(nome);
                }

                foreach (Modulos modulo in lst_modulos)
                {
                    string nome = modulo.cod_ufcd + " - " + modulo.nome_modulo;

                    lst_modulos_string.Add(nome);
                }

                lstb_formadores.DataSource = lst_formadores_string;
                lstb_formadores.DataBind();

                lstb_modulos.DataSource = lst_modulos_string;
                lstb_modulos.DataBind();
            }
            else if (ddl_curso.SelectedIndex == 0)
            {
                lstb_formadores.Items.Clear();
                lstb_modulos.Items.Clear();
            }
        }

        protected void ddl_regime_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ddl_regime.SelectedIndex;

            ddl_regime.Items.Clear();
            ddl_regime.Items.Insert(0, "---");
            ddl_regime.DataBind();
            regime.DataBind();
            ddl_regime.SelectedIndex = index;
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string nome_formador = lstb_formadores.SelectedValue;
            string[] partes_modulo = lstb_modulos.SelectedValue.Split('-');
            int cod_ufcd = Convert.ToInt32(partes_modulo[0]);

            int duracao_modulo = Modulos.Check_Duracao_Modulo(cod_ufcd);
            int horas_totais_formador = 0;

            foreach (ListItem formador_modulo in lstb_formadores_modulos.Items)
            {
                if (formador_modulo.ToString().Contains(nome_formador))
                {
                    string[] partes = formador_modulo.ToString().Split(new char[] { '|', '-' });
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
                string formador_modulo = nome_formador + " | " + partes_modulo[0] + " - " + partes_modulo[1];
                lstb_formadores_modulos.Items.Add(formador_modulo);
                lstb_modulos.Items.RemoveAt(lstb_modulos.SelectedIndex);

                Check_Horas_Totais();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlerts", "$('.alert.alert-danger, .alert.alert-success').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {
            string[] partes = lstb_formadores_modulos.SelectedValue.ToString().Split(new char[] { '|', '-' });

            foreach (ListItem formador in lstb_formadores.Items)
            {

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
                    string[] partes = formador_modulo.ToString().Split(new char[] { '|', '-' });
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
    }
}