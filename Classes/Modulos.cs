﻿using System;
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
        // UNFINISHEDDDDDDDDDDDDDDDDDDDDDDDDDD
        public static List<Modulos> Ler_ModulosAll(string search_designacao, int search_duracao, DateTime data_inicio, DateTime data_fim, int search_cod_ufcd, string sort_order)
        {
            List<Modulos> lst_modulos = new List<Modulos>();

            List<string> conditions = new List<string>();

            string query = $"select cod_modulo, nome_modulo, duracao, data_criacao, cod_ufcd from Modulos";

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
                conditions.Add($"data_criacao >= {data_inicio} and data_criacao <= {data_fim}");
            }
            if (search_cod_ufcd != 0)
            {
                conditions.Add($"cod_ufcd = {search_cod_ufcd}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY moeda_estado.valor_atual " + sort_order;
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

                lst_modulos.Add(informacao);
            }

            myConn.Close();

            return lst_modulos;
        }

        public static List<Modulos> Ler_Modulo(int cod_ufcd)
        {
            List<Modulos> lst_modulo = new List<Modulos>();

            string query = $"select cod_modulo, nome_modulo, duracao, data_criacao, cod_ufcd from Modulos where cod_ufcd = {cod_ufcd}";

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

                lst_modulo.Add(informacao);
            }

            myConn.Close();

            return lst_modulo;
        }
    }
}