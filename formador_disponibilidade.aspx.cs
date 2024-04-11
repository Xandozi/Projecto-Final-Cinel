using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Projeto_Final.Classes;

namespace Projeto_Final
{
    public partial class formador_disponibilidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if ((Request.QueryString["cod_user"].ToString() != Session["cod_user"].ToString()))
            {
                Response.Redirect("personal_zone.aspx");
            }
            else
            {
                int cod_user = Convert.ToInt32(Request.QueryString["cod_user"]);
                hf_cod_user.Value = cod_user.ToString();

                // RegisterStartupScript is used to call the JavaScript function from server-side
                ScriptManager.RegisterStartupScript(this, GetType(), "SetCodUser", $"setCodUser({cod_user});", true);
            }
        }

        // Method to receive data from client-side
        [WebMethod]
        public static bool Gravar_Disponibilidade_Formador(SlotData[] selectedSlots, int cod_user)
        {
            try
            {
                Horarios.Delete_Disponibilidade_Formador(cod_user);

                foreach (var slot in selectedSlots)
                {
                    // Access the title property for each slot
                    string titulo = slot.title;
                    string inicio_data_str = slot.start;
                    string fim_data_str = slot.end;
                    string color = slot.color;
                    int cod_turma = slot.cod_turma;

                    // Append default times if only date is provided
                    if (!inicio_data_str.Contains("T"))
                        inicio_data_str += "T08:00:00";
                    else if (inicio_data_str.EndsWith("Z"))
                        inicio_data_str = inicio_data_str.Remove(inicio_data_str.Length - 1);

                    if (!fim_data_str.Contains("T"))
                        fim_data_str += "T23:00:00";
                    else if (fim_data_str.EndsWith("Z"))
                        fim_data_str = fim_data_str.Remove(fim_data_str.Length - 1);

                    // Parse start and end date-time strings into DateTime objects
                    DateTime inicio_data_slot = DateTime.Parse(inicio_data_str);
                    DateTime fim_data_slot = DateTime.Parse(fim_data_str);

                    TimeSpan diferença_tempo = fim_data_slot.TimeOfDay - inicio_data_slot.TimeOfDay;

                    int diferença = diferença_tempo.Hours;

                    // Extract date and time components
                    string data_inicio = inicio_data_slot.ToString("yyyy-MM-dd");
                    string slot_inicio = inicio_data_slot.ToString("HH:mm:ss");
                    string data_fim = fim_data_slot.ToString("yyyy-MM-dd");
                    string slot_fim = fim_data_slot.ToString("HH:mm:ss");

                    for (int k = 0; k < diferença; k++)
                    {
                        if (k == 0)
                        {
                            int cod_timeslot = Horarios.Check_Timeslot(inicio_data_slot, inicio_data_slot.AddHours(1));

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio, titulo, color, cod_turma);
                        }
                        else
                        {
                            int cod_timeslot = Horarios.Check_Timeslot(inicio_data_slot.AddHours(k), inicio_data_slot.AddHours(1 + k));

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio, titulo, color, cod_turma);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string exx = ex.ToString();
                return false;
            }
        }

        // Define a class to represent the structure of each slot object
        public class SlotData
        {
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int cod_modulo { get; set; }
            public int cod_formador { get; set; }
            public int cod_sala { get; set; }
            public int cod_turma { get; set; }
            public string color { get; set; }
        }
    }
}