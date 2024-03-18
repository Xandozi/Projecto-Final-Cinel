using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class utilizadores_detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (Validation.Check_IsStaff(Session["username"].ToString()))
            {
                if (!Page.IsPostBack)
                {
                    List<Users> user = Users.Ler_Info_User(Convert.ToInt32(Request.QueryString["cod_user"]));

                    if (user.Count > 0)
                    {
                        lbl_cod_user.Text = user[0].cod_user.ToString();
                        lbl_cod_user.Font.Bold = true;

                        lbl_username.Text = user[0].username.ToString();
                        lbl_username.Font.Bold = true;

                        lbl_nome_completo.Text = user[0].nome_proprio.ToString() + " " + user[0].apelido.ToString();
                        lbl_nome_completo.Font.Bold = true;

                        lbl_morada.Text = user[0].morada;
                        lbl_morada.Font.Bold = true;

                        lbl_cod_postal.Text = user[0].cod_postal;
                        lbl_cod_postal.Font.Bold = true;

                        lbl_perfis.Text = user[0].perfis;
                        lbl_perfis.Font.Bold = true;

                        lbl_email.Text = user[0].email;
                        lbl_email.Font.Bold = true;

                        lbl_data_nascimento.Text = user[0].data_nascimento.ToString("yyyy-MM-dd");
                        lbl_data_nascimento.Font.Bold = true;

                        lbl_num_contacto.Text = user[0].num_contacto;
                        lbl_num_contacto.Font.Bold = true;

                        img_user.ImageUrl = user[0].foto;
                    }
                    if (Request.QueryString["msg"] == "yesedit")
                    {
                        lbl_mensagem.Text = "Informações alteradas com sucesso!";
                        lbl_mensagem.CssClass = "alert alert-success";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                    }
                }
            }
            else
            {
                Response.Redirect("personal_zone.aspx");
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"editar_utilizador.aspx?cod_user={lbl_cod_user.Text}");
        }
    }
}