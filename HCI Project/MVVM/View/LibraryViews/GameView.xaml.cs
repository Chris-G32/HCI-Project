﻿using HCI_Project.MVVM.ViewModel;
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
        public GameView()
        {
            InitializeComponent();
        }
         ~GameView() {
            WebWindow.Dispose();
        }

        private void ChangeWebsite_Click(object sender, RoutedEventArgs e)
        {
            var butt = sender as Button;
            var link = butt.Content as Uri;
            WebWindow.Source = link;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LinkSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddButton.Command.Execute(EnterLink.Text);
                EnterLink.Text = "";
                Keyboard.ClearFocus();
            }
        }
    }
    /// <summary>
    /// Converts "" to Visible anything else to Hidden
    /// </summary>
    public class HideWhenStringNull : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string @string)
            {
                if (@string == "")
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
