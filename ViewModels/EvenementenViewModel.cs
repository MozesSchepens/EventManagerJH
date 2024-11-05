using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EventManagerJH.Models;
using EventManagerJH.Data;
using EventManagerJH.Views;

namespace EventManagerJH.ViewModels
{
    public class EvenementenViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Evenement> EvenementenLijst { get; set; }
        public ObservableCollection<TodoItem> GeselecteerdeToDoLijst { get; set; }
        public ObservableCollection<Shift> GeselecteerdeShiftenLijst { get; set; }
        public ObservableCollection<Boodschap> GeselecteerdeBoodschappenLijst { get; set; }

        private Evenement _geselecteerdEvenement;
        public Evenement GeselecteerdEvenement
        {
            get => _geselecteerdEvenement;
            set
            {
                _geselecteerdEvenement = value;
                OnPropertyChanged(nameof(GeselecteerdEvenement));
                OnPropertyChanged(nameof(IsEvenementGeselecteerd));
                UpdateLijsten();
            }
        }

        private string _zoekTerm;
        public string ZoekTerm
        {
            get => _zoekTerm;
            set
            {
                _zoekTerm = value;
                OnPropertyChanged(nameof(ZoekTerm));
                FilterEvenementen();
            }
        }

        public ICommand NieuwEvenementCommand { get; }
        public ICommand BewerkEvenementCommand { get; }
        public ICommand BekijkDetailsCommand { get; }
        public ICommand VoegToDoToeCommand { get; }
        public ICommand VoegShiftToeCommand { get; }
        public ICommand VoegBoodschapToeCommand { get; }

        public EvenementenViewModel(AppDbContext context)
        {
            _context = context;
            LoadEvenementen();

            NieuwEvenementCommand = new RelayCommand(NieuwEvenement);
            BewerkEvenementCommand = new RelayCommand(BewerkEvenement, () => IsEvenementGeselecteerd);
            BekijkDetailsCommand = new RelayCommand(BekijkDetails, () => IsEvenementGeselecteerd);
            VoegToDoToeCommand = new RelayCommand(VoegToDoToe, () => IsEvenementGeselecteerd);
            VoegShiftToeCommand = new RelayCommand(VoegShiftToe, () => IsEvenementGeselecteerd);
            VoegBoodschapToeCommand = new RelayCommand(VoegBoodschapToe, () => IsEvenementGeselecteerd);
        }

        private void LoadEvenementen()
        {
            EvenementenLijst = new ObservableCollection<Evenement>(_context.Evenementen.ToList());
        }

        public bool IsEvenementGeselecteerd => GeselecteerdEvenement != null;

        private void FilterEvenementen()
        {
            if (string.IsNullOrWhiteSpace(ZoekTerm))
            {
                GeselecteerdEvenement = EvenementenLijst.FirstOrDefault();
            }
            else
            {
                var gefilterdeEvenementen = EvenementenLijst
                    .Where(e => e.Titel.Contains(ZoekTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                GeselecteerdEvenement = gefilterdeEvenementen.FirstOrDefault();
            }
            OnPropertyChanged(nameof(GeselecteerdEvenement));
        }

        private void UpdateLijsten()
        {
            if (GeselecteerdEvenement != null)
            {
                GeselecteerdeToDoLijst = new ObservableCollection<TodoItem>(_context.ToDoItems.Where(t => t.EvenementID == GeselecteerdEvenement.EvenementID).ToList());
                GeselecteerdeShiftenLijst = new ObservableCollection<Shift>(_context.Shiften.Where(s => s.EvenementID == GeselecteerdEvenement.EvenementID).ToList());
                GeselecteerdeBoodschappenLijst = new ObservableCollection<Boodschap>(_context.Boodschappen.Where(b => b.EvenementID == GeselecteerdEvenement.EvenementID).ToList());
            }
            else
            {
                GeselecteerdeToDoLijst = new ObservableCollection<TodoItem>();
                GeselecteerdeShiftenLijst = new ObservableCollection<Shift>();
                GeselecteerdeBoodschappenLijst = new ObservableCollection<Boodschap>();
            }

            OnPropertyChanged(nameof(GeselecteerdeToDoLijst));
            OnPropertyChanged(nameof(GeselecteerdeShiftenLijst));
            OnPropertyChanged(nameof(GeselecteerdeBoodschappenLijst));
        }

        private void NieuwEvenement()
        {
            var nieuwEvent = new Evenement
            {
                Titel = "Nieuw Evenement",
                Datum = DateTime.Now,
                Locatie = "Nieuwe locatie",
                Beschrijving = "Beschrijving"
            };
            EvenementenLijst.Add(nieuwEvent);
            _context.Evenementen.Add(nieuwEvent);
            _context.SaveChanges();
            GeselecteerdEvenement = nieuwEvent;
            OnPropertyChanged(nameof(EvenementenLijst));
        }

        private void BewerkEvenement()
        {
            if (GeselecteerdEvenement == null) return;

            var bewerkWindow = new BewerkEvenementWindow(GeselecteerdEvenement);
            if (bewerkWindow.ShowDialog() == true)
            {
                _context.Evenementen.Update(GeselecteerdEvenement);
                _context.SaveChanges();
                OnPropertyChanged(nameof(EvenementenLijst));
                MessageBox.Show($"Evenement '{GeselecteerdEvenement.Titel}' is bijgewerkt.", "Evenement Bewerken", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BekijkDetails()
        {
            if (GeselecteerdEvenement == null) return;

            string details = $"Titel: {GeselecteerdEvenement.Titel}\n" +
                             $"Datum: {GeselecteerdEvenement.Datum:dd/MM/yyyy}\n" +
                             $"Locatie: {GeselecteerdEvenement.Locatie}\n" +
                             $"Beschrijving: {GeselecteerdEvenement.Beschrijving}";

            MessageBox.Show(details, "Evenement Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void VoegToDoToe()
        {
            if (GeselecteerdEvenement == null) return;

            // Open ToDoLijstWindow voor het invoeren van de beschrijving van de ToDo
            var todoWindow = new ToDoToevoegenWindow();
            if (todoWindow.ShowDialog() == true)
            {
                var nieuwToDo = new TodoItem
                {
                    Beschrijving = todoWindow.Beschrijving,
                    IsVoltooid = false,
                    EvenementID = GeselecteerdEvenement.EvenementID
                };
                _context.ToDoItems.Add(nieuwToDo);
                _context.SaveChanges();
                GeselecteerdeToDoLijst.Add(nieuwToDo);
                OnPropertyChanged(nameof(GeselecteerdeToDoLijst));
            }
        }

        private void VoegShiftToe()
        {
            if (GeselecteerdEvenement == null) return;

            var shiftWindow = new ShiftToevoegenWindow();
            if (shiftWindow.ShowDialog() == true)
            {
                var nieuweShift = new Shift
                {
                    ShiftOmschrijving = shiftWindow.ShiftOmschrijving,
                    StartTijd = shiftWindow.StartTijd,
                    EindTijd = shiftWindow.EindTijd,
                    EvenementID = GeselecteerdEvenement.EvenementID
                };
                _context.Shiften.Add(nieuweShift);
                _context.SaveChanges();
                GeselecteerdeShiftenLijst.Add(nieuweShift);
                OnPropertyChanged(nameof(GeselecteerdeShiftenLijst));
            }
        }

        private void VoegBoodschapToe()
        {
            if (GeselecteerdEvenement == null) return;

            var boodschapWindow = new BoodschapToevoegenWindow();
            if (boodschapWindow.ShowDialog() == true)
            {
                var nieuweBoodschap = new Boodschap
                {
                    Item = boodschapWindow.Boodschap,
                    EvenementID = GeselecteerdEvenement.EvenementID
                };
                _context.Boodschappen.Add(nieuweBoodschap);
                _context.SaveChanges();
                GeselecteerdeBoodschappenLijst.Add(nieuweBoodschap);
                OnPropertyChanged(nameof(GeselecteerdeBoodschappenLijst));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
