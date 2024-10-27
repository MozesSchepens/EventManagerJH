using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class BoodschappenLijstWindow : Window
    {
        private Evenement _evenement;

        public BoodschappenLijstWindow(Evenement evenement)
        {
            InitializeComponent();
            _evenement = evenement;
            DataContext = _evenement;
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NieuweBoodschapTextBox.Text))
            {
                // Maak een nieuw Boodschap-object aan en voeg het toe aan de BoodschappenLijst
                var nieuweBoodschap = new Boodschap
                {
                    Item = NieuweBoodschapTextBox.Text
                };

                _evenement.BoodschappenLijst.Add(nieuweBoodschap);

                // Wis de tekstbox na het toevoegen
                NieuweBoodschapTextBox.Clear();
            }
        }
    }
}
