using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestingApp
{
    public class Launcher_Steam : Launcher
    {
        private HttpClient client;
        public Launcher_Steam()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("blah","blah");
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

        public async override Task<Game[]> FindGames()
        {
            var resp=  await client.GetStringAsync("https://httpbin.org/get");
            Console.Write(resp);
            Debug.WriteLine("Finding all games from Steam...");
            return null;
        }

        public override void GetGameInfo(ref Game game)
        {
            Debug.WriteLine("Getting info about " + game.Name);
        }
    }
}
