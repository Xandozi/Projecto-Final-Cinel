using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Horarios
    {
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fim { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }

        public static void Insert_Disponibilidade_Formador(int cod_user, int cod_timeslot, string data, string titulo)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@cod_timeslot", cod_timeslot);
                myCommand.Parameters.AddWithValue("@data", data);
                myCommand.Parameters.AddWithValue("@titulo", titulo);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Disponibilidade_Formador";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void Delete_Disponibilidade_Formador(int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Disponibilidade_Formador";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static int Check_Timeslot(DateTime hora_inicio, DateTime hora_fim)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@hora_inicio", hora_inicio.TimeOfDay);
                myCommand.Parameters.AddWithValue("@hora_fim", hora_fim.TimeOfDay);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@cod_timeslot";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Timeslot";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int cod_timeslot = Convert.ToInt32(myCommand.Parameters["@cod_timeslot"].Value);

                myConn.Close();

                return cod_timeslot;
            }
        }

        public static void Insert_Horario_Turma(int cod_turma, int cod_modulo, int cod_formador, int cod_sala, int cod_timeslot, string data, string titulo, string color)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);
                myCommand.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                myCommand.Parameters.AddWithValue("@cod_formador", cod_formador);
                myCommand.Parameters.AddWithValue("@cod_sala", cod_sala);
                myCommand.Parameters.AddWithValue("@cod_timeslot", cod_timeslot);
                myCommand.Parameters.AddWithValue("@data", data);
                myCommand.Parameters.AddWithValue("@titulo", titulo);
                myCommand.Parameters.AddWithValue("@color", color);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Horario_Turma";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void Delete_Horario_Turma(int cod_turma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Horario_Turma";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void Delete_Disponibilidade_Formador_Turma(int cod_turma, int cod_formador)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);
                myCommand.Parameters.AddWithValue("@cod_formador", cod_formador);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Disponibilidade_Formador_Turma";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void Delete_Disponibilidade_Sala_Turma(int cod_turma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_turma", cod_turma);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Disponibilidade_Sala_Turma";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }
    }
}