using Newtonsoft.Json;
using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Projeto_Final
{
    /// <summary>
    /// Summary description for Horarios_WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Horarios_WebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHorarios_JSON(int cod_turma)
        {
            List<FullCalendarData> lst_horario = GetHorarios_DB(cod_turma);

            string json = JsonConvert.SerializeObject(lst_horario, Formatting.None);

            return json;
        }

        public List<FullCalendarData> GetHorarios_DB(int cod_turma)
        {
            List<FullCalendarData> lst_horario = new List<FullCalendarData>();

            string query = $"WITH ContiguousSlots AS (" +
                           $"SELECT cod_timeslot, dataa, cod_mod_tur_for, cod_turma, cod_modulo, cod_formador, hora_inicio, hora_fim, titulo, color, ROW_NUMBER() OVER (ORDER BY dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Horarios.cod_timeslot, Horarios.dataa, Horarios.cod_mod_tur_for, Horarios.cod_sala, Horarios.titulo, Horarios.color, Timeslots.hora_inicio, Timeslots.hora_fim, Turmas.cod_turma, Modulos.cod_modulo, Formadores.cod_formador " +
                           $"FROM Horarios " +
                           $"JOIN Timeslots ON Timeslots.cod_timeslot = Horarios.cod_timeslot " +
                           $"join Modulos_Turmas_Formadores on Modulos_Turmas_Formadores.cod_mod_tur_for = Horarios.cod_mod_tur_for " +
                           $"join Turmas on Turmas.cod_turma = Modulos_Turmas_Formadores.cod_turma " +
                           $"join Modulos on Modulos.cod_modulo = Modulos_Turmas_Formadores.cod_modulo " +
                           $"join Formadores on Formadores.cod_formador = Modulos_Turmas_Formadores.cod_formador " +
                           $"WHERE Turmas.cod_turma = {cod_turma}) AS Horario) " +
                           $"SELECT MIN(hora_inicio) AS start_time, MAX(hora_fim) AS end_time, dataa, titulo, color FROM ContiguousSlots GROUP BY dataa, DATEADD(hour, -rn, hora_inicio), titulo, color " +
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

                            lst_horario.Add(eventData);
                        }
                    }
                }
            }

            return lst_horario;
        }
    }
}
