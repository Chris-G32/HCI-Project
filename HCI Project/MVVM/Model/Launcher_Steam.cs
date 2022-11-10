using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;

namespace HCI_Project.MVVM.Model
{
    public class Launcher_Steam:Launcher
    {
        public Launcher_Steam()
        {
            _url = "";
            _key = "";
        }

        public override string Name
        {
            get
            {
                return "Steam";
            }
        }
        public override LauncherID ID
        {
            get
            {
                return LauncherID.Steam;
            }
        }

        public override bool LaunchGame(Game game)
        {
            Debug.WriteLine("Launching " + game.Name + "...");
            return true;
        }

        public override Game[] FindGames()
        {
            Debug.WriteLine("Finding all games from Steam...");
            return null;
        }

        public override void GetGameInfo(ref Game game)
        {
            Debug.WriteLine("Getting info about " + game.Name);
        }
    }
}
