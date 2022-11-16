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
        public Game(int game_ID, string name, LauncherID launcher_ID, string[] tags, string[] images, string description, GameState state)
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
        public Game(string name)
        {
            Name=name;
        }

        /// <summary>
        /// For use in the FindGames function of each launcher to populate with minimum information
        /// </summary>
        public Game(int id, string name, LauncherID launcher)
        {
            Game_ID = id;
            Name = name;
            Launcher_ID = launcher;
        }

        public int Game_ID { get; }
        public string Name { get; }
        // Contains a value from the LauncherID enum from Launcher.cs
        public LauncherID Launcher_ID { get; set; }
        public String[] Tags { get; set; }
        public String[] Images { get; set; }
        public String Description { get; set; }
        // Contains a value from the GameState Enum in this file
        public GameState State { get; set; }
    }
}
