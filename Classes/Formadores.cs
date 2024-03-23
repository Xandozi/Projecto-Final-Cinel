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
        public string nome_completo { get; set; }
        public int cod_inscricao { get; set; }
        public int cod_modulo { get; set; }
        public string nome_modulo { get; set; }
        public int cod_turma { get; set; }
        public string nome_turma { get; set; }
        public int cod_curso { get; set; }
        public string nome_curso { get; set; }
        public DateTime data_inscricao { get; set; }

        public static bool Inserir_Formador_Turma_Modulo(int cod_formador, int cod_modulo, int cod_turma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formador", cod_formador);
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Formador_Turma_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }

        public static List<Formadores> Ler_FormadoresAll(int cod_curso)
        {
            List<Formadores> lst_formadores = new List<Formadores>();

            string query = $"select Formadores.cod_formador, Formadores.cod_inscricao, Users.nome_proprio, Users.apelido from Formadores " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formadores.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"where Inscricoes.cod_curso = {cod_curso}";

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
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;

                lst_formadores.Add(informacao);
            }

            myConn.Close();

            return lst_formadores;
        }

        public static List<Formadores> Ler_Inscricoes_Formador(int cod_user)
        {
            List<Formadores> lst_formador = new List<Formadores>();

            string query = $"select Formadores.cod_formador, Formadores.cod_inscricao, Users.nome_proprio, Users.apelido, Modulos_Turmas_Formadores.cod_turma, Turmas.nome_turma, Turmas.cod_curso, Cursos.nome_curso, Inscricoes.dataa, Modulos_Turmas_Formadores.cod_modulo, Modulos.nome_modulo from Formadores " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formadores.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           $"join Modulos_Turmas_Formadores on Modulos_Turmas_Formadores.cod_formador = Formadores.cod_formador " +
                           $"join Modulos on Modulos.cod_modulo = Modulos_Turmas_Formadores.cod_modulo " +
                           $"join Turmas on Turmas.cod_turma = Modulos_Turmas_Formadores.cod_turma " +
                           $"join Cursos on Cursos.cod_curso = Turmas.cod_curso " +
                           $"where Inscricoes.cod_user = {cod_user}";

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
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;
                informacao.cod_turma = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.nome_turma = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.cod_curso = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                informacao.nome_curso = !dr.IsDBNull(7) ? dr.GetString(7) : null;
                informacao.data_inscricao = !dr.IsDBNull(8) ? dr.GetDateTime(8) : default(DateTime);
                informacao.cod_modulo = !dr.IsDBNull(9) ? dr.GetInt32(9) : 000;
                informacao.nome_modulo = !dr.IsDBNull(10) ? dr.GetString(10) : null;

                lst_formador.Add(informacao);
            }

            myConn.Close();

            return lst_formador;
        }
    }
}