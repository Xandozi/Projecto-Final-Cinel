using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Formandos
    {
        public int cod_inscricao { get; set; }
        public int cod_formando { get; set; }
        public int cod_user { get; set; }
        public string nome_proprio { get; set; }
        public string apelido { get; set; }
        public string nome_completo { get; set; }
        public int cod_turma { get; set; }
        public string nome_turma { get; set; }
        public int cod_curso { get; set; }
        public string nome_curso { get; set; }
        public DateTime data_inscricao { get; set; }
        public string situacao { get; set; }
        public string regime { get; set; }

        public static bool Inserir_Formando_Turma(int cod_formando, int cod_turma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formando", cod_formando);
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Formando_Turma";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                bool resposta_sp = Convert.ToBoolean(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }

        public static List<Formandos> Ler_FormandosAll(int cod_curso)
        {
            List<Formandos> lst_formandos = new List<Formandos>();

            string query = $"select Formandos.cod_formando, Formandos.cod_inscricao, Users.nome_proprio, Users.apelido from Formandos " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formandos.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           $"where Inscricoes_Situacao.cod_situacao = 6 and Inscricoes.cod_curso = {cod_curso} " +
                           $"and not exists (select 1 from Inscricoes_Situacao " +
                                            $"where Inscricoes_Situacao.cod_user = Inscricoes.cod_user " +
                                            $"and Inscricoes_Situacao.cod_situacao = 1)";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formandos informacao = new Formandos();
                informacao.cod_formando = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;

                lst_formandos.Add(informacao);
            }

            myConn.Close();

            return lst_formandos;
        }

        public static List<Formandos> Ler_Inscricoes_Formando(int cod_user)
        {
            List<Formandos> lst_formando = new List<Formandos>();

            string query = $"select Formandos.cod_formando, Formandos.cod_inscricao, Users.nome_proprio, Users.apelido, Turmas_Formandos.cod_turma, Turmas.nome_turma, Turmas.cod_curso, Cursos.nome_curso, Inscricoes.dataa from Formandos " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formandos.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           $"join Turmas_Formandos on Turmas_Formandos.cod_formando = Formandos.cod_formando " +
                           $"join Turmas on Turmas.cod_turma = Turmas_Formandos.cod_turma " +
                           $"join Cursos on Cursos.cod_curso = Turmas.cod_curso " +
                           $"where Inscricoes.cod_user = {cod_user}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formandos informacao = new Formandos();
                informacao.cod_formando = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;
                informacao.cod_turma = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.nome_turma = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.cod_curso = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                informacao.nome_curso = !dr.IsDBNull(7) ? dr.GetString(7) : null;
                informacao.data_inscricao = !dr.IsDBNull(8) ? dr.GetDateTime(8) : default(DateTime);

                lst_formando.Add(informacao);
            }

            myConn.Close();

            return lst_formando;
        }

        public static List<Formandos> Ler_Formandos_Turma(int cod_turma)
        {
            List<Formandos> lst_formandos = new List<Formandos>();

            string query = $"select Formandos.cod_formando, Formandos.cod_inscricao, Users.nome_proprio, Users.apelido from Formandos " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formandos.cod_inscricao " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Turmas_Formandos on Turmas_Formandos.cod_formando = Formandos.cod_formando " +
                           $"where Turmas_Formandos.cod_turma = {cod_turma} and Inscricoes_Situacao.cod_situacao = 1";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formandos informacao = new Formandos();
                informacao.cod_formando = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;

                lst_formandos.Add(informacao);
            }

            myConn.Close();

            return lst_formandos;
        }

        public static void Alterar_Estado_Formando(int cod_formando)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formando", cod_formando);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Edit_Inscricoes_Situacao_Formando";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static List<Formandos> Ler_Formandos(string nome_turma, string nome_formando, int cod_curso, int cod_regime, string ordenacao_nome_turma, string ordenacao_nome_formando, int cod_inscricao_situacao)
        {
            List<Formandos> lst_formandos = new List<Formandos>();

            List<string> conditions = new List<string>();

            string query = $"select Formandos.cod_formando, Formandos.cod_inscricao, Users.nome_proprio, Users.apelido, Turmas.nome_turma, Cursos.nome_curso, Situacao.situacao, Users.cod_user, Regime.regime from Formandos " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formandos.cod_inscricao " +
                           $"join Cursos on Cursos.cod_curso = Inscricoes.cod_curso " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Users_Perfis on Users_Perfis.cod_user = Users.cod_user " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           $"join Turmas_Formandos on Turmas_Formandos.cod_formando = Formandos.cod_formando " +
                           $"join Turmas on Turmas.cod_turma = Turmas_Formandos.cod_turma " +
                           $"join Regime on Regime.cod_regime = Turmas.cod_regime " +
                           $"where Users_Perfis.cod_perfil = 4";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(nome_turma))
            {
                conditions.Add($"Turmas.nome_turma LIKE '%{nome_turma}%'");
            }
            if (!string.IsNullOrEmpty(nome_formando))
            {
                conditions.Add($"Users.nome_proprio LIKE '%{nome_formando}%' or Users.apelido LIKE '%{nome_formando}%'");
            }
            if (cod_curso != 0)
            {
                conditions.Add($"Cursos.cod_curso = {cod_curso}");
            }
            if (cod_regime != 0)
            {
                conditions.Add($"Turmas.cod_regime = {cod_regime}");
            }
            if (cod_inscricao_situacao != 0)
            {
                conditions.Add($"Inscricoes_Situacao.cod_situacao = {cod_inscricao_situacao}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (ordenacao_nome_turma != "Nenhuma" && string.IsNullOrEmpty(ordenacao_nome_formando))
            {
                query += " ORDER BY Turmas.nome_turma " + ordenacao_nome_turma;
            }
            if (string.IsNullOrEmpty(ordenacao_nome_turma) && ordenacao_nome_formando != "Nenhuma")
            {
                query += " ORDER BY Users.nome_proprio " + ordenacao_nome_formando;
            }
            if (ordenacao_nome_turma != "Nenhuma" && ordenacao_nome_formando != "Nenhuma")
            {
                query += " ORDER BY Users.nome_proprio " + ordenacao_nome_formando + ", Turmas.nome_turma " + ordenacao_nome_turma;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formandos informacao = new Formandos();
                informacao.cod_formando = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;
                informacao.nome_turma = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                informacao.nome_curso = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.situacao = !dr.IsDBNull(6) ? dr.GetString(6) : null;
                informacao.cod_user = !dr.IsDBNull(7) ? dr.GetInt32(7) : 000;
                informacao.regime = !dr.IsDBNull(8) ? dr.GetString(8) : null;

                lst_formandos.Add(informacao);
            }

            myConn.Close();

            return lst_formandos;
        }
    }
}