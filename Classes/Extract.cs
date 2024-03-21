using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Extract
    {
        // Função para extrair o cod_user através do username
        public static int Code(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@cod_user";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "User_Cod";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int cod_user = Convert.ToInt32(myCommand.Parameters["@cod_user"].Value);

                myConn.Close();

                return cod_user;
            }
        }

        // Função para extrair o cod_user através do email
        public static int Code_Via_Email(string email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@email", email);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@cod_user";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "User_Cod_Via_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int cod_user = Convert.ToInt32(myCommand.Parameters["@cod_user"].Value);

                myConn.Close();

                return cod_user;
            }
        }

        // Função para extrair o email do user através do seu username
        public static string Email(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@email";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.VarChar;
                resposta_SP.Size = 100;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "User_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                string email = myCommand.Parameters["@Email"].Value.ToString();

                myConn.Close();

                return email;
            }
        }

        // Função para extrair o username do cod_user em questão
        public static string Username(int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@username";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.VarChar;
                resposta_SP.Size = 50;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "User_Username";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                string username = myCommand.Parameters["@username"].Value.ToString();

                myConn.Close();

                return username;
            }
        }

        public static string Nome_Completo(int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@nome_proprio";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.VarChar;
                resposta_SP.Size = 50;
                myCommand.Parameters.Add(resposta_SP);

                SqlParameter resposta_SP1 = new SqlParameter();
                resposta_SP1.ParameterName = "@apelido";
                resposta_SP1.Direction = ParameterDirection.Output;
                resposta_SP1.SqlDbType = SqlDbType.VarChar;
                resposta_SP1.Size = 50;
                myCommand.Parameters.Add(resposta_SP1);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "User_Nome_Completo";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                string nome = myCommand.Parameters["@nome_proprio"].Value.ToString() + " " + myCommand.Parameters["@apelido"].Value.ToString();

                myConn.Close();

                return nome;
            }
        }

        public static int Cod_Inscricao_Formando(int cod_formando)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_formando", cod_formando);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@cod_inscricao";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Extract_Cod_Inscricao_Formando";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int cod_inscricao = Convert.ToInt32(myCommand.Parameters["@cod_inscricao"].Value);

                myConn.Close();

                return cod_inscricao;
            }
        }
    }
}