using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class BewerkEvenementWindow : Window
    {
        public BewerkEvenementWindow(Evenement evenement)
        {
            InitializeComponent();
            DataContext = evenement;
        }
    }
}

