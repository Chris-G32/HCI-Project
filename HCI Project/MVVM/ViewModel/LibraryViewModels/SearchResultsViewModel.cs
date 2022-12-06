using HCI_Project.Core;
using HCI_Project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class SearchResultsViewModel:ObservableObject
    {
        public string Query { get; set; } = "";
        public ListCollectionView Results { get; set; }
        public RelayCommand PlayGame { get; set; }
        private bool FilterByName(object filterMe)
        {
            //BEGIN OF LOGIC FOR NICER SEARCH
            var game = filterMe as Game;
            //Results.Refresh();
            return (game.Name.ToUpper()).Contains(Query.ToUpper());
        }
        public LibraryViewModel Parent { get; set; }
        public SearchResultsViewModel(string query,LibraryViewModel parent) {
            Keyboard.ClearFocus();
            Query= query;
            Results = new ListCollectionView(MainViewModel.GameHandler.Games)
            {
                Filter = FilterByName
            };
            Results.Refresh();
            PlayGame = new RelayCommand(o =>
            {
                MainViewModel.GameHandler.LaunchGame(o as Game);
            });
            Parent = parent;
        } 
    }
}
