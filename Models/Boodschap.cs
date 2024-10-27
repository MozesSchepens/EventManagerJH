using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerJH.Models
{
    public class Boodschap
    {
        public int BoodschapID { get; set; }
        public string Item { get; set; }
        public int EvenementID { get; set; } // Foreign key naar het evenement
        public Evenement Evenement { get; set; }
    }
}
