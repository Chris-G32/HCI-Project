using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.Model
{
    public enum LauncherID
    {
        Steam,
        Epic
    }
    public abstract class Launcher
    {
        public abstract string Name { get; }
        public abstract LauncherID id { get; }

        public abstract bool LaunchGame();
        public abstract Game[] FindGames();
        public abstract void GetGameInfo(Game game);
    }
}
