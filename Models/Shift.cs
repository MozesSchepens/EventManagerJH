using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerJH.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public string ShiftOmschrijving { get; set; }
        public DateTime StartTijd { get; set; }
        public DateTime EindTijd { get; set; }
        public int EvenementID { get; set; } // Foreign key naar het evenement
        public Evenement Evenement { get; set; }
    }
}
