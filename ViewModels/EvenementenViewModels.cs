using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EventManagerJH.Models;
using EventManagerJH.Views;

namespace EventManagerJH.ViewModels
{
    public class EvenementenViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Evenement> EvenementenLijst { get; set; }

        private Evenement _geselecteerdEvenement;
        public Evenement GeselecteerdEvenement
        {
            get => _geselecteerdEvenement;
            set
            {
                _geselecteerdEvenement = value;
                OnPropertyChanged(nameof(GeselecteerdEvenement));
                OnPropertyChanged(nameof(IsEvenementGeselecteerd));
                UpdateCommandStates();
            }
        }

        public ICommand NieuwEvenementCommand { get; }
        public ICommand BewerkEvenementCommand { get; }
        public ICommand BekijkDetailsCommand { get; }
        public ICommand VoegToDoToeCommand { get; }
        public ICommand VoegBoodschapToeCommand { get; }
        public ICommand VoegShiftToeCommand { get; }

        public EvenementenViewModel()
        {
            // Initialiseer EvenementenLijst met dummy data
            EvenementenLijst = new ObservableCollection<Evenement>
            {
                new Evenement { EvenementID = 1, Titel = "Koerrock", Datum = new DateTime(2024, 9, 27), Locatie = "Jeugdhuis", Beschrijving = "Groot feest" },
                new Evenement { EvenementID = 2, Titel = "Verjaardag Casi", Datum = new DateTime(2024, 12, 6), Locatie = "Binnen", Beschrijving = "Privé evenement" }
            };

            NieuwEvenementCommand = new RelayCommand(_ => NieuwEvenement());
            BewerkEvenementCommand = new RelayCommand(_ => BewerkEvenement(), _ => IsEvenementGeselecteerd);
            BekijkDetailsCommand = new RelayCommand(_ => BekijkDetails(), _ => IsEvenementGeselecteerd);
            VoegToDoToeCommand = new RelayCommand(_ => VoegToDoToe(), _ => IsEvenementGeselecteerd);
            VoegBoodschapToeCommand = new RelayCommand(_ => VoegBoodschapToe(), _ => IsEvenementGeselecteerd);
            VoegShiftToeCommand = new RelayCommand(_ => VoegShiftToe(), _ => IsEvenementGeselecteerd);
        }

        public bool IsEvenementGeselecteerd => GeselecteerdEvenement != null;

        private void NieuwEvenement()
        {
            var nieuwEvent = new Evenement
            {
                EvenementID = EvenementenLijst.Count + 1,
                Titel = "Nieuw Evenement",
                Datum = DateTime.Now,
                Locatie = "Nieuwe locatie",
                Beschrijving = "Beschrijving"
            };
            EvenementenLijst.Add(nieuwEvent);
            GeselecteerdEvenement = nieuwEvent; // Selecteer het nieuw toegevoegde evenement
        }

        private void BewerkEvenement()
        {
            if (GeselecteerdEvenement != null)
            {
                var bewerkWindow = new BewerkEvenementWindow(GeselecteerdEvenement);
                bewerkWindow.ShowDialog();
            }
        }

        private void BekijkDetails()
        {
            if (GeselecteerdEvenement != null)
            {
                var detailsWindow = new EvenementDetailsWindow(GeselecteerdEvenement);
                detailsWindow.ShowDialog();
            }
        }

        private void VoegToDoToe()
        {
            if (GeselecteerdEvenement != null)
            {
                GeselecteerdEvenement.ToDoLijst.Add(new TodoItem { Beschrijving = "Nieuw ToDo-item" });
                OnPropertyChanged(nameof(GeselecteerdEvenement));
            }
        }

        private void VoegBoodschapToe()
        {
            if (GeselecteerdEvenement != null)
            {
                GeselecteerdEvenement.BoodschappenLijst.Add(new Boodschap { Item = "Nieuw Boodschappenitem" });
                OnPropertyChanged(nameof(GeselecteerdEvenement));
            }
        }

        private void VoegShiftToe()
        {
            if (GeselecteerdEvenement != null)
            {
                GeselecteerdEvenement.ShiftenLijst.Add(new Shift { ShiftOmschrijving = "Nieuwe Shift", StartTijd = DateTime.Now, EindTijd = DateTime.Now.AddHours(1) });
                OnPropertyChanged(nameof(GeselecteerdEvenement));
            }
        }

        private void UpdateCommandStates()
        {
            ((RelayCommand)BewerkEvenementCommand).RaiseCanExecuteChanged();
            ((RelayCommand)BekijkDetailsCommand).RaiseCanExecuteChanged();
            ((RelayCommand)VoegToDoToeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)VoegBoodschapToeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)VoegShiftToeCommand).RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
