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
        void First_Startup()
        {

        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Debug.WriteLine("On Startup");
        }
    }
}
