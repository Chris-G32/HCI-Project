using HCI_Project.Core;
using HCI_Project.MVVM.Model.Games;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HCI_Project.MVVM.Model
{
    public enum GameState
    {
        OWNED,
        INSTALLED,
        NOT_OWNED
    }
    public class Game:ObservableObject
    {
        /// <summary>
        /// For use in the FindGames function of each launcher to populate with minimum information
        /// </summary>
        public Game(string id, string name, LauncherID launcher)
        {
            Game_ID = id;
            Name = name;
            Launcher_ID = launcher;
        }
        /// <summary>
        /// Used For Binding Stuff Dont feel like fixing it yet
        /// </summary>
        public Game(string id, string name)
        {
            Game_ID = id;
            Name = name;
            
        }
        /// <summary>
        /// Testing constructur for temporary use in the database
        /// </summary>
        public Game(string id, string name, LauncherID launcher, string desc, Uri headerImage = null, Uri iconImage = null, string shortDescription = null, int playtime = 0, int lastplayed=0, GameState state=GameState.OWNED, string galleryFolder=null)
        {
            Game_ID = id;
            Name = name;
            Launcher_ID = launcher;
            Description = desc;
            HeaderImage = headerImage;
            IconImage = iconImage;
            ShortDescription = shortDescription;
            PlaytimeHours = playtime;
            _lastplayed = lastplayed;
            if(galleryFolder != null)
                GalleryFolder = new Uri(galleryFolder);
        }

        public string Game_ID { get; }
        public string Name { get; }
        // Contains a value from the LauncherID enum from Launcher.cs
        public LauncherID Launcher_ID { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public int _lastplayed;
        public DateTime LastPlayed { 
            get
            {
                // Converts the Unix timestamp which steam uses into a DateTime object
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(_lastplayed);
                // Manual conversion from UTC -> EST
                return dto.DateTime.AddHours(-5);
            }
        }
        public int PlaytimeHours { get; set; }
        public Uri HeaderImage { get; set; }
        public Uri IconImage { get; set; } = new Uri("http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg");
        // Contains a value from the GameState Enum in this file
        public GameState State { get; set; }
        // Link to discord channel
        public ObservableCollection<Uri> SavedLinks { get; set; } = new ObservableCollection<Uri>();
        public bool Hidden { get; set; }
        private Uri _galleryFolder;
        public Uri HeroImage { get { return new Uri($"https://steamcdn-a.akamaihd.net/steam/apps/{Game_ID}/library_hero.jpg"); } }
        public Uri BoxImage { get { return new Uri($"https://steamcdn-a.akamaihd.net/steam/apps/{Game_ID}/library_600x900.jpg"); } }
        public Uri GalleryFolder { get { return _galleryFolder; }set { _galleryFolder = value;OnPropertyChanged(); } }

        private GameNews[] _news;
        public GameNews[] News
        {
            get
            {
                return _news;
            }
            set
            {
                _news = value; OnPropertyChanged();
            }
        }
    }
}
