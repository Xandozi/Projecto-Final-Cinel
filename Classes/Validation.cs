using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Validation
    {
        public string perfil { get; set; }
        public int cod_perfil { get; set; }

        // Função para verificar se o username existe na base de dados e que retorna true ou false
        public static bool Check_Username(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Username";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para verificar se o email existe na base de dados e retornar true ou false
        public static bool Check_Email(string email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@email", email);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para verificar se o user está ativo ou não e retornar true ou false
        public static bool Check_Active(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Active";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para ativar ou desativar o utilizador de acordo com o que estiver na BD
        public static void Ativacao(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@username", username);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "User_Activation";

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();
        }

        // Função para verificar se as credenciais de login estão corretas ou não e assim retornar um int de acordo com as opções
        public static int Check_Login(string username, string password)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@password", EncryptString(password));

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Login";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido;
            }
        }

        // Função para verificar o perfil do utilizador e retornar em string o mesmo
        public static List<Validation> Check_Perfil(int cod_user)
        {
            List<Validation> lst_perfil = new List<Validation>();

            string query = $"select cod_perfil, perfil from Users_Perfis where cod_user = {cod_user}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Validation informacao = new Validation();
                informacao.cod_perfil = dr.GetInt32(0);
                informacao.perfil = dr.GetString(1);

                lst_perfil.Add(informacao);
            }
            myConn.Close();
            return lst_perfil;
        }

        // Função para verificar se o utilizador tem o perfil de administrador na BD
        public static bool Check_IsStaff(string username)
        {
            List<Validation> lst_perfil = Validation.Check_Perfil(Extract.Code(username));

            foreach (Validation perfil in lst_perfil)
            {
                if (perfil.perfil == "Staff")
                {
                    return true;
                }
            }

            return false;
        }
        public static bool Check_IsFormando(string username)
        {
            List<Validation> lst_perfil = Validation.Check_Perfil(Extract.Code(username));

            foreach (Validation perfil in lst_perfil)
            {
                if (perfil.perfil == "Formando")
                {
                    return true;
                }
            }

            return false;
        }
        public static bool Check_IsFormador(string username)
        {
            List<Validation> lst_perfil = Validation.Check_Perfil(Extract.Code(username));

            foreach (Validation perfil in lst_perfil)
            {
                if (perfil.perfil == "Formador")
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Check_IsSuperAdmin(string username)
        {
            List<Validation> lst_perfil = Validation.Check_Perfil(Extract.Code(username));

            foreach (Validation perfil in lst_perfil)
            {
                if (perfil.perfil == "Super Admin")
                {
                    return true;
                }
            }

            return false;
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
    }
}