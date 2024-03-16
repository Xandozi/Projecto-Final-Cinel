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
    public class Users
    {
        public int cod_user { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string nome_proprio { get; set; }
        public string apelido { get; set; }
        public string morada { get; set; }
        public string cod_postal { get; set; }
        public DateTime data_nascimento { get; set; }
        public string num_contacto { get; set; }
        public string foto { get; set; }
        public bool ativo { get; set; }
        public string perfis { get; set; }
        public List<byte[]> ficheiros { get; set; }

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
                valido.SqlDbType = SqlDbType.Int;
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

        public static void Editar_User(int cod_user, string nome_proprio, string apelido, string morada, string cod_postal, DateTime data_nascimento, string num_contacto, bool ativo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@nome_proprio", nome_proprio);
                myCommand.Parameters.AddWithValue("@apelido", apelido);
                myCommand.Parameters.AddWithValue("@morada", morada);
                myCommand.Parameters.AddWithValue("@cod_postal", cod_postal);
                myCommand.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                myCommand.Parameters.AddWithValue("@num_contacto", num_contacto);
                myCommand.Parameters.AddWithValue("@ativo", ativo);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_User";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public static void Editar_User_Foto(int cod_user, byte[] foto)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@foto", foto);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_User_Foto";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
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

        public static List<Users> Ler_UsersAll(string search_designacao, int search_perfil, string search_email, string data_inicio, string data_fim, int search_cod_user, string sort_order, int estado)
        {
            List<Users> lst_users = new List<Users>();

            List<string> conditions = new List<string>();

            string query = $"select users.cod_user, users.username, users.email, users.nome_proprio, users.apelido, users.morada, users.cod_postal, users.data_nascimento, users.num_contacto, users.foto, Users.ativo from users";

            if (search_perfil != 0)
            {
                query = $"select users.cod_user, users.username, users.email, users.nome_proprio, users.apelido, users.morada, users.cod_postal, users.data_nascimento, users.num_contacto, users.foto, Perfis.perfil, Users.ativo from users join Users_Perfis on Users_Perfis.cod_user = Users.cod_user join Perfis on Perfis.cod_perfil = Users_Perfis.cod_perfil";
                conditions.Add($"Perfis.cod_perfil = {search_perfil}");
            }

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(search_designacao))
            {
                conditions.Add($"Users.username LIKE '%{search_designacao}%'");
            }
            if (data_inicio != null && data_fim != null)
            {
                conditions.Add($"Users.data_nascimento >= '{data_inicio}' and Users.data_nascimento <= '{data_fim}'");
            }
            if (search_cod_user != 0)
            {
                conditions.Add($"Users.cod_user = {search_cod_user}");
            }
            if (estado == 0)
            {
                conditions.Add($"Users.ativo = {estado}");
            }
            else if (estado == 1)
            {
                conditions.Add($"Users.ativo = {estado}");
            }
            if (!string.IsNullOrEmpty(search_email))
            {
                conditions.Add($"Users.email LIKE '%{search_email}%'");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY Users.username " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Users informacao = new Users();
                informacao.cod_user = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.username = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.email = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.nome_proprio = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.apelido = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                informacao.morada = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.cod_postal = !dr.IsDBNull(6) ? dr.GetString(6) : null;
                informacao.data_nascimento = !dr.IsDBNull(7) ? dr.GetDateTime(7) : default(DateTime);
                informacao.num_contacto = !dr.IsDBNull(8) ? dr.GetString(8) : null;
                informacao.foto = !dr.IsDBNull(9) ? "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]) : null;
                informacao.perfis = Users.Get_Perfis(dr.GetInt32(0));
                if (search_perfil == 0)
                    informacao.ativo = !dr.IsDBNull(10) ? dr.GetBoolean(10) : default(Boolean);
                else
                    informacao.ativo = !dr.IsDBNull(11) ? dr.GetBoolean(11) : default(Boolean);

                lst_users.Add(informacao);
            }

            myConn.Close();

            return lst_users;
        }
        // Função para ler a informação de um certo utilizador
        public static List<Users> Ler_Info_User(int cod_user)
        {
            List<Users> lst_user = new List<Users>();

            string query = $"select users.cod_user, users.username, users.email, users.nome_proprio, users.apelido, users.morada, users.cod_postal, users.data_nascimento, users.num_contacto, users.foto, Users.ativo from users where cod_user = {cod_user}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Users informacao = new Users();
                informacao.cod_user = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.username = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.email = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.nome_proprio = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.apelido = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                informacao.morada = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.cod_postal = !dr.IsDBNull(6) ? dr.GetString(6) : null;
                informacao.data_nascimento = !dr.IsDBNull(7) ? dr.GetDateTime(7) : default(DateTime);
                informacao.num_contacto = !dr.IsDBNull(8) ? dr.GetString(8) : null;
                informacao.foto = !dr.IsDBNull(9) ? "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]) : null;
                informacao.perfis = Users.Get_Perfis(dr.GetInt32(0));
                informacao.ativo = !dr.IsDBNull(10) ? dr.GetBoolean(10) : default(Boolean);

                lst_user.Add(informacao);
            }

            myConn.Close();

            return lst_user;
        }

        public static string Get_Perfis(int cod_user)
        {
            List<string> perfis = new List<string>();

            string query = $"select Perfis.perfil from Perfis join Users_Perfis on Users_Perfis.cod_perfil=Perfis.cod_perfil where Users_Perfis.cod_user = {cod_user}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                string perfil = dr.GetString(0);
                perfis.Add(perfil);
            }

            string string_perfis = string.Join(", ", perfis);

            return string_perfis;
        }

        public static bool Check_IsUserInformationComplete(int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_User_Information";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return resposta_sp;
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