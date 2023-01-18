using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace HCI_Project.MVVM.View.LibraryViews.ImageResources.Custom_Controls
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
        }


        public ListCollectionView SearchResults
        {
            get { return (ListCollectionView)GetValue(SearchResultsProperty); }
            set { SetValue(SearchResultsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchResults.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchResultsProperty =
            DependencyProperty.Register("SearchResults", typeof(ListCollectionView), typeof(SearchBar));



        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBar), new PropertyMetadata("Search"));


        public RelayCommand EntryClicked
        {
            get { return (RelayCommand)GetValue(EntryClickedProperty); }
            set { SetValue(EntryClickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntryClickedProperty =
            DependencyProperty.Register("EntryClicked", typeof(RelayCommand), typeof(SearchBar));

        private void ButtonTST_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }
    }
    /// <summary>
    /// Converts 0 to Visible anything else to Hidden
    /// </summary>
    public class IsCollectionEmpty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int @int)
            {
                if (@int == 0)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                    return System.Windows.Visibility.Hidden;
            }
            return System.Windows.Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
