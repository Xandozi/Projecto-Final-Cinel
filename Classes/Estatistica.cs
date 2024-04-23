using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Estatistica
    {
        public string Area { get; set; }
        public int TotalCursos { get; set; }
        public int TotalCursosTerminados()
        {
            int totalTerminados = 0;
            string query = "select count(*) from Turmas where data_fim < getdate()";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalTerminados = (int)command.ExecuteScalar();
                }
            }

            return totalTerminados;
        }

        public int TotalCursosDecorrer()
        {
            int totalDecorrer = 0;
            string query = "SELECT COUNT(*) FROM Turmas WHERE data_inicio <= GETDATE() AND data_fim >= GETDATE(); ";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalDecorrer = (int)command.ExecuteScalar();
                }
            }

            return totalDecorrer;
        }

        public int TotalFormandosEmTurmas()
        {
            int totalFormandos = 0;
            string query = "SELECT COUNT(f.cod_formando) FROM Formandos f INNER JOIN Turmas_Formandos tf on f.cod_formando=tf.cod_formando INNER JOIN Turmas t on t.cod_turma=tf.cod_turma WHERE data_fim >= GETDATE(); ";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalFormandos = (int)command.ExecuteScalar();
                }
            }

            return totalFormandos;
        }

        public static List<Estatistica> TotalCursosPorArea()
        {
            List<Estatistica> total_cursos_area = new List<Estatistica>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                string query = @"SELECT A.area AS Areas, COUNT(C.cod_curso) AS TotalCursos FROM Areas A JOIN Cursos C ON A.cod_area = C.cod_area GROUP BY A.area, A.cod_area;";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Estatistica statistics = new Estatistica
                        {
                            Area = reader["Areas"].ToString(),
                            TotalCursos = Convert.ToInt32(reader["TotalCursos"])
                        };

                        total_cursos_area.Add(statistics);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return total_cursos_area;
        }

        public static List<Formadores> GetTop10Formadores()
        {
            List<Formadores> formadores = new List<Formadores>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP 10 f.cod_formador, u.nome_proprio, u.apelido FROM Users u INNER JOIN Inscricoes i ON u.cod_user = i.cod_user INNER JOIN Formadores f ON i.cod_inscricao = f.cod_inscricao INNER JOIN Modulos_Turmas_Formadores ftm ON ftm.cod_formador = f.cod_formador INNER JOIN Horarios h ON ftm.cod_mod_tur_for = h.cod_mod_tur_for ORDER BY f.cod_formador; ", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Formadores formador = new Formadores();
                    formador.cod_formador = reader.GetInt32(0);
                    formador.nome_completo = reader.GetString(1) + " " + reader.GetString(2);
                    formadores.Add(formador);
                }
            }
            return formadores;
        }
    }
}