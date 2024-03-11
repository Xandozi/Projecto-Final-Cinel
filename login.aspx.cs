﻿using ASPSnippets.GoogleAPI;
using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            // Procedimento de sign in com a conta Google
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GoogleClientID"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["RedirectURI_Login"];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json); // Obtenção do json com a informação do utilizador
                    
                    // Extrair primeiro e ultimo nome
                    string[] names = profile.Name.Split(' ');
                    string firstName = names[0];
                    string lastName = names.Length > 1 ? names[names.Length - 1] : string.Empty;

                    int valido = Users.Inserir_User_Google(profile.Name, profile.Email, firstName, lastName); // Retorno da função. 1 para válido e ativo ou seja pode fazer login
                    int cod_user = Extract.Code_Via_Email(profile.Email); // Extrair o código de utilizador através do email do utilizador fornecido pelo google

                    if (profile.Verified_Email == "True" && valido == 1)        // Caso autenticação dê certo e o utilizador esteja ativo
                    {
                        Session["username"] = Extract.Username(cod_user);
                        Session["cod_user"] = cod_user;
                        Session["nome_proprio"] = firstName;
                        Session["apelido"] = lastName;

                        List<Validation> perfis = Validation.Check_Perfil(cod_user);
                        StringBuilder concatenatedPerfis = new StringBuilder();
                        foreach (Validation perfil in perfis)
                        {
                            concatenatedPerfis.Append(perfil.perfil);
                            concatenatedPerfis.Append(", ");
                        }
                        
                        if (concatenatedPerfis.Length > 0)
                        {
                            concatenatedPerfis.Length -= 2;
                        }

                        Session["google_foto"] = profile.Picture;
                        Session["perfil"] = concatenatedPerfis.ToString();
                        Session["email"] = profile.Email;
                        Session["logged"] = "yes";
                        Session["googlefb_log"] = "yes";
                        Response.Redirect("home.aspx", false);
                    }
                    else if (profile.Verified_Email == "True" && valido != 1)   // Caso autenticação seja correta mas o utilizador não esteja ativo
                    {
                        Email.Send(profile.Email, Extract.Username(cod_user));  // Enviar email de ativação novamente
                        Response.Redirect("login.aspx?message=Active%20a%20sua%20conta%20via%20email%20porfavor", false);     // Redirecionamento para a página com uma mensagem no url
                    }
                    // Else será executado pela autenticação errada do google que virá abaixo
                }

                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }

                // Caso haja uma mensagem passada pelo url, a lbl mensagem deverá mostrar isto
                if (Request.QueryString["msg"] == "yesemail")
                {
                    lbl_mensagem.Text = "Email mudado com sucesso!! Por favor verifique a sua caixa de entrada pois receberá um email de ativação.";
                    lbl_mensagem.CssClass = "alert alert-success";
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["message"]))
            {
                string message = Request.QueryString["message"];

                lbl_mensagem.Text = message;
                lbl_mensagem.CssClass = "alert alert-danger";
            }
        }

        protected void Login(object sender, EventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
        }

        protected void Clear(object sender, EventArgs e)
        {
            GoogleConnect.Clear(Request.QueryString["code"]);
        }

        public class GoogleProfile
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; }
            public string Email { get; set; }
            public string Verified_Email { get; set; }
        }
        protected void btn_login_Click(object sender, EventArgs e)
        {
            // Validação do login bem sucedida
            if (Validation.Check_Login(tb_username.Text, tb_pw.Text) == 1)
            {
                Session["username"] = tb_username.Text;
                Session["cod_user"] = Extract.Code(tb_username.Text);

                string nome_completo = Extract.Nome_Completo(Extract.Code(tb_username.Text));

                int firstSpaceIndex = nome_completo.IndexOf(' ');
                string nome_proprio = nome_completo.Substring(0, firstSpaceIndex);
                string apelido = nome_completo.Substring(firstSpaceIndex + 1);

                Session["nome_proprio"] = nome_proprio;
                Session["apelido"] = apelido;

                List<Validation> perfis = Validation.Check_Perfil(Extract.Code(tb_username.Text));
                StringBuilder concatenatedPerfis = new StringBuilder();
                foreach (Validation perfil in perfis)
                {
                    concatenatedPerfis.Append(perfil.perfil);
                    concatenatedPerfis.Append(", ");
                }

                if (concatenatedPerfis.Length > 0)
                {
                    concatenatedPerfis.Length -= 2;
                }

                Session["perfil"] = concatenatedPerfis.ToString();
                Session["email"] = Extract.Email(tb_username.Text);
                Session["logged"] = "yes";
                Response.Redirect("home.aspx", false);
            }
            else if (Validation.Check_Login(tb_username.Text, tb_pw.Text) == 2)   // Utilizador não ativo
            {
                lbl_mensagem.Text = "A sua conta não está ativada. Por favor veja a sua caixa de correio.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else // Credenciais erradas
            {
                lbl_mensagem.Text = "Credenciais inseridas estão erradas.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
        }

        protected void btn_forgot_pw_Click(object sender, EventArgs e)
        {
            if (!Validation.Check_Email(tb_email.Text))
            {
                Email.Send_Forgot_PW(tb_email.Text);
            }
            else
            {
                lbl_mensagem.Text = "Email não existe na nossa base de dados!";
            }
        }
    }
}