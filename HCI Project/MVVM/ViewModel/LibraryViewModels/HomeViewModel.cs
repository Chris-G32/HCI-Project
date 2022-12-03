using System;
using System.Collections.Generic;
using System.Text;
using HCI_Project.Core;
using HCI_Project.MVVM.Model;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class HomeViewModel:ObservableObject
    {
        public BitmapImage Imgtst { get; set; }=null;
        private Uri _testUri=null;
        public Uri TestUri { get { return _testUri; } set { _testUri = value; OnPropertyChanged(); } }
        public LibraryViewModel Parent { get; set; }
        public ObservableCollection<Game> RecentlyPlayed { get; }
        public Game testbind { get; set; }
        //Temporary test command to update image of something
        public RelayCommand PlayGame { get; set; }
        public HomeViewModel(LibraryViewModel parent = null)
        {
            const int MAXRECENTLYPLAYED= 6;
            RecentlyPlayed = new ObservableCollection<Game>();
            var count = 0;
            foreach(var game in MainViewModel.GameHandler.GetRecentlyPlayed())
            {
                if (count == MAXRECENTLYPLAYED)
                    break;
                RecentlyPlayed.Add(game);
                count++;
            }

            PlayGame = new RelayCommand(o =>
            {
                MainViewModel.GameHandler.LaunchGame(o as Game);
            });
            Parent = parent;
        }
    }
}
