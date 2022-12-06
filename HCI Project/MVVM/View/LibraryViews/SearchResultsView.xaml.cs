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

namespace HCI_Project.MVVM.View.LibraryViews
{
    /// <summary>
    /// Interaction logic for SearchResultsView.xaml
    /// </summary>
    public partial class SearchResultsView : UserControl
    {
        public SearchResultsView()
        {
            InitializeComponent();
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
