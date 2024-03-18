using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Email
    {
        // Função para enviar email de ativação ao utilizador que se registar.
        public static bool Send(string email_destino, string username)
        {
            try
            {
                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]);
                email.To.Add(new MailAddress(email_destino));
                email.Subject = $"CINEL - Ativação de Conta de {username}";

                email.IsBodyHtml = true;
                email.Body = $"<b>Obrigado pelo seu registo {username}. Para ativar a sua conta clique <a href='https://localhost:44374/ativacao.aspx?user={EncryptString(username)}'>aqui</a></b>";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_MailUser"];
                string pw = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, pw);
                servidor.EnableSsl = true;

                servidor.Send(email);

                // Email sent successfully
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Função para gerar e enviar uma palavra passe nova para o utilizador que pedir
        public static bool Send_Forgot_PW(string email_destino)
        {
            try
            {
                string password_encrypted = EncryptString(PasswordGen());
                string password = DecryptString(password_encrypted);
                Users.Update_Password(email_destino, password_encrypted);

                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]);
                email.To.Add(new MailAddress(email_destino));
                email.Subject = $"CINEL - Password Reset";

                email.IsBodyHtml = true;
                email.Body = $"Your new password is the following one: {password}";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_MailUser"];
                string pw = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, pw);
                servidor.EnableSsl = true;

                servidor.Send(email);

                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
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

        public static string DecryptString(string Message)
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

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]

            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");


            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

        public static string PasswordGen()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+";
            Random random = new Random();

            string password = "";

            password += validChars[random.Next(0, 52)];
            password += validChars[random.Next(52, 62)];
            password += validChars[random.Next(62, validChars.Length)];

            for (int i = 3; i < 9; i++)
            {
                password += validChars[random.Next(validChars.Length)];
            }

            password = new string(password.OrderBy(c => random.Next()).ToArray());

            return password;

        }
    }
}