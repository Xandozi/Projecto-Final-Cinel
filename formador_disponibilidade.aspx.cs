using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
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
        public static string[] ProcessSelectedSlots(string[] selectedSlots)
        {
            string[] slots = new string[selectedSlots.Length];
            int i = 0;
            // Process the selectedSlots array received from the client-side (JavaScript)
            foreach (var slot in selectedSlots)
            {
                slots[i] = slot;
                i++;
            }

            return slots;
        }
    }
}