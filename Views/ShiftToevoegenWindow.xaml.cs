using System;
using System.Windows;

namespace EventManagerJH.Views
{
    public partial class ShiftToevoegenWindow : Window
    {
        public string ShiftOmschrijving { get; private set; }
        public DateTime StartTijd { get; private set; }
        public DateTime EindTijd { get; private set; }

        public ShiftToevoegenWindow()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            // Controleer of de velden zijn ingevuld en de tijdformaten correct zijn
            if (!string.IsNullOrWhiteSpace(ShiftOmschrijvingTextBox.Text) &&
                DateTime.TryParse(StartTijdTextBox.Text, out DateTime startTijd) &&
                DateTime.TryParse(EindTijdTextBox.Text, out DateTime eindTijd))
            {
                ShiftOmschrijving = ShiftOmschrijvingTextBox.Text;
                StartTijd = startTijd;
                EindTijd = eindTijd;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Vul een geldige omschrijving en tijden in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
