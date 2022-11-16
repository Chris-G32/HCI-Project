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
        //Will need updated to first on game switch or instead just create whole new game vm, may be easier and more logical
        private int _tabIndex;

        public int TabIndex
        {
            get { return _tabIndex; }
            set { _tabIndex = value;OnPropertyChanged(); }
        }
        /// <summary>
        /// Runs the current game being displayed
        /// </summary>
        public RelayCommand PlayGame { get; set; }


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

