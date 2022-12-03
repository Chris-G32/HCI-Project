using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace HCI_Project.MVVM.View.LibraryViews.ImageResources.Custom_Controls
{
    /// <summary>
    /// Interaction logic for GalleryViewer.xaml
    /// </summary>
    public partial class GalleryViewer : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public ObservableCollection<Uri> ImageUris { get; set; }
        private int _selectedImageIndex { get; set; }
        public int SelectedImageIndex { get { return _selectedImageIndex; }
            set { _selectedImageIndex = value;
                try { 
                if (_selectedImageIndex >= ImageUris.Count())
                {
                    _selectedImageIndex= 0;
                    CurrentImageSource=ImageUris.First();
                }
                else if(_selectedImageIndex<0) {
                    _selectedImageIndex = ImageUris.Count()-1;
                    CurrentImageSource = ImageUris.Last();
                }
                else
                {
                    CurrentImageSource = ImageUris[SelectedImageIndex];
                }
                }
                catch { 
                    CurrentImageSource= null;
                
                }
                Debug.WriteLine("ImageSource in Gallery Viewer Set to: "+CurrentImageSource?.ToString());
                Debug.WriteLine("Index of img source: "+SelectedImageIndex.ToString());
                RaisePropertyChanged(nameof(SelectedImageIndex));
            } 
        
        }
        public Uri CurrentImageSource { get; set; }
        public GalleryViewer()
        {
            SelectedImageIndex= 0;
            //CurrentImageSource = (ImageUris.Count() == 0) ? new Uri("../DefaultImages/default_game_image@2x.png") :ImageUris.First();
            InitializeComponent();
        }

        public Uri ImageFolder
        {
            get { return (Uri)GetValue(ImageFolderProperty); }
            set { SetValue(ImageFolderProperty, value);
                ImageUris.Clear();
                var files = Directory.GetFiles(value.OriginalString);
                foreach (var file in files)
                {
                    if (ImageExtensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                    {
                        ImageUris.Add(new Uri(file));
                    }
                }
                CurrentImageSource = (ImageUris.Count() == 0) ? null : ImageUris.First();
            }
        }

        // Using a DependencyProperty as the backing store for ImageFolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageFolderProperty =
            DependencyProperty.Register("ImageFolder", typeof(Uri), typeof(GalleryViewer));

        private void ArrowButtonRight_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageIndex += 1;
        }

        private void ArrowButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageIndex -= 1;
        }
    }
}
