using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel associated with GameView
    /// </summary>
    public class GameViewModel:ObservableObject
    {
        public RelayCommand ChangePlayTab { get; set; }

        private object _currentTab;

        public object CurrentTab
        {
            get { return _currentTab; }
            set { _currentTab = value; }
        }


        /// <summary>
        /// Instantiates the GameView and its associated data
        /// </summary>
        public GameViewModel()
        {
          
        }
    }
}

