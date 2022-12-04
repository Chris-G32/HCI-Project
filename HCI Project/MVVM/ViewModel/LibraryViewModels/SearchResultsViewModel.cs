using HCI_Project.Core;
using HCI_Project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Windows.Data;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class SearchResultsViewModel:ObservableObject
    {
        public string Query { get; set; }
        public ListCollectionView Results { get; set; }
        private bool FilterByName(object filterMe)
        {
            //BEGIN OF LOGIC FOR NICER SEARCH
            var game = filterMe as Game;
            return (game.Name.ToUpper()).Contains(Query.ToUpper());
        }
        public SearchResultsViewModel(string query) {
            Query= query;
            Results = new ListCollectionView(MainViewModel.GameHandler.Games);
            Results.Filter = FilterByName;
        } 
    }
}
