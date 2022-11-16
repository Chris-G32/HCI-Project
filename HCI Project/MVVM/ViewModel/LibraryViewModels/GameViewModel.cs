using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;//Debugging Only atm
using System.Text;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    /// <summary>
    /// ViewModel associated with GameView
    /// </summary>
    public class GameViewModel : ObservableObject
    {
        //Current Game Selected Needs Actual Struct
        public object Game { get; private set; }

        /// <summary>
        /// Runs the current game being displayed
        /// </summary>
        public RelayCommand PlayGame { get; set; }

        private object _currentTab;

        public object CurrentTab
        {
            get { return _currentTab; }
            set { _currentTab = value; }
        }


        /// <summary>
        /// Instantiates the GameView and its associated data
        /// </summary>
        /// Also where all relay commands are constructed
        public GameViewModel()
        {
            PlayGame = new RelayCommand(o =>
            {
                //Some Logic TO Run the Game
                //SomeInterface.Run(Game)
                Debug.WriteLine("Running Game (Not Actually)");
            });
        }
        /// <summary>
        /// Instantiates the GameView and its associated data
        /// </summary>
        public GameViewModel(object game) : this()
        {
            Game = game;

        }
    }
}

