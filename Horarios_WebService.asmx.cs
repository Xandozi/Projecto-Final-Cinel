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
                           $"SELECT cod_timeslot, dataa, cod_mod_tur_for, cod_turma, cod_modulo, cod_formador, cod_sala, hora_inicio, hora_fim, titulo, color, ROW_NUMBER() OVER (ORDER BY dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Horarios.cod_timeslot, Horarios.dataa, Horarios.cod_mod_tur_for, Horarios.cod_sala, Horarios.titulo, Horarios.color, Timeslots.hora_inicio, Timeslots.hora_fim, Turmas.cod_turma, Modulos.cod_modulo, Formadores.cod_formador " +
                           $"FROM Horarios " +
                           $"join Salas on Salas.cod_sala = Horarios.cod_sala " +
                           $"join Timeslots on Timeslots.cod_timeslot = Horarios.cod_timeslot " +
                           $"join Modulos_Turmas_Formadores on Modulos_Turmas_Formadores.cod_mod_tur_for = Horarios.cod_mod_tur_for " +
                           $"join Turmas on Turmas.cod_turma = Modulos_Turmas_Formadores.cod_turma " +
                           $"join Modulos on Modulos.cod_modulo = Modulos_Turmas_Formadores.cod_modulo " +
                           $"join Formadores on Formadores.cod_formador = Modulos_Turmas_Formadores.cod_formador " +
                           $"where Turmas.cod_turma = {cod_turma}) AS Horario) " +
                           $"SELECT MIN(hora_inicio) AS start_time, MAX(hora_fim) AS end_time, dataa, titulo, color, cod_modulo, cod_formador, cod_sala FROM ContiguousSlots GROUP BY dataa, DATEADD(hour, -rn, hora_inicio), titulo, color, cod_modulo, cod_formador, cod_sala " +
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
                            eventData.color = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                            eventData.cod_modulo = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;
                            eventData.cod_formador = !dr.IsDBNull(6) ? dr.GetInt32(6) : 000;
                            eventData.cod_sala = !dr.IsDBNull(7) ? dr.GetInt32(7) : 000;

                            lst_horario.Add(eventData);
                        }
                    }
                }
            }

            return lst_horario;
        }

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
                           $"SELECT cod_timeslot, dataa, cod_user, available, hora_inicio, hora_fim, titulo, color, ROW_NUMBER() OVER (ORDER BY dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Disponibilidade.cod_timeslot, Disponibilidade.dataa, Disponibilidade.cod_user, Disponibilidade.available, Timeslots.hora_inicio, Timeslots.hora_fim, Disponibilidade.titulo, Disponibilidade.color " +
                           $"FROM Disponibilidade JOIN Timeslots ON Timeslots.cod_timeslot = Disponibilidade.cod_timeslot WHERE Disponibilidade.cod_user = {cod_user} AND Disponibilidade.available = 0) AS Availability) " +
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
                            eventData.color = !dr.IsDBNull(4) ? dr.GetString(4) : null;

                            lst_disponibilidade.Add(eventData);
                        }
                    }
                }
            }

            return lst_disponibilidade;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDisponibilidade_Sala_JSON(int cod_sala)
        {
            List<FullCalendarData> lst_disponibilidade_sala = GetDisponibilidade_Sala_DB(cod_sala);

            string json = JsonConvert.SerializeObject(lst_disponibilidade_sala, Formatting.None);

            return json;
        }

        public List<FullCalendarData> GetDisponibilidade_Sala_DB(int cod_sala)
        {
            List<FullCalendarData> lst_disponibilidade_sala = new List<FullCalendarData>();

            string query = $"WITH ContiguousSlots AS (" +
                           $"SELECT cod_timeslot, dataa, cod_sala, available, hora_inicio, hora_fim, titulo, color, ROW_NUMBER() OVER (ORDER BY dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Disponibilidade_Salas.cod_timeslot, Disponibilidade_Salas.dataa, Disponibilidade_Salas.cod_sala, Disponibilidade_Salas.available, Timeslots.hora_inicio, Timeslots.hora_fim, Disponibilidade_Salas.titulo, Disponibilidade_Salas.color " +
                           $"FROM Disponibilidade_Salas JOIN Timeslots ON Timeslots.cod_timeslot = Disponibilidade_Salas.cod_timeslot WHERE Disponibilidade_Salas.cod_sala = {cod_sala} AND Disponibilidade_Salas.available = 0) AS Availability) " +
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
                            eventData.color = !dr.IsDBNull(4) ? dr.GetString(4) : null;

                            lst_disponibilidade_sala.Add(eventData);
                        }
                    }
                }
            }

            return lst_disponibilidade_sala;
        }
    }
}
