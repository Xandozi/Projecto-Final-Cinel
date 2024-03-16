using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Salas
    {
        public int cod_sala { get; set; }
        public string nome_sala { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime ultimo_update { get; set; }
        public bool ativo { get; set; }

        public static bool Inserir_Sala(string nome_sala, DateTime data_criacao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@nome_sala", nome_sala);
                myCommand.Parameters.AddWithValue("@data_criacao", data_criacao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Sala";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();
                myCommand.Parameters.Clear();

                return resposta_sp;
            }
        }

        public static List<Salas> Ler_SalasAll(string search_designacao, string data_inicio, string data_fim, int search_cod_sala, string sort_order, int estado)
        {
            List<Salas> lst_salas = new List<Salas>();

            List<string> conditions = new List<string>();

            string query = $"select cod_sala, nome_sala, data_criacao, ultimo_update, ativo from Salas";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(search_designacao))
            {
                conditions.Add($"nome_sala LIKE '%{search_designacao}%'");
            }
            if (data_inicio != null && data_fim != null)
            {
                conditions.Add($"data_criacao >= '{data_inicio}' and data_criacao <= '{data_fim}'");
            }
            if (search_cod_sala != 0)
            {
                conditions.Add($"cod_sala = {search_cod_sala}");
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
                query += " ORDER BY cod_sala " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Salas informacao = new Salas();
                informacao.cod_sala = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_sala = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.data_criacao = !dr.IsDBNull(2) ? dr.GetDateTime(2) : default(DateTime);
                informacao.ultimo_update = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.ativo = !dr.IsDBNull(4) ? dr.GetBoolean(4) : default(Boolean);

                lst_salas.Add(informacao);
            }

            myConn.Close();

            return lst_salas;
        }

        public static List<Salas> Ler_Sala(int cod_sala)
        {
            List<Salas> lst_sala = new List<Salas>();

            string query = $"select cod_sala, nome_sala, data_criacao, ultimo_update, ativo from Salas where cod_sala = {cod_sala}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Salas informacao = new Salas();
                informacao.cod_sala = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_sala = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.data_criacao = !dr.IsDBNull(2) ? dr.GetDateTime(2) : default(DateTime);
                informacao.ultimo_update = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.ativo = !dr.IsDBNull(4) ? dr.GetBoolean(4) : default(Boolean);

                lst_sala.Add(informacao);
            }

            myConn.Close();

            return lst_sala;
        }

        public static bool Editar_Sala(int cod_sala, string nome_sala, DateTime ultimo_update, bool ativo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_sala", cod_sala);
                myCommand.Parameters.AddWithValue("@nome_sala", nome_sala);
                myCommand.Parameters.AddWithValue("@ultimo_update", ultimo_update);
                myCommand.Parameters.AddWithValue("@ativo", ativo);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_Sala";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return resposta_sp;
            }
        }

        public static bool Delete_Sala(int cod_sala)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_sala", cod_sala);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Sala";

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