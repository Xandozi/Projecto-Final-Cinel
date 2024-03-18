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

                    int valido = Users.Inserir_User_Google(profile.Name, profile.Email, firstName, lastName);
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

                        Session["google_foto"] = profile.Picture;
                        Session["perfil"] = concatenatedPerfis.ToString();
                        Session["email"] = profile.Email;
                        Session["logged"] = "yes";
                        Response.Redirect("home.aspx", false);
                    }
                    else if (profile.Verified_Email == "True" && valido == 0)
                    {
                        if (Email.Send(profile.Email, Extract.Username(cod_user)))  // Enviar email de ativação novamente
                            Response.Redirect("login.aspx?message=Ative%20a%20sua%20conta%20via%20email%20porfavor", false);     // Redirecionamento para a página com uma mensagem no url
                        else
                            Response.Redirect("login.aspx?message=Erro%20ao%20enviar%20email.%20Fale%20com%20o%20suporte%20por%20favor", false);
                    }
                    else if (profile.Verified_Email == "True" && valido == 2)   // Caso autenticação seja correta mas o utilizador não esteja ativo
                    {
                        Response.Redirect("login.aspx?message=A%20sua%20conta%20foi%20apagada.%20Fale%20com%20o%20suporte%20porfavor", false);     // Redirecionamento para a página com uma mensagem no url
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
                lbl_mensagem.CssClass = "alert alert-danger";
                lbl_mensagem.Text = "As passwords não coincidem!";
                tb_pw.Text = "";
                tb_pw_rpt.Text = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
            else
            {
                if (Validation.Check_Username(tb_username.Text) && Validation.Check_Email(tb_email.Text))
                {
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
                            Users.Inserir_User(tb_username.Text, tb_pw.Text, tb_email.Text, tb_primeiro_nome.Text, tb_apelido.Text, data_nascimento);
                            if (Email.Send(tb_email.Text, tb_username.Text))
                            {
                                lbl_mensagem.Text = "Utilizador registado com sucesso. Veja a sua caixa de correio, precisa de ativar a sua conta.";
                                lbl_mensagem.CssClass = "alert alert-success";
                            }
                            else
                            {
                                lbl_mensagem.Text = "Utilizador registado com sucesso. Porém houve um erro ao enviar o email de ativação, por favor fale com o suporte.";
                                lbl_mensagem.CssClass = "alert alert-info";
                            }

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                        }
                        else if (years > 121)
                        {
                            lbl_mensagem.Text = "Introduza uma idade válida até 121 anos, por favor.";
                            lbl_mensagem.CssClass = "alert alert-danger";

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                        }
                        else
                        {
                            lbl_mensagem.Text = "É necessário ter pelo menos 18 anos para se registar.";
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
                else if (!Validation.Check_Username(tb_username.Text))
                {
                    lbl_mensagem.Text = "Username já existe!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                    tb_username.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                }
                else if (!Validation.Check_Email(tb_email.Text))
                {
                    lbl_mensagem.Text = "Email já existe!";
                    lbl_mensagem.CssClass = "alert alert-danger";
                    tb_email.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
                }
                else
                {
                    lbl_mensagem.Text = "Username e email já existem.";
                    lbl_mensagem.CssClass = "alert alert-danger";
                    tb_username.Text = "";
                    tb_email.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
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