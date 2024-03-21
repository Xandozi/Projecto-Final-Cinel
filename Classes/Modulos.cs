using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Modulos
    {
        public int cod_modulo { get; set; }
        public string nome_modulo { get; set; }
        public int duracao { get; set; }
        public DateTime data_criacao { get; set; }
        public int cod_ufcd { get; set; }
        public DateTime ultimo_update { get; set; }
        public bool ativo { get; set; }
        public string ufcd_nome_modulo { get { return $"{cod_ufcd} | {nome_modulo}"; } }

        public static int Inserir_Modulo(int cod_ufcd, string nome_modulo, int duracao, DateTime data_criacao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_ufcd", cod_ufcd);
                myCommand.Parameters.AddWithValue("@nome_modulo", nome_modulo);
                myCommand.Parameters.AddWithValue("@duracao", duracao);
                myCommand.Parameters.AddWithValue("@data_criacao", data_criacao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return resposta_sp;
            }
        }

        public static List<Modulos> Ler_ModulosAll(string search_designacao, int search_duracao, string data_inicio, string data_fim, int search_cod_ufcd, string sort_order, int estado)
        {
            List<Modulos> lst_modulos = new List<Modulos>();

            List<string> conditions = new List<string>();

            string query = $"select cod_modulo, nome_modulo, duracao, data_criacao, cod_ufcd, ultimo_update, ativo from Modulos";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(search_designacao))
            {
                conditions.Add($"nome_modulo LIKE '%{search_designacao}%'");
            }
            if (search_duracao != 0)
            {
                conditions.Add($"duracao = {search_duracao}");
            }
            if (data_inicio != null && data_fim != null)
            {
                conditions.Add($"data_criacao >= '{data_inicio}' and data_criacao <= '{data_fim}'");
            }
            if (search_cod_ufcd != 0)
            {
                conditions.Add($"cod_ufcd = {search_cod_ufcd}");
            }
            if (estado == 0)
            {
                conditions.Add($"ativo = {estado}");
            }
            else if (estado == 1)
            {
                conditions.Add($"ativo = {estado}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY cod_ufcd " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Modulos informacao = new Modulos();
                informacao.cod_modulo = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.duracao = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.data_criacao = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.cod_ufcd = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.ultimo_update = !dr.IsDBNull(5) ? dr.GetDateTime(5) : default(DateTime);
                informacao.ativo = !dr.IsDBNull(6) ? dr.GetBoolean(6) : default(Boolean);

                lst_modulos.Add(informacao);
            }

            myConn.Close();

            return lst_modulos;
        }

        public static List<Modulos> Ler_ModulosAll_Curso(int cod_curso)
        {
            List<Modulos> lst_modulos = new List<Modulos>();

            string query = $"select Modulos.cod_modulo, Modulos.nome_modulo, Modulos.cod_ufcd, Modulos.duracao, Modulos.ativo from Modulos " +
                           $"join Cursos_Modulos on Cursos_Modulos.cod_modulo = Modulos.cod_modulo " +
                           $"where Cursos_Modulos.cod_curso = {cod_curso} and Modulos.ativo = 1";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Modulos informacao = new Modulos();
                informacao.cod_modulo = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.cod_ufcd = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.duracao = !dr.IsDBNull(3) ? dr.GetInt32(3) : 000;
                informacao.ativo = !dr.IsDBNull(4) ? dr.GetBoolean(4) : default(Boolean);

                lst_modulos.Add(informacao);
            }

            myConn.Close();

            return lst_modulos;
        }

        public static List<Modulos> Ler_Modulo(int cod_modulo)
        {
            List<Modulos> lst_modulo = new List<Modulos>();

            string query = $"select cod_modulo, nome_modulo, duracao, data_criacao, cod_ufcd, ultimo_update, ativo from Modulos where cod_modulo = {cod_modulo}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Modulos informacao = new Modulos();
                informacao.cod_modulo = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.duracao = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.data_criacao = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.cod_ufcd = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.ultimo_update = !dr.IsDBNull(5) ? dr.GetDateTime(5) : default(DateTime);
                informacao.ativo = !dr.IsDBNull(6) ? dr.GetBoolean(6) : default(Boolean);

                lst_modulo.Add(informacao);
            }

            myConn.Close();

            return lst_modulo;
        }

        public static string Extract_CodUFCD_Nome_Modulo(int cod_ufcd)
        {
            List<Modulos> lst_info_modulo = new List<Modulos>();

            string query = $"select cod_ufcd, nome_modulo from Modulos where cod_ufcd = {cod_ufcd}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Modulos informacao = new Modulos();
                informacao.cod_ufcd = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;

                lst_info_modulo.Add(informacao);
            }

            string info_modulo = lst_info_modulo[0].cod_ufcd + " - " + lst_info_modulo[0].nome_modulo;

            myConn.Close();

            return info_modulo;
        }

        public static int Check_ifExists_Modulo(int cod_ufcd)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_ufcd", cod_ufcd);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_ifExists_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return resposta_sp;
            }
        }

        public static int Check_Duracao_Modulo(int cod_modulo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);

                SqlParameter duracao = new SqlParameter();
                duracao.ParameterName = "@duracao";
                duracao.Direction = ParameterDirection.Output;
                duracao.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(duracao);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Duracao_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@duracao"].Value);
                myConn.Close();

                return resposta_sp;
            }
        }

        public static int Extract_Cod_Modulo_Via_Cod_UFCD(int cod_ufcd)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_ufcd", cod_ufcd);

                SqlParameter cod_modulo = new SqlParameter();
                cod_modulo.ParameterName = "@cod_modulo";
                cod_modulo.Direction = ParameterDirection.Output;
                cod_modulo.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(cod_modulo);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Extract_Cod_Modulo_Via_Cod_UFCD";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@cod_modulo"].Value);
                myConn.Close();

                myCommand.Parameters.Clear();

                return resposta_sp;
            }
        }

        public static int Editar_Modulo(int cod_modulo, string nome_modulo, int duracao, int cod_ufcd, DateTime ultimo_update, bool ativo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                myCommand.Parameters.AddWithValue("@nome_modulo", nome_modulo);
                myCommand.Parameters.AddWithValue("@duracao", duracao);
                myCommand.Parameters.AddWithValue("@cod_ufcd", cod_ufcd);
                myCommand.Parameters.AddWithValue("@ultimo_update", ultimo_update);
                myCommand.Parameters.AddWithValue("@ativo", ativo);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return resposta_sp;
            }
        }

        public static bool Delete_Modulo(int cod_modulo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Modulo";

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