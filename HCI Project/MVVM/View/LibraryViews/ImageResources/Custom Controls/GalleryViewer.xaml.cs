using Newtonsoft.Json.Linq;
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
    public partial class GalleryViewer : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //Used for displaying which image we are at
        public int ImageNumber { get { return SelectedImageIndex + 1; } }
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public ObservableCollection<Uri> ImageUris { get; set; } = new ObservableCollection<Uri>();
        private int _selectedImageIndex { get; set; }
        public int SelectedImageIndex { get { return _selectedImageIndex; }
            set { _selectedImageIndex = value;
                try {
                    
                    if (_selectedImageIndex >= ImageUris.Count())
                    {
                        _selectedImageIndex = 0;
                        CurrentImageSource = ImageUris.First();
                    }
                    else if (_selectedImageIndex < 0) {
                        _selectedImageIndex = ImageUris.Count() - 1;
                        CurrentImageSource = ImageUris.Last();
                    }
                    else
                    {
                        CurrentImageSource = ImageUris[SelectedImageIndex];
                    }
                }
                catch {
                    CurrentImageSource = null;

                }
                Debug.WriteLine("ImageSource in Gallery Viewer Set to: " + CurrentImageSource?.ToString());
                Debug.WriteLine("Index of img source: " + SelectedImageIndex.ToString());
                RaisePropertyChanged(nameof(SelectedImageIndex));
                RaisePropertyChanged(nameof(ImageNumber));
            }

        }
        
        private Uri _currentImageSource;
        public Uri CurrentImageSource { get { return _currentImageSource; } set { _currentImageSource = value;RaisePropertyChanged(nameof(CurrentImageSource)); } }
        public GalleryViewer()
        {
            InitializeComponent();
            SelectedImageIndex = 0;
            CurrentImageSource = (ImageUris.Count() == 0) ? null : ImageUris.First();
        }

        public Uri ImageFolder
        {
            get { return (Uri)GetValue(ImageFolderProperty); }
            set { SetValue(ImageFolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageFolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageFolderProperty =
            DependencyProperty.Register("ImageFolder", typeof(Uri), typeof(GalleryViewer), typeMetadata: new FrameworkPropertyMetadata(propertyChangedCallback: new PropertyChangedCallback(OnImageFolderChanged)));
        private static void OnImageFolderChanged(DependencyObject src, DependencyPropertyChangedEventArgs e)
        {
            var GalViewer=src as GalleryViewer;
            GalViewer.ImageUris.Clear();

            if (e.NewValue == null)
            {
                return;
            }

            var files = Directory.GetFiles((e.NewValue as Uri).OriginalString);
            foreach (var file in files)
            {
                if (ImageExtensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                {
                    GalViewer.ImageUris.Add(new Uri(file));
                }
            }
            
            GalViewer.CurrentImageSource = (GalViewer.ImageUris.Count() == 0) ? null : GalViewer.ImageUris.First();
            GalViewer.SelectedImageIndex = 0;GalViewer.RaisePropertyChanged(nameof(ImageFolder));
        }

        private void ArrowButtonRight_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageIndex += 1;
        }

        private void ArrowButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageIndex -= 1;
        }

        private void Gallery_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    SelectedImageIndex -= 1;
                    break;
                case Key.Right:
                    SelectedImageIndex += 1;
                    break;
            }
        }
    }
}
