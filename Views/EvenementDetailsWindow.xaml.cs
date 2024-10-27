using System;
using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class EvenementDetailsWindow : Window
    {
        public EvenementDetailsWindow(Evenement evenement)
        {
            InitializeComponent();
            DataContext = evenement;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}
