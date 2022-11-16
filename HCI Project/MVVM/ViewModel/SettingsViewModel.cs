using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using HCI_Project.Core;

namespace HCI_Project.MVVM.ViewModel
{
    public class SettingsViewModel:ObservableObject
    {
        public Visibility canSee { get; private set; } = Visibility.Hidden;
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

        /// <summary>
        /// Constructs the settings ViewModel
        /// </summary>
        /// <param name="PreviousVM"> The ViewModel to return to after settings is closed</param>
        public SettingsViewModel(object PreviousVM)
        {
            CameFromVM = PreviousVM;
        }

        /// <summary>
        /// Changes if the control is visible or not
        /// </summary>
        public void SwapVisibility()
        {
            if (canSee==Visibility.Hidden)
            {
                canSee = Visibility.Visible;
            }
            else
                canSee = Visibility.Hidden;
            
        }
    }
}
