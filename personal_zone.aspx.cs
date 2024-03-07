using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class personal_zone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }

            if (Request.QueryString["msg"] == "yesuser")
            {
                lblMessage.Text = "Username changed successfully!";
                lblMessage.CssClass = "alert alert-success";
                lbl_message2.Text = "Username changed successfully!";
                lbl_message2.CssClass = "alert alert-success";
            }
        }

        protected void btn_export_pdf_Click(object sender, EventArgs e)
        {
            //int cod_user = Extract.Code(Session["username"].ToString());
            //string filename = $"{EncryptString(DateTime.Now.ToString())}" + $"{EncryptString(Session["username"].ToString())}CoinCollection.pdf";

            //// Criar uma instância da classe ExportPDF
            //ExportPDF pdf = new ExportPDF();

            //// Gerar o PDF de acordo com o array de byte proveniente da função ExportPDF.MyCollection
            //byte[] pdfBytes = pdf.MyCollection(cod_user);

            //Response.Clear();

            //// Selecionar o tipo de ficheiro para PDF
            //Response.ContentType = "application/pdf";

            //// Indicar ao browser que o ficheiro é um downloadable file
            //Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");

            //// Escrever o array de byte para dentro do stream response
            //Response.BinaryWrite(pdfBytes);

            //Response.End();
        }

        protected void btn_delete_account_confirm_Click(object sender, EventArgs e)
        {

        }

        protected void btn_change_pw_Click(object sender, EventArgs e)
        {
            // Condição para mudar a password do utilizador
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw.Text) == 1 && tb_new_pw.Text == tb_new_pw_repeat.Text && tb_new_pw.Text != tb_pw.Text && tb_new_pw_repeat.Text != tb_pw.Text)
            {
                Insercao.Update_Password(Extract.Email(Session["username"].ToString()), Validation.EncryptString(tb_new_pw.Text));
                lblMessage.Text = "Password changed successfully!";
                lblMessage.CssClass = "alert alert-success";
                lbl_message2.Text = "Password changed successfully!";
                lbl_message2.CssClass = "alert alert-success";
            }
            else
            {
                lblMessage.Text = "Error changing password. Please try again.";
                lblMessage.CssClass = "alert alert-danger";
                lbl_message2.Text = "Error changing password. Please try again.";
                lbl_message2.CssClass = "alert alert-danger";
            }
        }

        protected void btn_change_username_Click(object sender, EventArgs e)
        {
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw_username.Text) == 1 && Session["username"].ToString() != tb_new_username.Text)
            {
                Insercao.Update_Username(Session["username"].ToString(), tb_new_username.Text);
                Session["username"] = tb_new_username.Text;
                Response.Redirect("personal_zone.aspx?msg=yesuser", false);
            }
            else
            {
                lblMessage.Text = "Error changing the username. Please try again.";
                lblMessage.CssClass = "alert alert-danger";
                lbl_message2.Text = "Error changing the username. Please try again.";
                lbl_message2.CssClass = "alert alert-danger";
            }
        }

        protected void btn_change_email_Click(object sender, EventArgs e)
        {
            if (Validation.Check_Login(Session["username"].ToString(), tb_pw_email.Text) == 1 && Session["email"].ToString() != tb_new_email.Text)
            {
                Insercao.Update_Email(Session["email"].ToString(), tb_new_email.Text);
                Session["email"] = tb_new_email.Text;
                Email.Send(tb_new_email.Text, Session["username"].ToString());
                Response.Redirect("logout.aspx?msg=yesemail", false);
            }
            else
            {
                lblMessage.Text = "Error changing the email. Please try again.";
                lblMessage.CssClass = "alert alert-danger";
                lbl_message2.Text = "Error changing the email. Please try again.";
                lbl_message2.CssClass = "alert alert-danger";
            }
        }
        public static string EncryptString(string Message)
        {
            string Passphrase = "batatascomarroz";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string

            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }

        protected void btn_logout2_Click(object sender, EventArgs e)
        {
            //Response.Redirect("logout.aspx");
            // Variáveis de sessão limpas ao clicar em logout
            Session.Clear();
            Session.Abandon();
            // Redirecionamento caso provenha de depois de mudar o email
            if (Request.QueryString["msg"] == "yesemail")
            {
                Response.Redirect("login.aspx?msg=yesemail");
            }
            Response.Redirect("login.aspx");
        }
    }
}