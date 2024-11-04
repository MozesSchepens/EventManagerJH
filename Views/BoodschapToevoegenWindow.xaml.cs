using EventManagerJH.Models;
using System.Windows;

namespace EventManagerJH.Views
{
    public partial class BoodschapToevoegenWindow : Window
    {
        public string Boodschap { get; private set; }

        public BoodschapToevoegenWindow()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            Boodschap = BoodschapTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
