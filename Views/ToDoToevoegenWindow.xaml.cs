using System.Windows;

namespace EventManagerJH.Views
{
    public partial class ToDoToevoegenWindow : Window
    {
        public string Beschrijving { get; private set; }

        public ToDoToevoegenWindow()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            Beschrijving = BeschrijvingTextBox.Text;
            DialogResult = true; 
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; 
        }
    }
}
