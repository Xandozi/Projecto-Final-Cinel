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
        public static bool ProcessSelectedSlots(string[] selectedSlots, int cod_user)
        {
            try
            {
                string[] slots = new string[selectedSlots.Length];
                int i = 0;
                // Process the selectedSlots array received from the client-side (JavaScript)
                foreach (var slot in selectedSlots)
                {
                    slots[i] = slot;
                    i++;
                }

                foreach (var slot in slots)
                {
                    // Split the string into start and end date-time strings
                    string[] parts = slot.Split(',');
                    string inicio_data_str = parts[0];
                    string fim_data_str = parts.Length > 1 ? parts[1] : inicio_data_str;

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

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio);
                        }
                        else
                        {
                            int cod_timeslot = Horarios.Check_Timeslot(inicio_data_slot.AddHours(k), inicio_data_slot.AddHours(1 + k));

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}