using System.Windows;
using EventManagerJH.ViewModels;

namespace EventManagerJH.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(EvenementenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
