using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using HCI_Project.Core;

namespace HCI_Project.MVVM.ViewModel
{
    public class SettingsViewModel:ObservableObject
    {
        /// <summary>
        /// The ViewModel to instantiate when exiting settings
        /// </summary>
        public object CameFromVM;
        public bool IsVisible=false;

        /// <summary>
        /// Instantiates the setting view with no return VM
        /// </summary>
        public SettingsViewModel()
        {
            
        }
    }
}
