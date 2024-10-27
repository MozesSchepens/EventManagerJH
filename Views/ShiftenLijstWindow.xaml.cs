using System;
using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class ShiftenLijstWindow : Window
    {
        private Evenement _evenement;

        public ShiftenLijstWindow(Evenement evenement)
        {
            InitializeComponent();
            _evenement = evenement;
            DataContext = _evenement;
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            // Voeg de nieuwe shift toe aan de ShiftenLijst als de velden niet leeg zijn
            if (!string.IsNullOrWhiteSpace(NieuweShiftTextBox.Text) &&
                DateTime.TryParse(StartTijdTextBox.Text, out DateTime startTijd) &&
                DateTime.TryParse(EindTijdTextBox.Text, out DateTime eindTijd))
            {
                // Maak een nieuw Shift-object en stel de waarden in
                var nieuweShift = new Shift
                {
                    ShiftOmschrijving = NieuweShiftTextBox.Text,
                    StartTijd = startTijd,
                    EindTijd = eindTijd
                };

                // Voeg de nieuwe shift toe aan de ShiftenLijst
                _evenement.ShiftenLijst.Add(nieuweShift);

                // Maak de velden leeg na toevoegen
                NieuweShiftTextBox.Clear();
                StartTijdTextBox.Clear();
                EindTijdTextBox.Clear();
            }
            else
            {
                // Toon een foutmelding als de tijden niet kunnen worden geparsed
                MessageBox.Show("Ongeldige tijdsindeling. Voer een geldige datum en tijd in voor start- en eindtijd.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
