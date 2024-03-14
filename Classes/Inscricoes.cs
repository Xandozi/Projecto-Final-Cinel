using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Inscricoes
    {
        public int cod_inscricao { get; set; }
        public int cod_user { get; set; }
        public int cod_curso { get; set; }
        public DateTime data_inscricao { get; set; }
        public int cod_formando { get; set; }
        public int cod_formador { get; set; }
        public int cod_situacao { get; set; }
        public string situacao { get; set; }
        public DateTime data_situacao { get; set; }

        public static int Insert_Inscricao(int tipo_inscricao, int cod_user, int cod_curso, DateTime data_inscricao)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@cod_curso", cod_curso);
                myCommand.Parameters.AddWithValue("@data_inscricao", data_inscricao);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valido);

                if(tipo_inscricao == 1)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Insert_Inscricao_Formador";
                }
                else if(tipo_inscricao == 2)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Insert_Inscricao_Formando";
                }

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int resposta_sp = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return resposta_sp;
            }
        }
    }
}