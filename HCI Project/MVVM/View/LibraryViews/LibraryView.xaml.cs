using HCI_Project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
        }

        private void Library_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsMouseDirectlyOver)
               Keyboard.ClearFocus();
        }

        //Used to Bind Library to a list of Games Ideally
        //public ObservableCollection<string> ListSource
        //{
        //    get { return (ObservableCollection<string>)GetValue(ListSourceProperty); }
        //    set { SetValue(ListSourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ListSource.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ListSourceProperty =
        //    DependencyProperty.Register("ListSource", typeof(ObservableCollection<string>), typeof(LibraryView), new PropertyMetadata(new ObservableCollection<string>()));

    }
}
