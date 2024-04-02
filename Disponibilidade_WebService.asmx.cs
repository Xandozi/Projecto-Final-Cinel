using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;

namespace Projeto_Final
{
    /// <summary>
    /// Summary description for Disponibilidade_WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Disponibilidade_WebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDisponibilidade_JSON(int cod_user)
        {
            List<FullCalendarData> lst_disponibilidade = GetDisponibilidade_DB(cod_user);

            string json = JsonConvert.SerializeObject(lst_disponibilidade, Formatting.None);

            return json;
        }

        public List<FullCalendarData> GetDisponibilidade_DB(int cod_user)
        {
            List<FullCalendarData> lst_disponibilidade = new List<FullCalendarData>();

            string query = $"select Disponibilidade.cod_timeslot, Disponibilidade.dataa, Disponibilidade.cod_user, Disponibilidade.available, Timeslots.hora_inicio, Timeslots.hora_fim " +
                           $"from Disponibilidade " +
                           $"join Timeslots on Timeslots.cod_timeslot = Disponibilidade.cod_timeslot " +
                           $"where Disponibilidade.cod_user = {cod_user} and Disponibilidade.available = 0";

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CinelConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myConn.Open();

                    using (SqlDataReader dr = myCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FullCalendarData eventData = new FullCalendarData();
                            eventData.Cod_Timeslot = !dr.IsDBNull(0) ? dr.GetInt32(0) : 000; // Set cod_timeslot
                            eventData.Data = !dr.IsDBNull(1) ? dr.GetDateTime(1) : default(DateTime); // Set dataa
                            eventData.TimeSlot_Inicio = !dr.IsDBNull(4) ? dr.GetTimeSpan(4) : default(TimeSpan);
                            eventData.TimeSlot_Fim = !dr.IsDBNull(5) ? dr.GetTimeSpan(5) : default(TimeSpan);

                            lst_disponibilidade.Add(eventData);
                        }
                    }
                }
            }

            return lst_disponibilidade;
        }
    }
}
