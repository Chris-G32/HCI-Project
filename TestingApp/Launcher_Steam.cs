using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TestingApp
{
    public class Launcher_Steam : Launcher
    {
        public Launcher_Steam()
        {
            _key = GetKey();
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
        
        public override string GetKey()
        {
            String res;
            using (FileStream fileStream = File.Open("../../.././steam.secret", FileMode.Open, FileAccess.Read))
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
                    res = streamReader.ReadLine();
            return res;
        }

        public override bool LaunchGame(Game game)
        {
            Console.WriteLine("steam://rungameid/" + game.Game_ID);
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
