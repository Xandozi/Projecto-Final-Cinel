using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Formadores_Modulos
    {
        public int cod_user { get; set; }
        public int cod_formador { get; set; }
        public int cod_modulo { get; set; }
        public int cod_ufcd { get; set; }
        public int ordem { get; set; }
        public int cod_turma { get; set; }
        public string nome_completo { get; set; }
        public string nome_modulo { get; set; }
        public string formador_modulo { get { return $"{nome_completo} | {cod_ufcd} | {nome_modulo}"; } }
        public string cod_formador_modulo { get { return $"{cod_formador}|{cod_modulo}"; } }

        public static List<Formadores_Modulos> Ler_Formadores_Modulos_Turma(int cod_turma)
        {
            List<Formadores_Modulos> lst_formadores_modulos = new List<Formadores_Modulos>();

            string query = $"select distinct Modulos.cod_modulo, Modulos.nome_modulo, Modulos.cod_ufcd, Users.cod_user, Cursos_Modulos.ordem, Formadores.cod_formador, Users.nome_proprio, Users.apelido from Modulos " +
                           $"join Modulos_Turmas_Formadores on Modulos_Turmas_Formadores.cod_modulo = Modulos.cod_modulo " +
                           $"join Cursos_Modulos on Cursos_Modulos.cod_modulo = Modulos.cod_modulo " +
                           $"join Cursos on Cursos.cod_curso = Cursos_Modulos.cod_curso " +
                           $"join Formadores on Formadores.cod_formador = Modulos_Turmas_Formadores.cod_formador " +
                           $"join Inscricoes on Inscricoes.cod_inscricao = Formadores.cod_inscricao " +
                           $"join Users on Users.cod_user = Inscricoes.cod_user " +
                           $"join Turmas on Turmas.cod_turma = Modulos_Turmas_Formadores.cod_turma " +
                           $"where Turmas.cod_turma = {cod_turma} " +
                           $"order by Cursos_Modulos.ordem asc";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Formadores_Modulos informacao = new Formadores_Modulos();
                informacao.cod_modulo = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000;
                informacao.nome_modulo = !dr.IsDBNull(1) ? dr.GetString(1) : null;
                informacao.cod_ufcd = !dr.IsDBNull(2) ? dr.GetInt32(2) : 000;
                informacao.cod_user = !dr.IsDBNull(3) ? dr.GetInt32(3) : 000;
                informacao.ordem = !dr.IsDBNull(4) ? dr.GetInt32(4) : 000;
                informacao.cod_formador = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;
                informacao.nome_completo = (!dr.IsDBNull(6) ? dr.GetString(6) : null) + " " + (!dr.IsDBNull(7) ? dr.GetString(7) : null);

                lst_formadores_modulos.Add(informacao);
            }

            myConn.Close();

            return lst_formadores_modulos;
        }
    }
}