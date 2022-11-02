using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.ViewModel
{
    public class GameViewModel:ObservableObject
    {
        public GameViewModel()
        {
            OnPropertyChanged();
        }
    }
}
