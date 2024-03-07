using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Insercao
    {
        // Função para inserir o user na BD
        public static void Inserir_User(string username, string password, string email, string nome_proprio, string apelido, DateTime data_nascimento)
        {
            string imagePath = HttpContext.Current.Server.MapPath("~\\img\\noimage.png");
            byte[] foto = File.ReadAllBytes(imagePath);

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@password", EncryptString(password));
                myCommand.Parameters.AddWithValue("@email", email);
                myCommand.Parameters.AddWithValue("@nome_proprio", nome_proprio);
                myCommand.Parameters.AddWithValue("@apelido", apelido);
                myCommand.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                myCommand.Parameters.AddWithValue("@foto", foto);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_User";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para inserir o user que faz registo/login através do Google
        public static int Inserir_User_Google(string username, string email, string nome_proprio, string apelido)
        {
            string imagePath = HttpContext.Current.Server.MapPath("~\\img\\noimage.png");
            byte[] foto = File.ReadAllBytes(imagePath);

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@email", email);
                myCommand.Parameters.AddWithValue("@nome_proprio", nome_proprio);
                myCommand.Parameters.AddWithValue("@apelido", apelido);
                myCommand.Parameters.AddWithValue("@foto", foto);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_User_Google";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int valido_SP = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return valido_SP;
            }
        }
        public static void Update_Username(string old_username, string new_username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@new_username", new_username);
                myCommand.Parameters.AddWithValue("@old_username", old_username);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Username";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para dar update ao Email
        public static void Update_Email(string old_email, string new_email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@new_email", new_email);
                myCommand.Parameters.AddWithValue("@old_email", old_email);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para dar update á password
        public static void Update_Password(string email, string password)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@email", email);
                myCommand.Parameters.AddWithValue("@password", password);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Password";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public static void Update_User(int cod_user, int status, int deleted)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@status", status);

                if (deleted == 0)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Update_User";
                }
                else
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Delete_User";
                }

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
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
    }
}