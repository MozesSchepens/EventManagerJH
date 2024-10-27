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
            if (!string.IsNullOrWhiteSpace(NieuweShiftTextBox.Text) &&
                DateTime.TryParse(StartTijdTextBox.Text, out DateTime startTijd) &&
                DateTime.TryParse(EindTijdTextBox.Text, out DateTime eindTijd))
            {
                var nieuweShift = new Shift
                {
                    ShiftOmschrijving = NieuweShiftTextBox.Text,
                    StartTijd = startTijd,
                    EindTijd = eindTijd
                };

                _evenement.ShiftenLijst.Add(nieuweShift);

                NieuweShiftTextBox.Clear();
                StartTijdTextBox.Clear();
                EindTijdTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Ongeldige tijdsindeling. Voer een geldige datum en tijd in voor start- en eindtijd.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
