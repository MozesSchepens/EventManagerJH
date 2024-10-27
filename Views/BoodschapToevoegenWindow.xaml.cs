using System.Windows;

namespace EventManagerJH.Views
{
    public partial class BoodschapToevoegenWindow : Window
    {
        public string Item { get; private set; }

        public BoodschapToevoegenWindow()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            Item = BoodschapTextBox.Text;
            DialogResult = true;
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
