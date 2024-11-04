using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class BewerkEvenementWindow : Window
    {
        public Evenement Evenement { get; private set; }

        public BewerkEvenementWindow(Evenement evenement)
        {
            InitializeComponent();
            Evenement = evenement;
            DataContext = Evenement;
        }


        private void OpslaanButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
