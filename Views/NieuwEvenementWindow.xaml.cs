using System;
using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class NieuwEvenementWindow : Window
    {
        public Evenement NieuwEvenement { get; private set; }

        public NieuwEvenementWindow()
        {
            InitializeComponent();
        }

        public NieuwEvenementWindow(Evenement bestaandEvenement) : this()
        {
            TitelTextBox.Text = bestaandEvenement.Titel;
            LocatieTextBox.Text = bestaandEvenement.Locatie;
            DatumPicker.SelectedDate = bestaandEvenement.Datum;
            BeschrijvingTextBox.Text = bestaandEvenement.Beschrijving;

            NieuwEvenement = bestaandEvenement;
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            NieuwEvenement = new Evenement
            {
                Titel = TitelTextBox.Text,
                Locatie = LocatieTextBox.Text,
                Datum = DatumPicker.SelectedDate ?? DateTime.Now,
                Beschrijving = BeschrijvingTextBox.Text
            };

            DialogResult = true;
        }
    }
}
