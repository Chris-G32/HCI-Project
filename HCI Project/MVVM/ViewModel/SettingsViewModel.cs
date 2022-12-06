using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using HCI_Project.Core;
using HCI_Project.MVVM.Model.Settings;

namespace HCI_Project.MVVM.ViewModel
{
    public class SettingsViewModel:ObservableObject
    {
        /// <summary>
        /// The ViewModel to instantiate when exiting settings
        /// </summary>
        public object CameFromVM;
        public bool IsVisible=false;
        public string InstallDir { get; set; }
        public ObservableCollection<string> GameTabs { get; set; }
        public SettingsManager SettingsHandler { get; set; }
        /// <summary>
        /// Instantiates the setting view with no return VM
        /// </summary>
        public SettingsViewModel()
        {
            SettingsHandler = MainViewModel.SettingsHandler;
            GameTabs = new ObservableCollection<string>() { "Play", "Community","Info","Gallery","LASTTAB" };
            var path = new FileInfo("../").Directory.FullName;
            InstallDir = path.Substring(0, path.IndexOf("HCI-Project"))+ "HCI-Project";
        }
    }
}
