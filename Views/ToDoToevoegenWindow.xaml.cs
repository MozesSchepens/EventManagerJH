﻿using System.Windows;

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
            DialogResult = true; // Sluit de dialog af en geeft aan dat het succesvol is
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Sluit de dialog af zonder te saven
        }
    }
}