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
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        int _imageIndex = 1;
        int _numImage = 0;
        public GameView()
        {
            InitializeComponent();
        }
         ~GameView() {
            WebWindow.Dispose();
        }

        private void MainPlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ArrowButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex--;
            if (_imageIndex < 1)
            {
                _imageIndex = _numImage;
            }
            //ImageWindow.Source = new Uri()
        }
        private void ArrowButtonRight_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex++;
            if (_imageIndex > _numImage)
            {
                _imageIndex = 1;
            }
            //ImageWindow.Source = new Uri()
        }

        private void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {

           _numImage = 0;  //need a way to get number of files in directory

        }
    }
}
