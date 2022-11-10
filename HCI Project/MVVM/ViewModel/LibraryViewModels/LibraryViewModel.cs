using HCI_Project.Core;
using HCI_Project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        //May End Up Not Being Stored Here, may make a copy to order list how they please possibly
        // public ObservableCollection<object> GamesList { get; private set; }

        //SubViews
        public GameViewModel GameVM { get; set; }

        //Relay Commands
        public RelayCommand OpenGamePage { get; set; }
        public ObservableCollection<Game> OwnedGames { get; set; }
        
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

        public LibraryViewModel()
        {
            //Initializing members
            OwnedGames = new ObservableCollection<Game>() { new Game("Shrt"), new Game("Medium Len Title"), new Game("A particularly very long title of a game that is still going on") };
            GameVM = new GameViewModel();

            //Set Default View
            CurrentView = GameVM;
            
            //Set Relay Commands
            OpenGamePage = new RelayCommand(o =>
              {
                  //Expecting to recieve a game object as parameter, also a int referring to its location in list should work
                  GameVM = new GameViewModel((Game)o);

              });
            
        }

    }
}
