using Newtonsoft.Json;
using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
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
                           $"SELECT cod_timeslot, dataa, cod_user, available, hora_inicio, hora_fim, titulo, color, cod_turma, ROW_NUMBER() OVER (ORDER BY titulo, dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Disponibilidade.cod_timeslot, Disponibilidade.dataa, Disponibilidade.cod_user, Disponibilidade.available, Timeslots.hora_inicio, Timeslots.hora_fim, Disponibilidade.titulo, Disponibilidade.color, Disponibilidade.cod_turma " +
                           $"FROM Disponibilidade JOIN Timeslots ON Timeslots.cod_timeslot = Disponibilidade.cod_timeslot WHERE Disponibilidade.cod_user = {cod_user} AND Disponibilidade.available = 0) AS Availability) " +
                           $"SELECT MIN(hora_inicio) AS start_time, MAX(hora_fim) AS end_time, dataa, titulo, color, cod_turma FROM ContiguousSlots GROUP BY dataa, DATEADD(hour, -rn, hora_inicio), titulo, color, cod_turma " +
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
                            eventData.Data = !dr.IsDBNull(2) ? dr.GetDateTime(2) : default(DateTime);
                            eventData.TimeSlot_Inicio = !dr.IsDBNull(0) ? dr.GetTimeSpan(0) : default(TimeSpan);
                            eventData.TimeSlot_Fim = !dr.IsDBNull(1) ? dr.GetTimeSpan(1) : default(TimeSpan);
                            eventData.title = !dr.IsDBNull(3) ? dr.GetString(3) : null;
                            eventData.color = !dr.IsDBNull(4) ? dr.GetString(4) : null;
                            eventData.cod_turma = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;

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
                           $"SELECT cod_timeslot, dataa, cod_sala, available, hora_inicio, hora_fim, titulo, color, cod_turma, ROW_NUMBER() OVER (ORDER BY titulo, dataa, hora_inicio) AS rn " +
                           $"FROM (SELECT Disponibilidade_Salas.cod_timeslot, Disponibilidade_Salas.dataa, Disponibilidade_Salas.cod_sala, Disponibilidade_Salas.available, Timeslots.hora_inicio, Timeslots.hora_fim, Disponibilidade_Salas.titulo, Disponibilidade_Salas.color, Disponibilidade_Salas.cod_turma " +
                           $"FROM Disponibilidade_Salas JOIN Timeslots ON Timeslots.cod_timeslot = Disponibilidade_Salas.cod_timeslot WHERE Disponibilidade_Salas.cod_sala = {cod_sala} AND Disponibilidade_Salas.available = 0) AS Availability) " +
                           $"SELECT MIN(hora_inicio) AS start_time, MAX(hora_fim) AS end_time, dataa, titulo, color, cod_turma FROM ContiguousSlots GROUP BY dataa, DATEADD(hour, -rn, hora_inicio), titulo, color, cod_turma " +
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
                            eventData.cod_turma = !dr.IsDBNull(5) ? dr.GetInt32(5) : 000;

                            lst_disponibilidade_sala.Add(eventData);
                        }
                    }
                }
            }

            return lst_disponibilidade_sala;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHorarioAutomatico_JSON(int cod_turma)
        {
            int cod_curso = Turmas.Extract_Cod_Curso_Turma(cod_turma);

            List<FullCalendarData> Horario = Gerador_Horario_Automatico(cod_turma, cod_curso);

            string json = JsonConvert.SerializeObject(Horario, Formatting.None);

            return json;
        }

        public List<FullCalendarData> Gerador_Horario_Automatico(int cod_turma, int cod_curso)
        {
            List<FullCalendarData> Horario_Turma = new List<FullCalendarData>();
            List<Formadores_Modulos> lst_formadores_modulos = Formadores_Modulos.Ler_Formadores_Modulos_Turma(cod_turma, cod_curso);
            List<Turmas> info_turma = Turmas.Ler_Turma(cod_turma);
            List<Salas> salas_ativas = Salas.Extract_Salas();
            DateTime data_inicio_turma = info_turma[0].cod_regime == 1 ? info_turma[0].data_inicio.AddHours(8) : info_turma[0].data_inicio.AddHours(16);
            int hora_inicio = info_turma[0].cod_regime == 1 ? 8 : 16;
            int hora_final = info_turma[0].cod_regime == 1 ? 16 : 23;
            DateTime comeco_aula = data_inicio_turma;
            DateTime fim_aula;

            foreach (Formadores_Modulos item in lst_formadores_modulos)
            {
                int cod_modulo = item.cod_modulo;
                int cod_formador = item.cod_formador;
                int cod_user = item.cod_user;
                int duracao_modulo = Modulos.Check_Duracao_Modulo(cod_modulo);
                int total_horas_modulo = duracao_modulo;

                List<FullCalendarData> lst_disponibilidade_formador = GetDisponibilidade_DB(cod_user);
                //DateTime data = DateTime.Parse(lst_disponibilidade_formador[0].start);

                int cont_sala = 0;

                while (total_horas_modulo > 0)
                {

                    if (comeco_aula.DayOfWeek == DayOfWeek.Sunday)
                    {
                        comeco_aula = comeco_aula.AddDays(1);
                    }
                    
                    if (comeco_aula.AddHours(Math.Min(total_horas_modulo, 4)).Hour < hora_final && comeco_aula.AddHours(Math.Min(total_horas_modulo, 4)).Hour != 0)
                        fim_aula = comeco_aula.AddHours(Math.Min(total_horas_modulo, 4));
                    else
                        fim_aula = comeco_aula.Date.AddHours(hora_final);

                    List<FullCalendarData> lst_sala_indisponivel = GetDisponibilidade_Sala_DB(salas_ativas[cont_sala].cod_sala);

                    while (IsFormador_Indisponivel(comeco_aula, fim_aula, lst_disponibilidade_formador) || IsSala_Indisponivel(comeco_aula, fim_aula, lst_sala_indisponivel) || comeco_aula == fim_aula)
                    {
                        if (comeco_aula.DayOfWeek == DayOfWeek.Sunday)
                            comeco_aula.AddDays(1);

                        if (IsSala_Indisponivel(comeco_aula, fim_aula, lst_sala_indisponivel))
                        {
                            cont_sala++;

                            if (cont_sala < salas_ativas.Count)
                            {
                                lst_sala_indisponivel.Clear();
                                lst_sala_indisponivel = GetDisponibilidade_Sala_DB(salas_ativas[cont_sala].cod_sala);
                            }
                            else
                            {
                                cont_sala = 0;

                                if (comeco_aula == fim_aula)
                                {
                                    comeco_aula = comeco_aula.AddDays(1).Date.AddHours(hora_inicio);
                                    fim_aula = comeco_aula.AddHours(4);

                                    if (comeco_aula.DayOfWeek == DayOfWeek.Sunday)
                                        comeco_aula.AddDays(1);
                                }
                                else if (comeco_aula.Hour < hora_final)
                                {
                                    comeco_aula = comeco_aula.AddHours(1);
                                    if (fim_aula.Hour < hora_final)
                                        fim_aula = fim_aula.AddHours(1);
                                }
                            }
                        }
                        else
                        {
                            if (comeco_aula == fim_aula)
                            {
                                comeco_aula = comeco_aula.AddDays(1).Date.AddHours(hora_inicio);
                                fim_aula = comeco_aula.AddHours(4);

                                if (comeco_aula.DayOfWeek == DayOfWeek.Sunday)
                                    comeco_aula.AddDays(1);
                            }
                            else if (comeco_aula.Hour < hora_final)
                            {
                                comeco_aula = comeco_aula.AddHours(1);
                                if (fim_aula.Hour < hora_final)
                                    fim_aula = fim_aula.AddHours(1);
                            }
                        }
                    }

                    string titulo_aula = info_turma[0].nome_turma + " | " + item.nome_modulo + " | " + item.nome_completo + " | " + salas_ativas[cont_sala].nome_sala;

                    TimeSpan inicio = new TimeSpan(comeco_aula.Hour, 0, 0);
                    TimeSpan fim = new TimeSpan(fim_aula.Hour, 0, 0);
                    TimeSpan dif_inicio_fim = fim - inicio;
                    int duracao_aula = dif_inicio_fim.Hours;

                    FullCalendarData aula = new FullCalendarData
                    {
                        title = titulo_aula,
                        Data = comeco_aula.Date,
                        TimeSlot_Inicio = inicio,
                        TimeSlot_Fim = fim,
                        cod_modulo = item.cod_modulo,
                        cod_formador = item.cod_formador,
                        cod_sala = salas_ativas[cont_sala].cod_sala,
                        color = "#1E90FF",
                        cod_turma = cod_turma
                    };

                    Horario_Turma.Add(aula);

                    total_horas_modulo -= duracao_aula;

                    if (fim_aula.Hour == hora_final)
                        comeco_aula = comeco_aula.AddDays(1).Date.AddHours(hora_inicio);
                    else
                        comeco_aula = fim_aula;

                }
            }

            return Horario_Turma;
        }

        private bool IsFormador_Indisponivel(DateTime comeco_aula, DateTime fim_aula, List<FullCalendarData> lst_formador_indisponivel)
        {
            bool conflito = false;

            foreach (var disponibilidade in lst_formador_indisponivel)
            {
                DateTime disponibilidade_comeco = DateTime.ParseExact(disponibilidade.start, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                DateTime disponibilidade_final = DateTime.ParseExact(disponibilidade.end, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                if ((comeco_aula >= disponibilidade_comeco && comeco_aula < disponibilidade_final) ||
                    (fim_aula > disponibilidade_comeco && fim_aula < disponibilidade_final) ||
                    (comeco_aula <= disponibilidade_comeco && fim_aula > disponibilidade_comeco))
                {
                    conflito = true;
                }
            }

            return conflito;
        }

        private bool IsSala_Indisponivel(DateTime comeco_aula, DateTime fim_aula, List<FullCalendarData> lst_sala_indisponivel)
        {
            bool conflito = false;

            foreach (var disponibilidade in lst_sala_indisponivel)
            {
                DateTime disponibilidade_comeco = DateTime.ParseExact(disponibilidade.start, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                DateTime disponibilidade_final = DateTime.ParseExact(disponibilidade.end, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                if ((comeco_aula >= disponibilidade_comeco && comeco_aula < disponibilidade_final) ||
                    (fim_aula > disponibilidade_comeco && fim_aula < disponibilidade_final) ||
                    (comeco_aula <= disponibilidade_comeco && fim_aula > disponibilidade_comeco))
                {
                    conflito = true;
                }
            }

            return conflito;
        }
    }
}
