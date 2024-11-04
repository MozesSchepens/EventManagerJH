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
                FilterEvenementen(); // Filter evenementen zodra de zoekterm wordt bijgewerkt
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
            try
            {
                var evenementen = _context.Evenementen.ToList();
                EvenementenLijst = new ObservableCollection<Evenement>(evenementen);
                OnPropertyChanged(nameof(EvenementenLijst));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het laden van evenementen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

                if (gefilterdeEvenementen.Any())
                {
                    GeselecteerdEvenement = gefilterdeEvenementen.First();
                }
                else
                {
                    GeselecteerdEvenement = null;
                }
            }
            OnPropertyChanged(nameof(GeselecteerdEvenement));
        }

        private void UpdateLijsten()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het bijwerken van de lijsten: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NieuwEvenement()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het aanmaken van een nieuw evenement: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BewerkEvenement()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het bewerken van het evenement: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VoegToDoToe()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het toevoegen van een ToDo-item: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VoegShiftToe()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het toevoegen van een shift: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VoegBoodschapToe()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het toevoegen van een boodschap: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BekijkDetails()
        {
            try
            {
                if (GeselecteerdEvenement == null) return;

                string details = $"Titel: {GeselecteerdEvenement.Titel}\n" +
                                 $"Datum: {GeselecteerdEvenement.Datum:dd/MM/yyyy}\n" +
                                 $"Locatie: {GeselecteerdEvenement.Locatie}\n" +
                                 $"Beschrijving: {GeselecteerdEvenement.Beschrijving}";

                MessageBox.Show(details, "Evenement Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het bekijken van de details: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
