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
        public int cod_qualificacao { get; set; }
        public string nome_curso { get; set; }
        public int duracao_curso { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public int cod_regime { get; set; }
        public string regime { get; set; }
        public int cod_turmas_estado { get; set; }
        public string estado { get; set; }
        public List<Formandos> formandos { get; set; }
        public List<Formadores_Modulos> formadores_modulos { get; set; }

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

        public static List<Turmas> Ler_TurmasAll(string nome_turma, int cod_qualificacao, int cod_curso, int cod_regime, string inicio_data_inicio, string fim_data_inicio, string inicio_data_fim, string fim_data_fim, int cod_area, string ordenacao_nome_turma, string ordenacao_cod_qualificacao, string duracao, int cod_turma_estado)
        {
            List<Turmas> lst_turmas = new List<Turmas>();

            List<string> conditions = new List<string>();

            string query = $"select Turmas.cod_turma, Turmas.cod_curso, Turmas.nome_turma, Turmas.data_inicio, Turmas.data_fim, Cursos.cod_qualificacao, Turmas.cod_regime, Regime.Regime, " +
                           $"Cursos.duracao_estagio + (select SUM(Modulos.duracao) from Modulos join Cursos_Modulos on Modulos.cod_modulo = Cursos_Modulos.cod_modulo where Cursos_Modulos.cod_curso = Cursos.cod_curso) as total_duracao, " +
                           $"Turmas.cod_turmas_estado, Turmas_Estado.turma_estado, Cursos.nome_curso from Turmas " +
                           $"join Regime on Regime.cod_regime = Turmas.cod_regime " +
                           $"join Turmas_Estado on Turmas_Estado.cod_turmas_estado = Turmas.cod_turmas_estado " +
                           $"join Cursos on Cursos.cod_curso = Turmas.cod_curso ";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(nome_turma))
            {
                conditions.Add($"Turmas.nome_turma LIKE '%{nome_turma}%'");
            }
            if (cod_qualificacao != 0)
            {
                conditions.Add($"Cursos.cod_qualificacao = {cod_qualificacao}");
            }
            if (cod_curso != 0)
            {
                conditions.Add($"Cursos.cod_curso = {cod_curso}");
            }
            if (cod_regime != 0)
            {
                conditions.Add($"Turmas.cod_regime = {cod_regime}");
            }
            if (inicio_data_inicio != null && fim_data_inicio != null)
            {
                conditions.Add($"Turmas.data_inicio >= '{inicio_data_inicio}' and Turmas.data_inicio <= '{fim_data_inicio}'");
            }
            if (inicio_data_fim != null && fim_data_fim != null)
            {
                conditions.Add($"Turmas.data_fim >= '{inicio_data_fim}' and Turmas.data_fim <= '{fim_data_fim}'");
            }
            if (cod_area != 0)
            {
                conditions.Add($"Cursos.cod_area = {cod_area}");
            }
            if (duracao != "Todas")
            {
                if (duracao == "curta")
                    conditions.Add($"Cursos.duracao_estagio + (SELECT SUM(Modulos.duracao) FROM Modulos JOIN Cursos_Modulos ON Modulos.cod_modulo = Cursos_Modulos.cod_modulo WHERE Cursos_Modulos.cod_curso = Cursos.cod_curso) <= 300");
                else
                    conditions.Add($"Cursos.duracao_estagio + (SELECT SUM(Modulos.duracao) FROM Modulos JOIN Cursos_Modulos ON Modulos.cod_modulo = Cursos_Modulos.cod_modulo WHERE Cursos_Modulos.cod_curso = Cursos.cod_curso) > 300");
            }
            if (cod_turma_estado != 0)
            {
                conditions.Add($"Turmas.cod_turmas_estado = {cod_turma_estado}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (ordenacao_nome_turma != "Nenhuma" && string.IsNullOrEmpty(ordenacao_cod_qualificacao))
            {
                query += " ORDER BY Turmas.nome_turma " + ordenacao_nome_turma;
            }
            if (string.IsNullOrEmpty(ordenacao_nome_turma) && ordenacao_cod_qualificacao != "Nenhuma")
            {
                query += " ORDER BY Cursos.cod_qualificacao " + ordenacao_cod_qualificacao;
            }
            if (ordenacao_nome_turma != "Nenhuma" && ordenacao_cod_qualificacao != "Nenhuma")
            {
                query += " ORDER BY Cursos.cod_qualificacao " + ordenacao_cod_qualificacao + ", Turmas.nome_turma " + ordenacao_nome_turma;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Turmas informacao = new Turmas();
                informacao.cod_turma = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_curso = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_turma = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.data_inicio = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.data_fim = !dr.IsDBNull(4) ? dr.GetDateTime(4) : default(DateTime);
                informacao.cod_qualificacao = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;
                informacao.cod_regime = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                informacao.regime = !dr.IsDBNull(7) ? dr.GetString(7) : null;
                informacao.duracao_curso = !dr.IsDBNull(8) ? dr.GetInt32(8) : 000;
                informacao.cod_turmas_estado = !dr.IsDBNull(9) ? dr.GetInt32(9) : 000;
                informacao.estado = !dr.IsDBNull(10) ? dr.GetString(10) : null;
                informacao.nome_curso = !dr.IsDBNull(11) ? dr.GetString(11) : null;

                lst_turmas.Add(informacao);
            }

            myConn.Close();

            return lst_turmas;
        }

        public static List<Turmas> Ler_Turma(int cod_turma)
        {
            List<Turmas> lst_turma = new List<Turmas>();

            string query = $"select Turmas.cod_turma, Turmas.cod_curso, Turmas.nome_turma, Turmas.data_inicio, Turmas.data_fim, Cursos.cod_qualificacao, Turmas.cod_regime, Regime.regime, " +
                $"Cursos.duracao_estagio + (select SUM(Modulos.duracao) from Modulos join Cursos_Modulos on Modulos.cod_modulo = Cursos_Modulos.cod_modulo where Cursos_Modulos.cod_curso = Cursos.cod_curso) as total_duracao, " +
                $"Turmas.cod_turmas_estado, Turmas_Estado.turma_estado, Cursos.nome_curso from Turmas " +
                $"join Turmas_Estado on Turmas_Estado.cod_turmas_estado = Turmas.cod_turmas_estado " +
                $"join Cursos on Cursos.cod_curso = Turmas.cod_curso " +
                $"join Regime on Regime.cod_regime = Turmas.cod_regime " +
                $"where Turmas.cod_turma = {cod_turma}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Turmas informacao = new Turmas();
                informacao.cod_turma = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_curso = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_turma = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.data_inicio = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.data_fim = !dr.IsDBNull(4) ? dr.GetDateTime(4) : default(DateTime);
                informacao.cod_qualificacao = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;
                informacao.cod_regime = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                informacao.regime = !dr.IsDBNull(7) ? dr.GetString(7) : null;
                informacao.duracao_curso = !dr.IsDBNull(8) ? dr.GetInt32(8) : 000;
                informacao.cod_turmas_estado = !dr.IsDBNull(9) ? dr.GetInt32(9) : 000;
                informacao.estado = !dr.IsDBNull(10) ? dr.GetString(10) : null;
                informacao.nome_curso = !dr.IsDBNull(11) ? dr.GetString(11) : null;
                informacao.formadores_modulos = Formadores_Modulos.Ler_Formadores_Modulos_Turma(cod_turma);
                informacao.formandos = Formandos.Ler_Formandos_Turma(cod_turma);

                lst_turma.Add(informacao);
            }

            myConn.Close();

            return lst_turma;
        }

        public static void Check_Estado_Turmas()
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Turmas_Estado";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public static void Editar_Turma(int cod_turma, int cod_regime, int cod_turmas_estado)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);
                myCommand.Parameters.AddWithValue("@cod_regime", cod_regime);
                myCommand.Parameters.AddWithValue("@cod_turmas_estado", cod_turmas_estado);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_Turma";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                myCommand.Parameters.Clear();
            }
        }
    }
}