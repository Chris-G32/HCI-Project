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
using HCI_Project.Core;
using HCI_Project.MVVM.Model;
namespace HCI_Project.MVVM.View.LibraryViews.ImageResources.Custom_Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GamePreview : UserControl
    {
        private Image DefaultImage = new Image();
        public GamePreview()
        {
            InitializeComponent();
            
        }

        public Game GameSrc
        {
            get { return (Game)GetValue(GameSrcProperty); }
            set { SetValue(GameSrcProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameSrcProperty =
            DependencyProperty.Register("GameSrc", typeof(Game), typeof(GamePreview));


        //public Uri GameImageSrc
        //{
        //    get { return (Uri)GetValue(GameImageSrcProperty); }
        //    set { SetValue(GameImageSrcProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for GameImage.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty GameImageSrcProperty =
        //    DependencyProperty.Register("GameImageSrc", typeof(Uri), typeof(GamePreview));




        public RelayCommand OpenGameCommand
        {
            get { return (RelayCommand)GetValue(OpenGameCommandProperty); }
            set { SetValue(OpenGameCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenGameCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenGameCommandProperty =
            DependencyProperty.Register("OpenGameCommand", typeof(RelayCommand), typeof(GamePreview) );


        public RelayCommand PlayCommand
        {
            get { return (RelayCommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.Register("PlayCommand", typeof(RelayCommand), typeof(GamePreview));



        public bool UseBoxImage
        {
            get { return (bool)GetValue(UseBoxImageProperty); }
            set { SetValue(UseBoxImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseBoxImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBoxImageProperty =
            DependencyProperty.Register("UseBoxImage", typeof(bool), typeof(GamePreview), new PropertyMetadata(false));

        public Uri DisplayedImage
        {
            get { if (UseBoxImage)
                    return GameSrc.BoxImage;
                else
                    return GameSrc.HeaderImage;
            }
            
        }


    }
}
