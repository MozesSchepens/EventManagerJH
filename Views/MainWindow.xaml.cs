using System.Windows;
using EventManagerJH.ViewModels;
using EventManagerJH.Data;

namespace EventManagerJH.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dbContext = new AppDbContext(); 
            this.DataContext = new EvenementenViewModel(dbContext);
        }
    }
}
