using System.Collections.ObjectModel;

namespace EventManagerJH.Models
{
    public class Evenement
    {
        public int EvenementID { get; set; }
        public string Titel { get; set; }
        public DateTime Datum { get; set; }
        public string Locatie { get; set; }
        public string Beschrijving { get; set; }

        // ToDo-lijst, Boodschappenlijst en Shiftenlijst
        public ObservableCollection<TodoItem> ToDoLijst { get; set; }
        public ObservableCollection<Boodschap> BoodschappenLijst { get; set; }
        public ObservableCollection<Shift> ShiftenLijst { get; set; }

        public Evenement()
        {
            ToDoLijst = new ObservableCollection<TodoItem>();
            BoodschappenLijst = new ObservableCollection<Boodschap>();
            ShiftenLijst = new ObservableCollection<Shift>();
        }
    }

   
    

    
}
