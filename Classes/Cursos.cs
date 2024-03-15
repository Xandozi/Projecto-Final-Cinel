using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Cursos
    {
        public int cod_curso { get; set; }
        public string nome_curso { get; set; }
        public int duracao_estagio { get; set; }
        public int cod_qualificacao { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime ultimo_update { get; set; }
        public int duracao_curso { get; set; }
        public bool ativo { get; set; }
        public List<string> modulos_curso { get; set; }
        public int cod_curso_modulo { get; set; }
        public int cod_modulo { get; set; }

        public static int Inserir_Curso(int cod_qualificacao, string nome_curso, int duracao_estagio, DateTime data_criacao, List<int> ufcds)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_qualificacao", cod_qualificacao);
                myCommand.Parameters.AddWithValue("@nome_curso", nome_curso);
                myCommand.Parameters.AddWithValue("@duracao_estagio", duracao_estagio);
                myCommand.Parameters.AddWithValue("@data_criacao", data_criacao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                SqlParameter cod_curso = new SqlParameter();
                cod_curso.ParameterName = "@cod_curso";
                cod_curso.Direction = ParameterDirection.Output;
                cod_curso.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(cod_curso);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Curso";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                int resposta_cod_curso = Convert.ToInt32(myCommand.Parameters["@cod_curso"].Value);

                myConn.Close();
                myCommand.Parameters.Clear();

                if (resposta_sp == 1)
                {
                    foreach (int cod_ufcd in ufcds)
                        Cursos.Inserir_Curso_Modulos(resposta_cod_curso, Modulos.Extract_Cod_Modulo_Via_Cod_UFCD(cod_ufcd));
                } 

                return resposta_sp;
            }
        }

        public static void Inserir_Curso_Modulos(int cod_curso, int cod_modulo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Curso_Modulo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                myCommand.Parameters.Clear();
            }
        }

        public static List<Cursos> Ler_CursosAll(string search_designacao, int search_duracao, string data_inicio, string data_fim, int search_cod_qualificacao, string sort_order, int estado)
        {
            List<Cursos> lst_cursos = new List<Cursos>();

            List<string> conditions = new List<string>();

            string query = $@"select Cursos.cod_curso, Cursos.nome_curso, Cursos.duracao_estagio, Cursos.data_criacao, Cursos.cod_qualificacao, Cursos.ultimo_update, Cursos.duracao_estagio + (select SUM(Modulos.duracao) from Modulos join Cursos_Modulos on Modulos.cod_modulo = Cursos_Modulos.cod_modulo where Cursos_Modulos.cod_curso = Cursos.cod_curso) as total_duracao, Cursos.ativo from Cursos";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(search_designacao))
            {
                conditions.Add($"Cursos.nome_curso LIKE '%{search_designacao}%'");
            }
            if (search_duracao != 0)
            {
                conditions.Add($"Cursos.duracao_estagio + (SELECT SUM(Modulos.duracao) FROM Modulos JOIN Cursos_Modulos ON Modulos.cod_modulo = Cursos_Modulos.cod_modulo WHERE Cursos_Modulos.cod_curso = Cursos.cod_curso) = {search_duracao}");
            }
            if (data_inicio != null && data_fim != null)
            {
                conditions.Add($"Cursos.data_criacao >= '{data_inicio}' and Cursos.data_criacao <= '{data_fim}'");
            }
            if (search_cod_qualificacao != 0)
            {
                conditions.Add($"Cursos.cod_qualificacao = {search_cod_qualificacao}");
            }
            if (estado == 0)
            {
                conditions.Add($"Cursos.ativo = {estado}");
            }
            else if (estado == 1)
            {
                conditions.Add($"Cursos.ativo = {estado}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY Cursos.cod_qualificacao " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Cursos informacao = new Cursos();
                informacao.cod_curso = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_curso = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.duracao_estagio = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.data_criacao = !dr.IsDBNull(3) ? dr.GetDateTime(3) : default(DateTime);
                informacao.cod_qualificacao = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.ultimo_update = !dr.IsDBNull(5) ? dr.GetDateTime(5) : default(DateTime);
                informacao.duracao_curso = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                informacao.ativo = !dr.IsDBNull(7) ? dr.GetBoolean(7) : default(Boolean);

                lst_cursos.Add(informacao);
            }

            myConn.Close();

            return lst_cursos;
        }

        public static List<Cursos> Ler_Curso(int cod_curso)
        {
            List<Cursos> lst_curso = new List<Cursos>();

            string query = $"select Cursos.cod_curso, Cursos.nome_curso, Cursos.duracao_estagio + (select SUM(Modulos.duracao) from Modulos join Cursos_Modulos on Modulos.cod_modulo = Cursos_Modulos.cod_modulo where Cursos_Modulos.cod_curso = Cursos.cod_curso) as total_duracao, Cursos.duracao_estagio, Cursos.data_criacao, Cursos.cod_qualificacao, Cursos.ultimo_update, Cursos.ativo from Cursos where Cursos.cod_curso = {cod_curso}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Cursos informacao = new Cursos();
                informacao.cod_curso = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_curso = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.duracao_curso = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.duracao_estagio = !dr.IsDBNull(3) ? dr.GetInt32(3) : 000;
                informacao.data_criacao = !dr.IsDBNull(4) ? dr.GetDateTime(4) : default(DateTime);
                informacao.cod_qualificacao = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;
                informacao.ultimo_update = !dr.IsDBNull(6) ? dr.GetDateTime(6) : default(DateTime);
                informacao.modulos_curso = Cursos.Ler_Curso_UFCD(dr.GetInt32(0));
                informacao.ativo = !dr.IsDBNull(7) ? dr.GetBoolean(7) : default(Boolean);

                lst_curso.Add(informacao);
            }

            myConn.Close();

            return lst_curso;
        }

        public static List<string> Ler_Curso_UFCD(int cod_curso)
        {
            List<Modulos> lst_modulos_curso = new List<Modulos>();

            string query = $"select Modulos.cod_ufcd, Modulos.nome_modulo from Modulos join Cursos_Modulos on Cursos_Modulos.cod_modulo = Modulos.cod_modulo where Cursos_Modulos.cod_curso = {cod_curso}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Modulos informacao = new Modulos();
                informacao.cod_ufcd = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;

                lst_modulos_curso.Add(informacao);
            }

            List<string> lst_modulos_curso_tostring = new List<string>();

            foreach (Modulos modulo in lst_modulos_curso)
            {
                string info_modulo = modulo.cod_ufcd + " - " + modulo.nome_modulo;
                lst_modulos_curso_tostring.Add(info_modulo);
            }

            myConn.Close();

            return lst_modulos_curso_tostring;
        }

        public static int Editar_Curso(int cod_curso, string nome_curso, int duracao_estagio, int cod_qualificacao, DateTime ultimo_update, List<int> ufcds, bool ativo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);
                myCommand.Parameters.AddWithValue("@nome_curso", nome_curso);
                myCommand.Parameters.AddWithValue("@duracao_estagio", duracao_estagio);
                myCommand.Parameters.AddWithValue("@cod_qualificacao", cod_qualificacao);
                myCommand.Parameters.AddWithValue("@ultimo_update", ultimo_update);
                myCommand.Parameters.AddWithValue("@ativo", ativo);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Editar_Curso";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();
                myCommand.Parameters.Clear();

                if (resposta_sp == 1)
                {
                    foreach (int cod_ufcd in ufcds)
                        Cursos.Inserir_Curso_Modulos(cod_curso, Modulos.Extract_Cod_Modulo_Via_Cod_UFCD(cod_ufcd));
                }

                return resposta_sp;
            }
        }

        public static bool Delete_Curso(int cod_curso)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Curso";

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