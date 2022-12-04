using HCI_Project.MVVM.Model;
using HCI_Project.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            //MainViewModel.GameHandler.SaveAllGamesOnClose();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //MainViewModel.GameHandler.SaveAllGamesOnClose();
        }
    }
}
