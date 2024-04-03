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

            string query = $"WITH ContiguousSlots AS (" +
                           $"SELECT cod_timeslot, dataa, cod_user, available, hora_inicio, hora_fim, titulo, ROW_NUMBER() OVER (ORDER BY dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Disponibilidade.cod_timeslot, Disponibilidade.dataa, Disponibilidade.cod_user, Disponibilidade.available, Timeslots.hora_inicio, Timeslots.hora_fim, Disponibilidade.titulo " +
                           $"FROM Disponibilidade JOIN Timeslots ON Timeslots.cod_timeslot = Disponibilidade.cod_timeslot WHERE Disponibilidade.cod_user = {cod_user} AND Disponibilidade.available = 0) AS Availability) " +
                           $"SELECT MIN(hora_inicio) AS start_time, MAX(hora_fim) AS end_time, dataa, titulo FROM ContiguousSlots GROUP BY dataa, DATEADD(hour, -rn, hora_inicio), titulo " +
                           $"ORDER BY dataa, start_time;";


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
                            eventData.Data = !dr.IsDBNull(2) ? dr.GetDateTime(2) : default(DateTime); // Set dataa
                            eventData.TimeSlot_Inicio = !dr.IsDBNull(0) ? dr.GetTimeSpan(0) : default(TimeSpan);
                            eventData.TimeSlot_Fim = !dr.IsDBNull(1) ? dr.GetTimeSpan(1) : default(TimeSpan);
                            eventData.title = !dr.IsDBNull(3) ? dr.GetString(3) : null;

                            lst_disponibilidade.Add(eventData);
                        }
                    }
                }
            }

            return lst_disponibilidade;
        }
    }
}
