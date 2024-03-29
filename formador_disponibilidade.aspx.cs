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

namespace Projeto_Final
{
    public partial class formador_disponibilidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Method to receive data from client-side
        [WebMethod]
        public static void ProcessSelectedSlots(string[] selectedSlots)
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
                string startDateTimeStr = parts[0];
                string endDateTimeStr = parts.Length > 1 ? parts[1] : startDateTimeStr;

                // Append default times if only date is provided
                if (!startDateTimeStr.Contains("T"))
                {
                    startDateTimeStr += "T08:00:00";
                }
                else if (startDateTimeStr.EndsWith("Z"))
                {
                    startDateTimeStr = startDateTimeStr.Remove(startDateTimeStr.Length - 1);
                }
                if (!endDateTimeStr.Contains("T"))
                {
                    endDateTimeStr += "T23:00:00";
                }
                else if (endDateTimeStr.EndsWith("Z"))
                {
                    endDateTimeStr = endDateTimeStr.Remove(endDateTimeStr.Length - 1);
                }

                // Parse start and end date-time strings into DateTime objects
                DateTime startDateTime = DateTime.Parse(startDateTimeStr);
                DateTime endDateTime = DateTime.Parse(endDateTimeStr);

                // Extract date and time components
                string startDate = startDateTime.ToString("yyyy-MM-dd");
                string startTime = startDateTime.ToString("HH:mm:ss");
                string endDate = endDateTime.ToString("yyyy-MM-dd");
                string endTime = endDateTime.ToString("HH:mm:ss");

                // Output the components
                Console.WriteLine("Start Date: " + startDate);
                Console.WriteLine("Start Time: " + startTime);
                Console.WriteLine("End Date: " + endDate);
                Console.WriteLine("End Time: " + endTime);
            }
        }
    }
}