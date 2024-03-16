using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class personal_zone_editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if ((Request.QueryString["cod_user"].ToString() != Session["cod_user"].ToString()))
            {
                if (Validation.Check_IsStaff(Session["username"].ToString()))
                {
                    if (!Page.IsPostBack)
                    {
                        List<Users> user = Users.Ler_Info_User(Convert.ToInt32(Request.QueryString["cod_user"]));

                        if (user.Count > 0)
                        {
                            lbl_cod_user.Text = user[0].cod_user.ToString();
                            lbl_cod_user.Font.Bold = true;

                            lbl_username.Text = Session["username"].ToString();
                            lbl_username.Font.Bold = true;

                            tb_nome_proprio.Text = user[0].nome_proprio.ToString();
                            tb_nome_proprio.Font.Bold = true;

                            tb_apelido.Text = user[0].apelido.ToString();
                            tb_apelido.Font.Bold = true;

                            tb_morada.Text = user[0].morada;
                            tb_morada.Font.Bold = true;

                            tb_cod_postal.Text = user[0].cod_postal;
                            tb_cod_postal.Font.Bold = true;

                            lbl_perfis.Text = user[0].perfis;
                            lbl_perfis.Font.Bold = true;

                            lbl_email.Text = user[0].email;
                            lbl_email.Font.Bold = true;

                            tb_data_nascimento.Text = user[0].data_nascimento.ToString();
                            tb_data_nascimento.Font.Bold = true;

                            tb_num_contacto.Text = user[0].num_contacto;
                            tb_num_contacto.Font.Bold = true;

                            img_user.ImageUrl = user[0].foto;
                        }
                    }
                }
                else
                {
                    Response.Redirect("personal_zone.aspx");
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    List<Users> user = Users.Ler_Info_User(Convert.ToInt32(Request.QueryString["cod_user"]));

                    if (user.Count > 0)
                    {
                        lbl_cod_user.Text = user[0].cod_user.ToString();
                        lbl_cod_user.Font.Bold = true;

                        lbl_username.Text = Session["username"].ToString();
                        lbl_username.Font.Bold = true;

                        tb_nome_proprio.Text = user[0].nome_proprio.ToString();
                        tb_nome_proprio.Font.Bold = true;

                        tb_apelido.Text = user[0].apelido.ToString();
                        tb_apelido.Font.Bold = true;

                        tb_morada.Text = user[0].morada;
                        tb_morada.Font.Bold = true;

                        tb_cod_postal.Text = user[0].cod_postal;
                        tb_cod_postal.Font.Bold = true;

                        lbl_perfis.Text = user[0].perfis;
                        lbl_perfis.Font.Bold = true;

                        lbl_email.Text = user[0].email;
                        lbl_email.Font.Bold = true;

                        tb_data_nascimento.Text = user[0].data_nascimento.ToString("yyyy-MM-dd");
                        tb_data_nascimento.Font.Bold = true;

                        tb_num_contacto.Text = user[0].num_contacto;
                        tb_num_contacto.Font.Bold = true;

                        img_user.ImageUrl = user[0].foto;
                    }
                }
            }
        }

        protected void btn_logout2_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx", false);
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            Stream imgStream = fu_foto.PostedFile.InputStream;
            int tamanho_ficheiro = fu_foto.PostedFile.ContentLength;
            byte[] imgBinaryData = new byte[tamanho_ficheiro];
            imgStream.Read(imgBinaryData, 0, tamanho_ficheiro);

            if (DateTime.TryParse(tb_data_nascimento.Text, out DateTime data_nascimento))
            {
                TimeSpan ageSpan = DateTime.Today - data_nascimento;
                int years = DateTime.Today.Year - data_nascimento.Year;
                if (data_nascimento > DateTime.Today.AddYears(-years))
                    years--;
                int months = DateTime.Today.Month - data_nascimento.Month;
                if (DateTime.Today.Month < data_nascimento.Month || (DateTime.Today.Month == data_nascimento.Month && DateTime.Today.Day < data_nascimento.Day))
                {
                    years--;
                    months += 12;
                }
                int days = ageSpan.Days;

                if (years >= 18 && years <= 121 || (years == 18 && (months > 0 || (months == 0 && days >= 0))))
                {
                    Users.Editar_User(Convert.ToInt32(lbl_cod_user.Text), tb_nome_proprio.Text, tb_apelido.Text, tb_morada.Text, tb_cod_postal.Text, data_nascimento, tb_num_contacto.Text, true);
                    if (fu_foto.HasFile)
                        Users.Editar_User_Foto(Convert.ToInt32(lbl_cod_user.Text), imgBinaryData);
                    Response.Redirect("personal_zone.aspx?msg=yesedit", false);
                }
                else if (years > 121)
                {
                    lbl_mensagem.Text = "Introduza uma idade válida até 121 anos, por favor.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                }
                else
                {
                    lbl_mensagem.Text = "É necessário ter pelo menos 18 anos.";
                    lbl_mensagem.CssClass = "alert alert-danger";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                }
            }
            else
            {
                lbl_mensagem.Text = "Formato de data inválido! Por favor tente novamente.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_change_pw_Click(object sender, EventArgs e)
        {
            // Condição para mudar a password do utilizador
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw.Text) == 1 && tb_new_pw.Text == tb_new_pw_repeat.Text && tb_new_pw.Text != tb_pw.Text && tb_new_pw_repeat.Text != tb_pw.Text)
            {
                Users.Update_Password(Extract.Email(Session["username"].ToString()), Validation.EncryptString(tb_new_pw.Text));
                lbl_mensagem.Text = "Password mudada com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
            else
            {
                lbl_mensagem.Text = "Erro ao mudar a password. Por favor tente novamente.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_change_username_Click(object sender, EventArgs e)
        {
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw_username.Text) == 1 && Session["username"].ToString() != tb_new_username.Text)
            {
                Users.Update_Username(Session["username"].ToString(), tb_new_username.Text);
                Session["username"] = tb_new_username.Text;
                Response.Redirect("personal_zone.aspx?msg=yesuser", false);
            }
            else
            {
                lbl_mensagem.Text = "Erro ao mudar o username. Por favor tente novamente.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
        }

        protected void btn_change_email_Click(object sender, EventArgs e)
        {
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw_email.Text) == 1 && Session["email"].ToString() != tb_new_email.Text)
            {
                Users.Update_Email(Session["email"].ToString(), tb_new_email.Text);
                Session["email"] = tb_new_email.Text;
                Email.Send(tb_new_email.Text, Session["username"].ToString());
                Response.Redirect("logout.aspx?msg=yesemail", false);
            }
            else
            {
                lbl_mensagem.Text = "Erro ao mudar o email. Por favor tente novamente.";
                lbl_mensagem.CssClass = "alert alert-danger";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
        }
    }
}