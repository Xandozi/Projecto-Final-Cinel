using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class FullCalendarData
    {
        public int Cod_Timeslot { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan TimeSlot_Inicio { get; set; }
        public TimeSpan TimeSlot_Fim { get; set; }
        public string start => $"{Data:yyyy-MM-dd}T{TimeSlot_Inicio}";
        public string end => $"{Data:yyyy-MM-dd}T{TimeSlot_Fim}";
    }
}