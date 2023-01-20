using HCI_Project.MVVM.Model;
using HCI_Project.MVVM.Model.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Provides methods and data for interacting wiht the users owned games and communicating state info.
        /// </summary>
        public static GameManager GameHandler;

        /// <summary>
        /// Used to communicate with global settings throughout the application.
        /// </summary>
        public static SettingsManager SettingsHandler;
        private void First_Startup()
        {

        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            //Check if it is the first startup by seeing if the settings and the db files exist
            //NEEDS DONE
            //if(somecondition)
            //{
            //First_Startup();
            //return;
            //}

            //Creates the MainWindow. Doing this here in code allows for dialog boxes
            //to be thrown by things while loading mainWindow
            //and not cause bindings to start
            var mainWindow= new MainWindow();

            //Initialize games first, settings has some reliance on this stuff
            GameHandler=new GameManager();
            //^^^^ Cannot spawn a window from this because

            SettingsHandler=new SettingsManager();

            //Display Mainwindow and instantiate bindings
            mainWindow.InitializeComponent();
            mainWindow.Show();
            Debug.WriteLine("On Startup");
        }
    }
}
