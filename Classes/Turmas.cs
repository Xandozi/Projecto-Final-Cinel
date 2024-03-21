using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Turmas
    {
        public int cod_turma { get; set; }
        public string nome_turma { get; set; }
        public int cod_curso { get; set; }
        public int nome_curso { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public int cod_regime { get; set; }
        public string regime { get; set; }
        public int cod_turmas_estado { get; set; }
        public string turma_estado { get; set; }
        public List<Formandos> formandos { get; set; }
        public List<Formadores> formadores { get; set; }

        public static void Inserir_Turma(int cod_curso, DateTime data_inicio, int cod_regime, List<Formandos> formandos, List<Formadores_Modulos> formadores_modulos)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);
                myCommand.Parameters.AddWithValue("@data_inicio", data_inicio);
                myCommand.Parameters.AddWithValue("@cod_regime", cod_regime);

                SqlParameter cod_turma = new SqlParameter();
                cod_turma.ParameterName = "@cod_turma";
                cod_turma.Direction = ParameterDirection.Output;
                cod_turma.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(cod_turma);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Turma";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@cod_turma"].Value);
                myConn.Close();
                myCommand.Parameters.Clear();

                foreach (Formandos formando in formandos)
                    Turmas.Inserir_Formandos_Turma(formando.cod_formando, resposta_sp, formando.cod_inscricao);

                foreach (Formadores_Modulos formador in formadores_modulos)
                    Turmas.Inserir_Formadores_Turma_Modulo(formador.cod_formador, formador.cod_modulo, resposta_sp);
            }
        }

        public static void Inserir_Formandos_Turma(int cod_formando, int cod_turma, int cod_inscricao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formando", cod_formando);
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);
                myCommand.Parameters.AddWithValue("@cod_inscricao", cod_inscricao);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Formando_Turma";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                myCommand.Parameters.Clear();
            }
        }

        public static void Inserir_Formadores_Turma_Modulo(int cod_formador, int cod_modulo, int cod_turma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formador", cod_formador);
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Formador_Turma_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                myCommand.Parameters.Clear();
            }
        }
    }
}