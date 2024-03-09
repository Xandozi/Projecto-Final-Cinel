using ASPSnippets.GoogleAPI;
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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Procedimento de sign in com a conta Google
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GoogleClientID"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["RedirectURI_Register"];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

                    // Extrair primeiro e ultimo nome
                    string[] names = profile.Name.Split(' ');
                    string firstName = names[0];
                    string lastName = names.Length > 1 ? names[names.Length - 1] : string.Empty;

                    int valido = Insercao.Inserir_User_Google(profile.Name, profile.Email, firstName, lastName);
                    int cod_user = Extract.Code_Via_Email(profile.Email);

                    if (profile.Verified_Email == "True" && valido == 1)
                    {
                        Session["username"] = Extract.Username(cod_user);
                        Session["cod_user"] = cod_user;

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

                        Session["perfil"] = concatenatedPerfis.ToString();
                        Session["email"] = profile.Email;
                        Session["logged"] = "yes";
                        Response.Redirect("home.aspx", false);
                    }
                    else if (profile.Verified_Email == "True" && valido != 1)
                    {
                        Email.Send(profile.Email, Extract.Username(cod_user));
                        Response.Redirect("login.aspx?message=Activate%20your%20account%20via%20email", false);
                    }
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
            }
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            if (tb_pw.Text != tb_pw_rpt.Text)
            {
                lbl_mensagem.Text = "As passwords não coincidem!";
                tb_pw.Text = "";
                tb_pw_rpt.Text = "";
            }
            else
            {
                if (Validation.Check_Username(tb_username.Text) && Validation.Check_Email(tb_email.Text))
                {
                    if (DateTime.TryParse(tb_data_nascimento.Text, out DateTime data_nascimento))
                    {
                        Insercao.Inserir_User(tb_username.Text, tb_pw.Text, tb_email.Text, tb_primeiro_nome.Text, tb_apelido.Text, data_nascimento);
                        Email.Send(tb_email.Text, tb_username.Text);
                        lbl_mensagem.Text = "Utilizador registado com sucesso. Veja a sua caixa de correio, precisa de ativar a sua conta.";
                        lbl_mensagem.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lbl_mensagem.Text = "Formato de data inválido! Por favor tente novamente.";
                    }
                }
                else if (!Validation.Check_Username(tb_username.Text))
                {
                    lbl_mensagem.Text = "Username já existe!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                    tb_username.Text = "";
                }
                else if (!Validation.Check_Email(tb_email.Text))
                {
                    lbl_mensagem.Text = "Email já existe!";
                    tb_email.Text = "";
                }
                else
                {
                    lbl_mensagem.Text = "Username e email já existem.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                    tb_username.Text = "";
                    tb_email.Text = "";
                }
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
    }
}