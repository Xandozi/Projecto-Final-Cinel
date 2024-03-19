using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Formadores
    {
        public int cod_formador { get; set; }
        public string nome_proprio { get; set; }
        public string apelido { get; set; }
        public int cod_inscricao { get; set; }
        public int cod_modulo { get; set; }
        public int cod_turma { get; set; }

        public static bool Inserir_Formador(int cod_formador, int cod_inscricao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formador", cod_formador);
                myCommand.Parameters.AddWithValue("@cod_inscricao", cod_inscricao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Formador";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }

        public static List<Formadores> Ler_FormadoresAll()
        {
            List<Formadores> lst_formadores = new List<Formadores>();

            string query = $"select Formadores.cod_formador, Formadores.cod_inscricao, Users.nome_proprio, Users.apelido from Formadores " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formadores.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formadores informacao = new Formadores();
                informacao.cod_formador = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;

                lst_formadores.Add(informacao);
            }

            myConn.Close();

            return lst_formadores;
        }
    }
}