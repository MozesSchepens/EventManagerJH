using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerJH.Models
{
    public class TodoItem
    {
        public int TodoItemID { get; set; }
        public string Beschrijving { get; set; }
        public int EvenementID { get; set; } 
        public Evenement Evenement { get; set; }
    }

}
