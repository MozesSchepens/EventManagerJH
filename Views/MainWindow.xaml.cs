using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EventManagerJH.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Methode om de achtergrondkleur van de evenementenlijst te veranderen
        private void VeranderAchtergrondKleur(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            byte r = (byte)rnd.Next(256);
            byte g = (byte)rnd.Next(256);
            byte b = (byte)rnd.Next(256);

            // Verander de achtergrondkleur van de ListBox
            EvenementenListBox.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        // Methode om een rand toe te voegen aan de geselecteerde ListBox-item
        private void EvenementenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EvenementenListBox.SelectedItem != null)
            {
                EvenementenListBox.BorderBrush = new SolidColorBrush(Colors.Blue);
                EvenementenListBox.BorderThickness = new Thickness(2);
            }
            else
            {
                EvenementenListBox.BorderThickness = new Thickness(0);
            }
        }
    }
}
