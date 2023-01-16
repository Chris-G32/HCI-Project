using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Project.MVVM.View
{
    /// <summary>
    /// Interaction logic for AccountSelectorPopup.xaml
    /// </summary>
    public partial class AccountSelectorPopup : Window
    {
        public AccountSelectorPopup()
        {
            InitializeComponent();
        }

        public AccountSelectorPopup(List<string> accountOptions)
        {
            AccountOptions = accountOptions;
            InitializeComponent();
        }
        public List<string> AccountOptions
        {
            get { return (List<string>)GetValue(AccountOptionsProperty); }
            set { SetValue(AccountOptionsProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for AccountOptions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccountOptionsProperty =
            DependencyProperty.Register("AccountOptions", typeof(List<string>), typeof(AccountSelectorPopup));

        public string Result { get; set; } = "";
        public int ResultIndex { get; set; } = -1;

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = "";
            ResultIndex = -1;
            Close();
        }
    }
}
