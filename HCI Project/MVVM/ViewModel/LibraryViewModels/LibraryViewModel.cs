using HCI_Project.Core;
using HCI_Project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public enum TestView { Game,Home}
    /// <summary>
    /// Handles LibraryViews and its instantiated subviews
    /// </summary>
    public class LibraryViewModel : ObservableObject
    {
        //May End Up Not Being Stored Here, may make a copy to order list how they please possibly
        // public ObservableCollection<object> GamesList { get; private set; }

        //SubViews
        //private GameViewModel _gameVM;
        //public GameViewModel GameVM {get{ return _gameVM; } set { _gameVM = value; OnPropertyChanged(); } }
        public GameViewModel GameVM { get; set; }
        //      private HomeViewModel _homeVM;
        //public HomeViewModel HomeVM { get { return _homeVM; } set { _homeVM = value; OnPropertyChanged(); } }
        public HomeViewModel HomeVM { get; set; }
        //Relay Commands
        public RelayCommand OpenGamePage { get; set; }
        public RelayCommand SetHomeView { get; set; } 
        public ObservableCollection<Game> OwnedGames { get; set; }
        
        private object _currentView;

        /// <summary>
        /// Currently displayed subview
        /// </summary>
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
        /// Instantiates the LibraryView. Handles switching between views seen next to library.
        /// </summary>
        public LibraryViewModel()
        {
            //Initializing members
            OwnedGames = new ObservableCollection<Game>() { new Game("Shrt"), new Game("Medium Len Title"), new Game("A particularly very long title of a game that is still going on") };
            GameVM = new GameViewModel();
            HomeVM = new HomeViewModel();
            //Set Default View

            CurrentView = HomeVM;
            //Set Relay Commands
            OpenGamePage = new RelayCommand(o =>
              {
                  //Expecting to recieve a game object as parameter, also a int referring to its location in list should work
                  GameVM = new GameViewModel(o as Game);
                  CurrentView = GameVM;

              });
            SetHomeView = new RelayCommand(o => { 
                CurrentView = HomeVM;
            }
            );

        }
    }
}
