using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO.Compression;
using HCI_Project.Utilities;
using System.Windows.Controls;
using System.IO;
using HCI_Project.MVVM.ViewModel.LibraryViewModels;
using HCI_Project.MVVM.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using HCI_Project.MVVM.Model.Settings;

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        //public static SomeSortOfHandler(s)
        //Search Bar Stuff
        private ObservableCollection<Game> _searchList;
        public ObservableCollection<Game> SearchList { get { return _searchList; } set { _searchList = value; OnPropertyChanged(); } }
        public ListCollectionView SearchedForView { get; }
        public ICollectionView AllGames {get;}

        public static GameManager GameHandler;

        private string _searchFor="";
        public string SearchFor { get { return _searchFor; } 
            set { 
                //Set Value
                _searchFor =(value==null)? "":value;
                SearchedForView.Refresh();
                OnPropertyChanged();
            } 
        }
        public static SettingsManager SettingsHandler;
        public RelayCommand SetSearchResultsView { get; set; }
        public RelayCommand SetGameView { get; set; }
        public RelayCommand ToggleSettings { get; set; }
        public RelayCommand Test { get; set; }
        private static LibraryViewModel _libraryVM;
        public LibraryViewModel LibraryVM {
            get { return _libraryVM; }
            set
            {
                _libraryVM = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel SettingsVM { get; set; }
        private static object _currentView;
        
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private bool HideHiddenGames(object filterMe)
        {
            var game = filterMe as Game;
            return !game.Hidden;
        }
        //Search predicate for sorting searchbar
        private bool FilterByName(object filterMe)
        {
            //BEGIN OF LOGIC FOR NICER SEARCH
            var game= filterMe as Game;
            //int MAXLEN = _searchFor.Count();
            //var nameFormatted = game.Name.Replace('-',' ');
            //nameFormatted = game.Name.Replace(':', ' ');
            //var substrs = nameFormatted.Split(' ');
            
            //foreach (var str in substrs)
            //{
            //    if (str == "" ||str==" ")
            //        continue;
            //    if (_searchFor.Contains(str.First())){
            //        return true;
            //    }
            //}
            return (game.Name.ToUpper()).Contains(_searchFor.ToUpper());
        }
        /// <summary>
        /// Constructor which creates ViewModel that contains MainWindows bindings
        /// </summary>
        public MainViewModel()
        {
            SettingsHandler = new SettingsManager();
            GameHandler = new GameManager();
            SearchedForView = new ListCollectionView(GameHandler.Games); 
            AllGames=CollectionViewSource.GetDefaultView(GameHandler.Games);
            AllGames.Filter=HideHiddenGames;
            SearchedForView.Filter = FilterByName; 
            
            //Instantiate each ViewModel
            LibraryVM = new LibraryViewModel();
            SettingsVM = new SettingsViewModel();
            //Set Default View
            CurrentView = LibraryVM;
            SearchList = new ObservableCollection<Game>();
            //var resp=database.getgames containing Mortal

            //Set Bound Commands
            ToggleSettings = new RelayCommand(o =>
            {
                //Toggle off
                if (SettingsVM.IsVisible)
                {
                    CurrentView=SettingsVM.CameFromVM;
                    
                }
                //Toggle on
                else
                {
                    SettingsVM.CameFromVM = CurrentView;
                    CurrentView = SettingsVM; 
                   
                }
                SettingsVM.IsVisible = !SettingsVM.IsVisible;
                OnPropertyChanged();
            });
            SetGameView = new RelayCommand(o =>
            {
                
                LibraryVM.GameVM.SelectedGame = o as Game;
                //LibraryVM.GameVM.SelectedGame = o as Game;
                LibraryVM.CurrentView = LibraryVM.GameVM;
                CurrentView = LibraryVM;
                
            });
            SetSearchResultsView = new RelayCommand(o =>
            {
                LibraryVM.CurrentView = new SearchResultsViewModel(_searchFor,LibraryVM);
                CurrentView = LibraryVM;

            });

        }
    }
}
