using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Inscricoes
    {
        public int cod_inscricao { get; set; }
        public int cod_user { get; set; }
        public string nome_proprio { get; set; }
        public string apelido { get; set; }
        public int cod_qualificacao { get; set; }
        public string nome_curso { get; set; }
        public DateTime data_inscricao { get; set; }
        public int cod_formando { get; set; }
        public int cod_formador { get; set; }
        public int cod_situacao { get; set; }
        public string situacao { get; set; }
        public DateTime data_situacao { get; set; }

        public static int Insert_Inscricao(int tipo_inscricao, int cod_user, int cod_curso, DateTime data_inscricao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);
                myCommand.Parameters.AddWithValue("@data_inscricao", data_inscricao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                if(tipo_inscricao == 1)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Insert_Inscricao_Formador";
                }
                else if(tipo_inscricao == 2)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Insert_Inscricao_Formando";
                }

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }

        public static List<Inscricoes> GetFormadores_Por_Validar()
        {
            List<Inscricoes> lst_formadores_por_validar = new List<Inscricoes>();

            string query = "select Inscricoes.cod_inscricao, Users.cod_user, Users.nome_proprio, Users.apelido, Cursos.cod_qualificacao, Cursos.nome_curso, Inscricoes.dataa, Formadores.cod_formador from Inscricoes " +
                           "join Users on Users.cod_user = Inscricoes.cod_user " +
                           "join Cursos on Cursos.cod_curso = Inscricoes.cod_curso " +
                           "join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           "join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           "join Formadores on Formadores.cod_inscricao = Inscricoes.cod_inscricao " +
                           "where Inscricoes_Situacao.cod_situacao = 11";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Inscricoes informacao = new Inscricoes();
                informacao.cod_inscricao = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_user = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.cod_qualificacao = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.nome_curso = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.data_inscricao = !dr.IsDBNull(6) ? dr.GetDateTime(6) : default(DateTime);
                informacao.cod_formador = !dr.IsDBNull(7) ? dr.GetInt32(7) : 000;

                lst_formadores_por_validar.Add(informacao);
            }

            myConn.Close();

            return lst_formadores_por_validar;
        }

        public static List<Inscricoes> GetFormandos_Por_Validar()
        {
            List<Inscricoes> lst_formandos_por_validar = new List<Inscricoes>();

            string query = "select Inscricoes.cod_inscricao, Users.cod_user, Users.nome_proprio, Users.apelido, Cursos.cod_qualificacao, Cursos.nome_curso, Inscricoes.dataa, Formandos.cod_formando from Inscricoes " +
                           "join Users on Users.cod_user = Inscricoes.cod_user " +
                           "join Cursos on Cursos.cod_curso = Inscricoes.cod_curso " +
                           "join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           "join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           "join Formandos on Formandos.cod_inscricao = Inscricoes.cod_inscricao " +
                           "where Inscricoes_Situacao.cod_situacao = 5";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Inscricoes informacao = new Inscricoes();
                informacao.cod_inscricao = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_user = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.cod_qualificacao = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.nome_curso = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.data_inscricao = !dr.IsDBNull(6) ? dr.GetDateTime(6) : default(DateTime);
                informacao.cod_formando = !dr.IsDBNull(7) ? dr.GetInt32(7) : 000;

                lst_formandos_por_validar.Add(informacao);
            }

            myConn.Close();

            return lst_formandos_por_validar;
        }

        public static bool Validar_Inscricao(int cod_user, int cod_inscricao, int cod_situacao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@cod_inscricao", cod_inscricao);
                myCommand.Parameters.AddWithValue("@cod_situacao", cod_situacao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Validar_Inscricao";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }

        public static bool Revogar_Inscricao(int cod_user, int cod_inscricao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@cod_inscricao", cod_inscricao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Revogar_Inscricao";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }
    }
}