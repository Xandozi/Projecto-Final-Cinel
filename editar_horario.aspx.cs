using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class editar_horario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (!Validation.Check_IsStaff(Session["username"].ToString()))
            {
                Response.Redirect("personal_zone.aspx");
            }
            else
            {
                if (Request.QueryString["cod_turma"] != null && Request.QueryString["nome_turma"] != null && Request.QueryString["regime"] != null)
                {
                    int cod_turma;
                    if (int.TryParse(Request.QueryString["cod_turma"], out cod_turma))
                    {
                        string nome_turma = Request.QueryString["nome_turma"];

                        lbl_nome_turma.Text = nome_turma;

                        if (!Page.IsPostBack)
                        {
                            modulos.SelectCommand = $"select Modulos.cod_modulo, Modulos.nome_modulo, Modulos.cod_ufcd, Modulos.duracao from Modulos " +
                                                    $"join Cursos_Modulos on Cursos_Modulos.cod_modulo = Modulos.cod_modulo " +
                                                    $"join Cursos on Cursos.cod_curso = Cursos_Modulos.cod_curso " +
                                                    $"join Turmas on Turmas.cod_curso = Cursos.cod_curso " +
                                                    $"where Turmas.cod_turma = {cod_turma}";

                            ddl_modulo.DataBind();
                        }

                        List<Formadores> formador = Formadores.Check_Formador_Modulo(cod_turma, Convert.ToInt32(ddl_modulo.SelectedValue));
                        lbl_nome_formador.Text = formador[0].nome_completo;
                        hf_cod_formador.Value = formador[0].cod_formador.ToString();
                        hf_regime.Value = Request.QueryString["regime"];
                    }
                }
            }
        }

        // Method to receive data from client-side
        [WebMethod]
        public static bool ProcessSelectedSlots(SlotData[] selectedSlots, int cod_turma)
        {
            try
            {
                Horarios.Insert_Horario_Turma(cod_turma);

                foreach (var slot in selectedSlots)
                {
                    // Access the title property for each slot
                    string titulo = slot.title;
                    string inicio_data_str = slot.start;
                    string fim_data_str = slot.end;

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

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio, titulo);
                        }
                        else
                        {
                            int cod_timeslot = Horarios.Check_Timeslot(inicio_data_slot.AddHours(k), inicio_data_slot.AddHours(1 + k));

                            Horarios.Insert_Disponibilidade_Formador(cod_user, cod_timeslot, data_inicio, titulo);
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
            public string color { get; set; }
        }
    }
}