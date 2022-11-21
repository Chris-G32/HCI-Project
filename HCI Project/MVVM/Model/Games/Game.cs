using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.Model
{
    public enum GameState
    {
        OWNED,
        INSTALLED,
        NOT_OWNED
    }
    public class Game
    {
        public Game(string game_ID, string name, LauncherID launcher_ID, string[] tags, string[] images, string description, GameState state)
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

        public string Game_ID { get; }
        public string Name { get; }
        // Contains a value from the LauncherID enum from Launcher.cs
        public LauncherID Launcher_ID { get; set; }
        public string[] Tags { get; set; }
        public string[] Images { get; set; }
        public string Description { get; set; }
        // Contains a value from the GameState Enum in this file
        public GameState State { get; set; }
    }
}
