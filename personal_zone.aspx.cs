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
            else
            {
                List<Users> user = Users.Ler_Info_User(Convert.ToInt32(Session["cod_user"]));

                if (user.Count > 0)
                {
                    lbl_cod_user.Text = user[0].cod_user.ToString();
                    lbl_cod_user.Font.Bold = true;

                    lbl_username.Text = Session["username"].ToString();
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

                    lbl_data_nascimento.Text = user[0].data_nascimento.ToShortDateString();
                    lbl_data_nascimento.Font.Bold = true;

                    lbl_num_contacto.Text = user[0].num_contacto;
                    lbl_num_contacto.Font.Bold = true;

                    img_user.ImageUrl = user[0].foto;
                }
            }

            if (Request.QueryString["msg"] == "yesuser")
            {
                lbl_mensagem.Text = "Username mudado com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
            }
            if (Request.QueryString["msg"] == "yesedit")
            {
                lbl_mensagem.Text = "Informações alteradas com sucesso!";
                lbl_mensagem.CssClass = "alert alert-success";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "fadeAlert", "$('.alert').delay(5000).fadeOut('slow');", true);
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
            Response.Redirect("logout.aspx", false);
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"personal_zone_editar.aspx?cod_user={Session["cod_user"]}");
        }
    }
}