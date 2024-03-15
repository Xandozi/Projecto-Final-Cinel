using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class editar_modulo : System.Web.UI.Page
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
                    List<Modulos> modulo = Modulos.Ler_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"]));

                    if (modulo.Count > 0)
                    {
                        tb_cod_ufcd.Text = modulo[0].cod_ufcd.ToString();
                        tb_cod_ufcd.Font.Bold = true;

                        tb_designacao.Text = modulo[0].nome_modulo.ToString();
                        tb_designacao.Font.Bold = true;

                        tb_duracao.Text = modulo[0].duracao.ToString();
                        tb_duracao.Font.Bold = true;

                        lbl_data_criacao.Text = modulo[0].data_criacao.ToShortDateString();
                        lbl_data_criacao.Font.Bold = true;

                        lbl_ultimo_update.Text = modulo[0].ultimo_update.ToString();
                        lbl_ultimo_update.Font.Bold = true;

                        cb_ativo.Checked = modulo[0].ativo;
                    }

                }
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            if (Modulos.Editar_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_ufcd.Text), DateTime.Now, cb_ativo.Checked) == 1)
            {
                Response.Redirect($"modulos_detalhe.aspx?cod_modulo={Convert.ToInt32(Request.QueryString["cod_modulo"])}");
            }
            else if (Modulos.Editar_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_ufcd.Text), DateTime.Now, cb_ativo.Checked) == 2)
            {
                lbl_mensagem.Text = "Designação do Módulo já existe!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (Modulos.Editar_Modulo(Convert.ToInt32(Request.QueryString["cod_modulo"]), tb_designacao.Text, Convert.ToInt32(tb_duracao.Text), Convert.ToInt32(tb_cod_ufcd.Text), DateTime.Now, cb_ativo.Checked) == 3)
            {
                lbl_mensagem.Text = "Código UFCD do Módulo já existe!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else
            {
                lbl_mensagem.Text = "Erro ao editar o módulo!";
                lbl_mensagem.CssClass = "alert alert-danger";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
        }
    }
}