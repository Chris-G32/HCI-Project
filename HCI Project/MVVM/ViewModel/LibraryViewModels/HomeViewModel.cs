using System;
using System.Collections.Generic;
using System.Text;
using HCI_Project.Core;
using System.Windows.Media;
using System.Threading.Tasks;

namespace HCI_Project.MVVM.ViewModel.LibraryViewModels
{
    public class HomeViewModel:ObservableObject
    {
        public ImageSource Imgtst { get; set; }=null;
        public HomeViewModel()
        {
           // _ = FetchImageTest(Imgtst);
        }
        public async Task FetchImageTest(ImageSource input)
        {
           
        }
    }
}
