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

            NieuwEvenementCommand = new RelayCommand<object>(param => NieuwEvenement());
            BewerkEvenementCommand = new RelayCommand<object>(param => BewerkEvenement(), param => IsEvenementGeselecteerd);
            BekijkDetailsCommand = new RelayCommand<object>(param => BekijkDetails(), param => IsEvenementGeselecteerd);
            VoegToDoToeCommand = new RelayCommand<object>(param => VoegToDoToe(), param => IsEvenementGeselecteerd);
            VoegShiftToeCommand = new RelayCommand<object>(param => VoegShiftToe(), param => IsEvenementGeselecteerd);
            VoegBoodschapToeCommand = new RelayCommand<object>(param => VoegBoodschapToe(), param => IsEvenementGeselecteerd);
        }

        private void LoadEvenementen()
        {
            var evenementen = _context.Evenementen.ToList();
            EvenementenLijst = new ObservableCollection<Evenement>(evenementen);
            OnPropertyChanged(nameof(EvenementenLijst));
        }

        public bool IsEvenementGeselecteerd => GeselecteerdEvenement != null;

        private void UpdateLijsten()
        {
            if (GeselecteerdEvenement != null)
            {
                GeselecteerdeToDoLijst = new ObservableCollection<TodoItem>(
                    _context.ToDoItems.Where(t => t.EvenementID == GeselecteerdEvenement.EvenementID).ToList());

                GeselecteerdeShiftenLijst = new ObservableCollection<Shift>(
                    _context.Shiften.Where(s => s.EvenementID == GeselecteerdEvenement.EvenementID).ToList());

                GeselecteerdeBoodschappenLijst = new ObservableCollection<Boodschap>(
                    _context.Boodschappen.Where(b => b.EvenementID == GeselecteerdEvenement.EvenementID).ToList());
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

                MessageBox.Show($"Evenement '{GeselecteerdEvenement.Titel}' is bijgewerkt.",
                                "Evenement Bewerken", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void VoegToDoToe()
        {
            if (GeselecteerdEvenement == null) return;

            var addTodoWindow = new ToDoToevoegenWindow();
            if (addTodoWindow.ShowDialog() == true)
            {
                var nieuwToDo = new TodoItem
                {
                    Beschrijving = addTodoWindow.Beschrijving,
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

            var addShiftWindow = new ShiftToevoegenWindow();
            if (addShiftWindow.ShowDialog() == true)
            {
                var nieuweShift = new Shift
                {
                    ShiftOmschrijving = addShiftWindow.ShiftOmschrijving,
                    StartTijd = addShiftWindow.StartTijd,
                    EindTijd = addShiftWindow.EindTijd,
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

            var addBoodschapWindow = new BoodschapToevoegenWindow();
            if (addBoodschapWindow.ShowDialog() == true)
            {
                var nieuweBoodschap = new Boodschap
                {
                    Item = addBoodschapWindow.Boodschap,
                    EvenementID = GeselecteerdEvenement.EvenementID
                };
                _context.Boodschappen.Add(nieuweBoodschap);
                _context.SaveChanges();
                GeselecteerdeBoodschappenLijst.Add(nieuweBoodschap);
                OnPropertyChanged(nameof(GeselecteerdeBoodschappenLijst));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
