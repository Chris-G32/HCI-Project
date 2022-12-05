using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class HomeView : UserControl,INotifyPropertyChanged
    {
        private int _rowCount = 2;
        public int RowCount { get { return _rowCount; } set{ _rowCount = value; RaisePropertyChanged(nameof(RowCount)); } }
        
        private int _colCount = 3;
        public int ColCount { get { return _colCount; } set { _colCount = value; RaisePropertyChanged(nameof(ColCount)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public HomeView()
        {
            InitializeComponent();
            
        }

        private void MiniPlayButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RecentlyPlayedGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Ratio is less than 6 width for 9 height
            if (e.NewSize.Width < (e.NewSize.Height * 2 / 3))
            {
                RowCount = 6;
                ColCount = 1;
            }
            //Ratio >16 width for 9 height
            else if (e.NewSize.Width >= e.NewSize.Height*16/9)
            {
             
                RowCount = 2;
                ColCount = 3;
            }
            else
            {
                RowCount = 3;
                ColCount = 2;
            }

        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptureWithin)
                Keyboard.ClearFocus();
        }
    }
}
