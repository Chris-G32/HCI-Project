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

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class HomeViewModel:ObservableObject
    {
        public BitmapImage Imgtst { get; set; }=null;
        private Uri _testUri=null;
        public Uri TestUri { get { return _testUri; } set { _testUri = value; OnPropertyChanged(); } }
       
        public ObservableCollection<Game> RecentlyPlayed { get; }
        public Game testbind { get; set; }
        //Temporary test command to update image of something
        public RelayCommand GetImage { get; set; }
        public HomeViewModel()
        {
            const int MAXRECENTLYPLAYED= 6;
            RecentlyPlayed = new ObservableCollection<Game>();
            var count = 0;
            foreach(var game in MainViewModel.GameHandler.Games)
            {
                if (count == MAXRECENTLYPLAYED)
                    break;
                RecentlyPlayed.Add(game);
                count++;
            }
            
            
            GetImage = new RelayCommand(o => {
                //Imgtst=new BitmapImage();

                TestUri = new Uri("https://w7.pngwing.com/pngs/925/348/png-transparent-logo-online-and-offline-e-online-design-text-logo-online-and-offline.png");
            });
        }
    }
}
