using System;
using System.Collections.Generic;
using System.Text;

namespace TestingApp
{
    public enum GameState
    {
        OWNED,
        INSTALLED,
        NOT_OWNED
    }
    public struct Game
    {
        public int Game_ID { get; set; }
        public string Name { get; set; }
        // Contains a value from the LauncherID enum from Launcher.cs
        public LauncherID Launcher_ID { get; set; }
        public String[] Tags { get; set; }
        public String[] Images { get; set; }
        public String Description { get; set; }
        // Contains a value from the GameState Enum in this file
        public GameState State { get; set; }
    }
}
