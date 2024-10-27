﻿using System.Windows;
using EventManagerJH.Models;

namespace EventManagerJH.Views
{
    public partial class ToDoLijstWindow : Window
    {
        private Evenement _evenement;

        public ToDoLijstWindow(Evenement evenement)
        {
            InitializeComponent();
            _evenement = evenement;
            DataContext = _evenement;
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NieuwToDoTextBox.Text))
            {
                // Maak een nieuw TodoItem-object aan en voeg het toe aan de ToDoLijst
                var nieuwToDo = new TodoItem
                {
                    Beschrijving = NieuwToDoTextBox.Text
                };

                _evenement.ToDoLijst.Add(nieuwToDo);

                // Wis de tekstbox na het toevoegen
                NieuwToDoTextBox.Clear();
            }
        }
    }
}