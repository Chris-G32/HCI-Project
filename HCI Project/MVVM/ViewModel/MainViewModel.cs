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

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    {
        //public static SomeSortOfHandler(s)
        //Search Bar Stuff
        private ObservableCollection<Game> _searchList;
        public ObservableCollection<Game> SearchList { get { return _searchList; } set {_searchList = value; OnPropertyChanged(); } }

        public static GameManager GameHandler;

        private string _searchFor;
        public string SearchFor { get { return _searchFor; } 
            set { 
                //Set Value
                _searchFor = value;
                //Clear Displayed Items
                //if (_searchFor == null || _searchFor== ""){
                //    SearchList=new ObservableCollection<Game>(LibraryVM.OwnedGames);
                //}
                //else {
                //    SearchList.Clear();
                //    //Query database for results
                //    foreach(var game in LibraryVM.OwnedGames)
                //    {
                //        if ((game.Name.ToLower()).Contains(_searchFor.ToLower()))
                //        {
                //           SearchList.Add(game);
                //        }
                //    } 
                //}
                SearchList = GameHandler.SearchByName(_searchFor);
                OnPropertyChanged();
            } 
        }
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
        /// <summary>
        /// Constructor which creates ViewModel that contains MainWindows bindings
        /// </summary>
        public MainViewModel()
        {
            GameHandler = new GameManager();

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
        }
    }
}
