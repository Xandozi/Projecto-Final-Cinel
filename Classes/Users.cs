using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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

        // Função para ler a informação de um certo utilizador
        public static List<Users> Ler_Info_User(int cod_user)
        {
            List<Users> lst_user = new List<Users>();

            string query = $"select users.cod_user, users.username, users.email, users.nome_proprio, users.apelido, users.morada, users.cod_postal, users.data_nascimento, users.num_contacto, users.foto from users where cod_user = {cod_user}";

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
    }
}