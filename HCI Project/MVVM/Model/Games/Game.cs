using HCI_Project.Core;
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
        public Game(string game_ID, string name, LauncherID launcher_ID, List<string> tags, string[] images, string description, GameState state)
        {
            Game_ID = game_ID;
            Name = name;
            Launcher_ID = launcher_ID;
            Tags = tags;
            Images = images;
            Description = description;
            State = state;
        }

        /// <summary>
        /// For testing Purposes
        /// </summary>
        public Game(string name,string definition)
        {
            Name=name;
            Description = definition;
        }

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
        /// Testing constructur for temporary use in the database
        /// </summary>
        public Game(string id, string name, LauncherID launcher, string desc, Uri headerImage = null, Uri iconImage=null, string shortDescription=null, int playtime=0)
        {
            Game_ID = id;
            Name = name;
            Launcher_ID = launcher;
            Description = desc;
            HeaderImage = headerImage;
            IconImage = iconImage;
            Short_Description = shortDescription;
            Playtime_Hours = playtime;
        }

        public string Game_ID { get; }
        public string Name { get; }
        // Contains a value from the LauncherID enum from Launcher.cs
        public LauncherID Launcher_ID { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string[] Images { get; set; }
        public string Description { get; set; }
        public string Short_Description { get; set; }

        public int Playtime_Hours { get; set; }
        public Uri HeaderImage { get; set; }=new Uri("https://cdn.akamai.steamstatic.com/steam/apps/1276390/header_alt_assets_4.jpg?t=1669803774");
        public Uri IconImage { get; set; } = new Uri("http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg");
        // Contains a value from the GameState Enum in this file
        public GameState State { get; set; }
        // Link to discord channel
        public ObservableCollection<Uri> SavedLinks { get; set; } = new ObservableCollection<Uri>();
    }
}
