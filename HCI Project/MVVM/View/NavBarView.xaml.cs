using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HCI_Project.MVVM.Model;

namespace HCI_Project.MVVM.View
{
    /// <summary>
    /// Interaction logic for NavBarView.xaml
    /// </summary>
    public partial class NavBarView : UserControl
    {
        private bool _settingsShown=false;
        public NavBarView()
        {
            InitializeComponent();
        }



        //SEARCH BAR: Selected Entry (use command to display respective page, etc.)
        public RelayCommand SelectedEntryCommand 
        {
            get { return (RelayCommand)GetValue(SelectedEntryCommandProperty); }
            set { SetValue(SelectedEntryCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEntryCommandProperty =
            DependencyProperty.Register("SelectedEntryCommand", typeof(RelayCommand), typeof(NavBarView));



        //HOME BUTTON FUNCTIONALITY
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //home displays
        }

        public RelayCommand ToggleHomeCommand
        {
            get { return (RelayCommand)GetValue(ToggleHomeCommandProperty); }
            set { SetValue(ToggleHomeCommandProperty, value); }
        }

        public static readonly DependencyProperty ToggleHomeCommandProperty =
            DependencyProperty.Register("ToggleHomeCommand", typeof(RelayCommand), typeof(NavBarView));


        //SETTINGS BUTTON FUNCTIONALITY
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


        /// <summary>
        /// Results to display in dropdown box
        /// </summary>
        public ObservableCollection<Game> SearchResults
        {
            get { return (ObservableCollection<Game>)GetValue(SearchResultsProperty); }
            set { SetValue(SearchResultsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchResultsProperty =
            DependencyProperty.Register("SearchResults", typeof(ObservableCollection<Game>), typeof(NavBarView));

        ///<summary>
        ///
        /// </summary>


        public string SearchQuery
        {
            get { return (string)GetValue(SearchQueryProperty); }
            set { SetValue(SearchQueryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchQuery.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchQueryProperty =
            DependencyProperty.Register("SearchQuery", typeof(string), typeof(NavBarView));

        //Search Box Functionality styling
        /// <summary>
        /// Allows for combobox dropdown to be displayed on text edit
        /// </summary>
        private void SearchBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.IsDropDownOpen = true;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var combo = sender as ComboBox;

            if (!combo.IsDropDownOpen){
                combo.IsDropDownOpen = true;
            }
        }
    }
}
