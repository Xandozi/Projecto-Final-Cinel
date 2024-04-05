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
        public int cod_user { get; set; }
        public int cod_formador { get; set; }
        public string nome_proprio { get; set; }
        public string apelido { get; set; }
        public string nome_completo { get; set; }
        public int cod_inscricao { get; set; }
        public int cod_modulo { get; set; }
        public string nome_modulo { get; set; }
        public int cod_ufcd { get; set; }
        public int cod_turma { get; set; }
        public string nome_turma { get; set; }
        public int cod_curso { get; set; }
        public string nome_curso { get; set; }
        public DateTime data_inscricao { get; set; }
        public string situacao { get; set; }
        public string regime { get; set; }

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

        public static List<Formadores> Ler_Formadores(string nome_turma, string nome_formando, int cod_curso, int cod_regime, int cod_modulos, string ordenacao_nome_turma, string ordenacao_nome_formando, int cod_inscricao_situacao)
        {
            List<Formadores> lst_formadores = new List<Formadores>();

            List<string> conditions = new List<string>();

            conditions.Add($"Users_Perfis.cod_perfil = 3");

            string query = $"select Formadores.cod_formador, Formadores.cod_inscricao, Users.nome_proprio, Users.apelido, Turmas.nome_turma, Cursos.nome_curso, Situacao.situacao, Users.cod_user, Regime.regime, Modulos.cod_modulo, Modulos.nome_modulo, Modulos.cod_ufcd from Formadores " +
                           $"join Modulos_Turmas_Formadores on Modulos_Turmas_Formadores.cod_formador = Formadores.cod_formador " +
                           $"join Modulos on Modulos.cod_modulo = Modulos_Turmas_Formadores.cod_modulo " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formadores.cod_inscricao " +
                           $"join Cursos on Cursos.cod_curso = Inscricoes.cod_curso " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Users_Perfis on Users_Perfis.cod_user = Users.cod_user " +
                           $"join Inscricoes_Situacao on Inscricoes_Situacao.cod_inscricao = Inscricoes.cod_inscricao " +
                           $"join Situacao on Situacao.cod_situacao = Inscricoes_Situacao.cod_situacao " +
                           $"join Turmas on Turmas.cod_turma = Modulos_Turmas_Formadores.cod_turma " +
                           $"join Regime on Regime.cod_regime = Turmas.cod_regime ";

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
            if (cod_modulos != 0)
            {
                conditions.Add($"Modulos.cod_modulo = {cod_modulos}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (ordenacao_nome_turma != "Nenhuma" && ordenacao_nome_formando == "Nenhuma")
            {
                query += " ORDER BY Turmas.nome_turma " + ordenacao_nome_turma;
            }
            if (ordenacao_nome_turma == "Nenhuma" && ordenacao_nome_formando != "Nenhuma")
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
                Formadores informacao = new Formadores();
                informacao.cod_formador = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.cod_inscricao = !dr.IsDBNull(1) ? dr.GetInt32(1) : 000;
                informacao.nome_proprio = !dr.IsDBNull(2) ? dr.GetString(2) : null;
                informacao.apelido = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                informacao.nome_completo = informacao.nome_proprio + " " + informacao.apelido;
                informacao.nome_turma = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                informacao.nome_curso = !dr.IsDBNull(5) ? dr.GetString(5) : null;
                informacao.situacao = !dr.IsDBNull(6) ? dr.GetString(6) : null;
                informacao.cod_user = !dr.IsDBNull(7) ? dr.GetInt32(7) : 000;
                informacao.regime = !dr.IsDBNull(8) ? dr.GetString(8) : null;
                informacao.cod_modulo = !dr.IsDBNull(9) ? dr.GetInt32(9) : 000;
                informacao.nome_modulo = !dr.IsDBNull(10) ? dr.GetString(10) : null;
                informacao.cod_ufcd = !dr.IsDBNull(11) ? dr.GetInt32(11) : 000;

                lst_formadores.Add(informacao);
            }

            myConn.Close();

            return lst_formadores;
        }
    }
}