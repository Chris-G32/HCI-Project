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
using HCI_Project.Core;
namespace HCI_Project.MVVM.View
{
    /// <summary>
    /// Interaction logic for SearchBarView.xaml
    /// </summary>
    public partial class NavBarView : UserControl
    {
        private bool _settingsShown=false;
        public NavBarView()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_settingsShown == false)
            {
                HomeButton.Visibility = Visibility.Collapsed;
                SearchBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                HomeButton.Visibility = Visibility.Visible;
                SearchBox.Visibility = Visibility.Visible;
            }
            _settingsShown = !_settingsShown;
        }


        public RelayCommand ToggleSettingsCommand
        {
            get { return (RelayCommand)GetValue(ToggleSettingsCommandProperty); }
            set { SetValue(ToggleSettingsCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleSettingsCommandProperty =
            DependencyProperty.Register("ToggleSettingsCommand", typeof(RelayCommand), typeof(NavBarView));



        public RelayCommand HomeButtonCommand
        {
            get { return (RelayCommand)GetValue(HomeButtonCommandProperty); }
            set { SetValue(HomeButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HomeButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeButtonCommandProperty =
            DependencyProperty.Register("HomeButtonCommand", typeof(RelayCommand), typeof(NavBarView));


    }
}
