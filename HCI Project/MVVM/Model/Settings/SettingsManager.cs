using HCI_Project.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HCI_Project.MVVM.Model.Settings
{
    public class SettingsManager:ObservableObject
    {
        private SettingsObject _current;

        public SettingsObject Current { get { return _current; } set { _current = value; OnPropertyChanged(); } }

        private SettingsObject _previous;

        private SettingsObject _default;

        public SettingsManager()
        {
            _current = ReadSettingsFromFiles("../../../config.json");
            _previous = ReadSettingsFromFiles("../../../previousConfig.json");
            _default = new SettingsObject(false, false, GameTabs.PLAY);
        }

        private SettingsObject ReadSettingsFromFiles(string location)
        {
            SettingsObject _settings;

            using (StreamReader r = new StreamReader(location))
            {
                string json = r.ReadToEnd();
                dynamic options = JsonConvert.DeserializeObject(json);
                _settings = new SettingsObject((bool)options.launchOnStartup, (bool)options.showHidden, (GameTabs)options.defaultTab);
            }
            return _settings;
        }

        /// <summary>
        /// Updates all settings files objects and files accordingly
        /// </summary>
        public void SaveSettings(JsonSerializerSettings _options)
        {
            
        }

        /// <summary>
        /// Restores the previous settings state
        /// </summary>
        public void RestoreFromPrevious()
        {
            Current = _previous;
        }

        /// <summary>
        ///  Restores the default settings;
        /// </summary>
        public void RestoreFromDefault()
        {
            Current = _default;
        }

    }

    public enum GameTabs
    {
        PLAY,
        COMMUNITY,
        INFO,
        GALLERY,
        LAST
    }

    public class SettingsObject:ObservableObject
    {
        public bool ShowHidden { get; set; }
        public bool LaunchOnStartup { get; set; }
        public GameTabs DefaultTab { get; set; }

        public SettingsObject(bool showHidden, bool launchOnStartup, GameTabs defaultTab)
        {
            ShowHidden = showHidden;
            LaunchOnStartup = launchOnStartup;
            DefaultTab = defaultTab;
            //Show install directory
        }

        
    }
}
