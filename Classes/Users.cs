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
        public List<string> perfis { get; set; }
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
                informacao.cod_user = dr.GetInt32(0);
                informacao.username = dr.GetString(1);
                informacao.email = dr.GetString(2);
                informacao.nome_proprio = dr.GetString(3);
                informacao.apelido = dr.GetString(4);
                informacao.morada = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.cod_postal = !dr.IsDBNull(6) ? dr.GetString(6) : null;
                informacao.data_nascimento = !dr.IsDBNull(7) ? dr.GetDateTime(7) : default(DateTime);
                informacao.num_contacto = !dr.IsDBNull(8) ? dr.GetString(8) : null;
                informacao.foto = !dr.IsDBNull(9) ? "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]) : null;

                lst_user.Add(informacao);
            }

            myConn.Close();
            return lst_user;
        }
    }
}