using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;//Debugging Only atm
using System.Text;
using HCI_Project.MVVM.Model;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    /// <summary>
    /// ViewModel associated with GameView
    /// </summary>
    public class GameViewModel : ObservableObject
    {
        //Current Game Selected Needs Actual Struct
        public Game SelectedGame { get; private set; }
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

        public RelayCommand AddLink { get; set; }
        public RelayCommand RemoveLink { get; set; } 

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
        public GameViewModel(Game game) : this()
        {
            SelectedGame = game;
            PlayGame = new RelayCommand(o =>
            {
                //Some Logic TO Run the Game
                //SomeInterface.Run(Game)
                MainViewModel.GameHandler.LaunchGame(game);
                Debug.WriteLine("Running Game, Actually");
            });
            RemoveLink = new RelayCommand(o => {
                SelectedGame.SavedLinks.Remove(o as Uri);
            });
            AddLink = new RelayCommand(o => {
                Uri addMe;
                try
                {
                    var uriStr = o as string;
                    if (uriStr.Contains("https://") == false && uriStr.Contains("htts://")==false)
                    {
                        uriStr = "https://" + uriStr;
                    }
                    addMe = new Uri(uriStr);
                    SelectedGame.SavedLinks.Add(addMe);
                }
                catch
                {
                    string messageBoxText = "Invalid link entered, make sure it links to a web address.";
                    string caption = "Invalid Uri";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                }



            });
        }
    }
}

