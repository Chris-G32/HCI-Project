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

namespace HCI_Project.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            
            InitializeComponent();
        }

        private void RestoreDefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Revert to default settings?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
            }

        }

        private void RestorePrevSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Restore previously saved settings?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
            }
        }

        private void ShowHiddenCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LaunchOnStartupCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
