using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        //May End Up Not Being Stored Here, may make a copy to order list how they please possibly
        public ObservableCollection<object> GamesList { get; private set; }
        public RelayCommand OpenGamePage { get; set; }
        public LibraryViewModel()
        {
            OpenGamePage = new RelayCommand(o =>
              {
                  //cast o to the type of GamesList elemeent

              });
        }

    }
}
