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

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    {
        //public static SomeSortOfHandler(s)
        public RelayCommand SetGameView { get; set; }
        public RelayCommand OpenSettings { get; set; }
        public GameViewModel GameVM { get; set; }
        public LibraryViewModel LibraryVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        private bool _settingsOpen = false;
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
            //Instantiate each ViewModel
            GameVM = new GameViewModel();
            SettingsVM = new SettingsViewModel();
            LibraryVM = new LibraryViewModel();

            //Set Default View
            CurrentView = LibraryVM;
            //Set Bound Commands
            SetGameView = new RelayCommand(o =>
            {
                CurrentView = GameVM;
                OnPropertyChanged();
            });
            OpenSettings = new RelayCommand(o =>
            {
               SettingsVM.SwapVisibility();
               OnPropertyChanged();
            });
        }
    }
}
