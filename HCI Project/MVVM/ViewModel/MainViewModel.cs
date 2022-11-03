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
using HCI_Project.MVVM.Model;
using System.Windows.Controls;
using System.IO;

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    {
        //public static SomeSortOfHandler(s)
        public RelayCommand SetGameView { get; set; }
        public RelayCommand TaskViewCmd { get; set; }
        public GameViewModel GameVM { get; set; }

        private object _currentView;

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
            GameVM = new GameViewModel();
           
            CurrentView = GameVM;
            SetGameView = new RelayCommand(o =>
            {
                CurrentView = GameVM;
                OnPropertyChanged();
            });
            
        }
    }
}
