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
            lbl_mensagem.Text = "";
            lbl_mensagem.CssClass = "";

            if (ddl_curso.SelectedIndex != 0)
            {
                List<Formadores> lst_formadores = Formadores.Ler_FormadoresAll(Convert.ToInt32(ddl_curso.SelectedValue));

                List<Modulos> lst_modulos = Modulos.Ler_ModulosAll_Curso(Convert.ToInt32(ddl_curso.SelectedValue));

                List<Formandos> lst_formandos = Formandos.Ler_FormandosAll(Convert.ToInt32(ddl_curso.SelectedValue));

                List<Formadores_Modulos> lst_formadores_modulos = new List<Formadores_Modulos>();

                List<Formandos> lst_turmas_formandos = new List<Formandos>();

                lstb_formadores.DataSource = lst_formadores;
                lstb_formadores.DataTextField = "nome_completo";
                lstb_formadores.DataValueField = "cod_formador";
                lstb_formadores.DataBind();

                lstb_modulos.DataSource = lst_modulos;
                lstb_modulos.DataTextField = "ufcd_nome_modulo";
                lstb_modulos.DataValueField = "cod_modulo";
                lstb_modulos.DataBind();

                lstb_formandos_legiveis.DataSource = lst_formandos;
                lstb_formandos_legiveis.DataTextField = "nome_completo";
                lstb_formandos_legiveis.DataValueField = "cod_formando";
                lstb_formandos_legiveis.DataBind();

                lstb_formadores_modulos.DataSource = lst_formadores_modulos;
                lstb_formadores_modulos.DataTextField = "formador_modulo";
                lstb_formadores_modulos.DataValueField = "cod_formador_modulo";
                lstb_formadores_modulos.DataBind();

                lstb_formandos_turma.DataSource = lst_turmas_formandos;
                lstb_formandos_turma.DataTextField = "nome_completo";
                lstb_formandos_turma.DataValueField = "cod_formando";
                lstb_formandos_turma.DataBind();
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
                string nome_formador = lstb_formadores.SelectedItem.ToString();

                int duracao_modulo = Modulos.Check_Duracao_Modulo(Convert.ToInt32(lstb_modulos.SelectedValue));
                int horas_totais_formador = 0;
                horas_totais_formador += duracao_modulo;

                foreach (ListItem item in lstb_formadores_modulos.Items)
                {
                    string[] partes = item.Value.Split('|');
                    int cod_formador = Convert.ToInt32(partes[0]);
                    int cod_modulo_item = Convert.ToInt32(partes[1]);

                    if (item.Text.Contains(nome_formador))
                    {
                        horas_totais_formador += Modulos.Check_Duracao_Modulo(cod_modulo_item);
                    }
                }

                if (horas_totais_formador > 200)
                {
                    lbl_mensagem_formadores_modulos.Text = "Formador ultrapassará o límite de 200h por CET. Não pode adicionar mais.";
                    lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels", "$('#" + lbl_mensagem_formadores_modulos.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else
                {
                    string[] partes_modulo = lstb_modulos.SelectedItem.ToString().Split('|');
                    Formadores_Modulos formador_modulo = new Formadores_Modulos
                    {
                        cod_formador = Convert.ToInt32(lstb_formadores.SelectedValue),
                        nome_completo = lstb_formadores.SelectedItem.ToString(),
                        cod_modulo = Convert.ToInt32(lstb_modulos.SelectedValue),
                        nome_modulo = partes_modulo[1],
                        cod_ufcd = Convert.ToInt32(partes_modulo[0])
                    };

                    lstb_formadores_modulos.Items.Add(new ListItem(formador_modulo.formador_modulo, formador_modulo.cod_formador_modulo));

                    lbl_mensagem_formadores_modulos.Text = "Formador e módulo adicionados com sucesso.";
                    lbl_mensagem_formadores_modulos.CssClass = "alert alert-success";

                    Check_Horas_Totais(lstb_formadores.SelectedItem.ToString());

                    lstb_modulos.Items.RemoveAt(lstb_modulos.SelectedIndex);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels2", "$('#" + lbl_mensagem_formadores_modulos.ClientID + "').delay(2000).fadeOut('slow');", true);
                }
            }
            else
            {
                lbl_mensagem_formadores_modulos.Text = "Tem de escolher um formador e um módulo antes de adicionar ao curso.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels3", "$('#" + lbl_mensagem_formadores_modulos.ClientID + "').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {
            if (lstb_formadores_modulos.SelectedIndex != -1)
            {
                ListItem selectedListItem = lstb_formadores_modulos.SelectedItem;
                string[] partes_value = selectedListItem.Value.Split('|');
                string[] partes_item = selectedListItem.Text.Split('|');
                int cod_formador = Convert.ToInt32(partes_value[0]);
                int cod_modulo = Convert.ToInt32(partes_value[1]);
                string nome_completo = partes_item[0];
                string cod_ufcd = partes_item[1];
                string nome_modulo = partes_item[2];

                Formadores formador_removido = new Formadores
                {
                    nome_completo = nome_completo.Trim(),
                    cod_formador = cod_formador
                };

                Modulos modulo_removido = new Modulos
                {
                    cod_ufcd = Convert.ToInt32(cod_ufcd),
                    nome_modulo = nome_modulo.Trim(),
                    cod_modulo = cod_modulo
                };

                lstb_formadores_modulos.Items.Remove(selectedListItem);

                bool contem = false;
                foreach (ListItem formador in lstb_formadores.Items)
                {
                    if (formador.Text.Trim() == partes_item[0].Trim())
                    {
                        contem = true;
                        break;
                    }
                }

                if (!contem)
                    lstb_formadores.Items.Add(new ListItem(formador_removido.nome_completo, formador_removido.cod_formador.ToString()));

                lstb_modulos.Items.Add(new ListItem(modulo_removido.ufcd_nome_modulo, modulo_removido.cod_modulo.ToString()));

                lbl_mensagem_formadores_modulos.Text = "Formador e módulo removidos da turma.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels11", "$('#" + lbl_mensagem_formadores_modulos.ClientID + "').delay(2000).fadeOut('slow');", true);

                Check_Horas_Totais(formador_removido.nome_completo);
            }
            else
            {
                lbl_mensagem_formadores_modulos.Text = "Tem de selecionar um formador e módulo inserido antes de poder remover.";
                lbl_mensagem_formadores_modulos.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels11", "$('#" + lbl_mensagem_formadores_modulos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }



        protected void lstb_formadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int horas_totais_formador = 0;

            foreach (ListItem item in lstb_formadores_modulos.Items)
            {
                string[] partes = item.Value.Split('|');
                int cod_formador = Convert.ToInt32(partes[0]);
                int cod_modulo = Convert.ToInt32(partes[1]);

                if (item.Text.Contains(lstb_formadores.SelectedItem.ToString()))
                {
                    horas_totais_formador += Modulos.Check_Duracao_Modulo(cod_modulo);
                }
            }

            lbl_horas_totais_formador.Text = horas_totais_formador.ToString() + " horas de um máximo de 200 horas.";
            lbl_horas_totais_formador.CssClass = "alert alert-info";

            lbl_mensagem_formadores_modulos.Text = "";
            lbl_mensagem_formadores_modulos.CssClass = "";

            lbl_mensagem.Text = "";
            lbl_mensagem.CssClass = "";
        }

        private void Check_Horas_Totais(string nome_formador)
        {
            int horas_totais_formador = 0;

            foreach (ListItem item in lstb_formadores_modulos.Items)
            {
                string[] partes = item.Value.Split('|');
                int cod_formador = Convert.ToInt32(partes[0]);
                int cod_modulo_item = Convert.ToInt32(partes[1]);

                if (item.Text.Contains(nome_formador))
                {
                    horas_totais_formador += Modulos.Check_Duracao_Modulo(cod_modulo_item);
                }
            }

            if (horas_totais_formador < 200)
            {
                lbl_horas_totais_formador.Text = horas_totais_formador.ToString() + " horas de um máximo de 200 horas.";
                lbl_horas_totais_formador.CssClass = "alert alert-info";
            } 
            else if (horas_totais_formador == 200)
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

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels4", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder adicionar.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";

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

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels6", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem_formandos.Text = "Tem de selecionar um formando antes de poder remover.";
                lbl_mensagem_formandos.CssClass = "alert alert-danger";

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";

                lbl_mensagem.Text = "";
                lbl_mensagem.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels7", "$('#" + lbl_mensagem_formandos.ClientID + "').delay(2000).fadeOut('slow');", true);
            }
        }

        protected void btn_criar_turma_Click(object sender, EventArgs e)
        {
            DateTime data_inicio;

            if (DateTime.TryParse(tb_data_inicio.Text, out data_inicio))
            {
                if (ddl_curso.SelectedIndex == 0 || ddl_curso.SelectedIndex == -1)
                {
                    lbl_mensagem.Text = "Tem de escolher um curso antes de criar a turma.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels111", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else if (ddl_regime.SelectedIndex == 0 || ddl_regime.SelectedIndex == -1)
                {
                    lbl_mensagem.Text = "Tem de escolher um regime antes de criar a turma.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels1111", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else if (lstb_modulos.Items.Count != 0)
                {
                    lbl_mensagem.Text = "Ainda tem módulos por alocar a formadores. Aloque todos os módulos antes de continuar.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels1112", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else if (lstb_formandos_turma.Items.Count < 5)
                {
                    lbl_mensagem.Text = "Uma turma tem de ter pelo menos 5 formandos.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels113", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else if (data_inicio <= DateTime.Today)
                {
                    lbl_mensagem.Text = "A data de início não é válida.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels114", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
                }
                else
                {
                    int cod_curso = Convert.ToInt32(ddl_curso.SelectedValue);
                    int cod_regime = Convert.ToInt32(ddl_regime.SelectedValue);

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

                    List<Formadores_Modulos> lst_formadores_modulos = new List<Formadores_Modulos>();

                    foreach (ListItem item in lstb_formadores_modulos.Items)
                    {
                        string[] partes_item = item.Text.ToString().Split('|');
                        string nome_formador = partes_item[0].Trim();
                        int cod_ufcd = Convert.ToInt32(partes_item[1]);
                        string nome_modulo = partes_item[2];
                        string[] partes_value = item.Value.ToString().Split('|');
                        int cod_formador = Convert.ToInt32(partes_value[0]);
                        int cod_modulo = Convert.ToInt32(partes_value[1]);

                        Formadores_Modulos formador_modulo = new Formadores_Modulos
                        {
                            nome_completo = nome_formador,
                            cod_formador = cod_formador,
                            nome_modulo = nome_modulo,
                            cod_modulo = cod_modulo,
                            cod_ufcd = cod_ufcd
                        };

                        lst_formadores_modulos.Add(formador_modulo);
                    }

                    Turmas.Inserir_Turma(cod_curso, data_inicio, cod_regime, lst_formandos, lst_formadores_modulos);

                    lbl_mensagem.Text = "Turma criada com sucesso!";
                    lbl_mensagem.CssClass = "alert alert-success";

                    lbl_mensagem_formadores_modulos.Text = "";
                    lbl_mensagem_formadores_modulos.CssClass = "";

                    lbl_mensagem_formandos.Text = "";
                    lbl_mensagem_formandos.CssClass = "";

                    lbl_horas_totais_formador.Text = "";
                    lbl_horas_totais_formador.CssClass = "";

                    ddl_curso.SelectedIndex = -1;
                    ddl_regime.SelectedIndex = -1;
                    tb_data_inicio.Text = "";

                    lstb_formadores.Items.Clear();
                    lstb_modulos.Items.Clear();
                    lstb_formadores_modulos.Items.Clear();
                    lstb_formandos_legiveis.Items.Clear();
                    lstb_formandos_turma.Items.Clear();
                }
            }
            else
            {
                lbl_mensagem.Text = "Tem de escolher uma data de início de curso.";
                lbl_mensagem.CssClass = "alert alert-danger";

                lbl_mensagem_formadores_modulos.Text = "";
                lbl_mensagem_formadores_modulos.CssClass = "";

                lbl_mensagem_formandos.Text = "";
                lbl_mensagem_formandos.CssClass = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeLabels9", "$('#" + lbl_mensagem.ClientID + "').delay(5000).fadeOut('slow');", true);
            }
        }
    }
}