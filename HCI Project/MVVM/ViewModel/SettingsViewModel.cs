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
        public SettingsViewModel()
        {

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
