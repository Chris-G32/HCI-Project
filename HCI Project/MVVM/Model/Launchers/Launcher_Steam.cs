using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HCI_Project.MVVM.Model
{
    public class Launcher_Steam:Launcher
    {
        // Client used for API Requests
        private HttpClient client;
        private string _steamid = "";
        private string _steamname = "bay219";
        public Launcher_Steam()
        {
            client = new HttpClient();
            // This accepts a json file by default
            client.DefaultRequestHeaders.Add("accept", "text/json; charset=utf-8");
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

        /// <summary>
        /// Reads from the "steam.secret" file in the same folder as this file and returns the Steam API key
        /// </summary>
        protected override string GetKey()
        {
            String res;
            using (FileStream fileStream = File.Open("../../.././steam.secret", FileMode.Open, FileAccess.Read))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
                res = streamReader.ReadLine();
            return res;
        }

        public override bool LaunchGame(Game game)
        {
            return true;
        }

        public async override Task<List<Game>> FindGames()
        {
            // If the user's SteamID has not been found yet, wait for it to be found first
            if (_steamid == "")
                await PopulateSteamID();

            // The final list of games to be returned
            List<Game> gameObjectsList = new List<Game>();

            // Call the IPlayerService API to get the user's owned games' ids and names
            var resp = await client.GetStringAsync("https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key=" + _key + "&steamid=" + _steamid + "&include_appinfo=true");
            // Converts the API response into usable data (see subclasses below)
            SteamGames games = JsonSerializer.Deserialize<SteamGames>(resp);

            // Converts the deserialized data into Game objects (generic) with the proper Game_IDs, Names, and Launcher_IDs
            foreach (SteamGames.SteamGamesResponse.SteamGame game in games.response.games)
            {
                //Console.WriteLine("Name: " + game.name + "   ID: " + game.appid);
                Game tempGame = new Game(game.appid, game.name, LauncherID.Steam);
                //Adds each Game object to the list of Games to be returned
                gameObjectsList.Add(tempGame);
            }

            return gameObjectsList;
        }

        /// <summary>
        /// Converts a Steam name into a SteamID usable within the Steam API. Stores it in the field _steamid
        /// </summary>
        public async Task PopulateSteamID()
        {
            var idResp = await client.GetStringAsync("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key=" + _key + "&vanityurl=" + _steamname);
            SteamID id = JsonSerializer.Deserialize<SteamID>(idResp);
            _steamid = id.response.steamid;
        }

        public override void GetGameInfo(ref Game game)
        {
            Debug.WriteLine("Getting info about " + game.Name);
        }

        //////
        // BEGIN SUBCLASSES FOR JSON SERIALIZATION
        //////

        /// <summary>
        /// Response class for getting SteamID from Steam API. The subclass, SteamIDResponse, holds the necessary info
        /// </summary>
        private class SteamID
        {
            public SteamIDResponse response { get; set; }

            /// <summary>
            /// Sub-class of SteamID, holds actual ID information
            /// </summary>
            public class SteamIDResponse
            {
                // User's SteamID
                public string steamid { get; set; }
                // status of request (1 for success, 0 for fail)
                public long success { get; set; }
            }
        }

        /// <summary>
        /// A container class holding the information received from the "GetOwnedGames" Steam API Call
        /// </summary>
        private class SteamGames
        {
            public SteamGamesResponse response { get; set; }
            /// <summary>
            /// A subclass, holding the number of games an account owns (game_count) and an array of all games.
            /// These games are stored as objects of the SteamGame subclass
            /// </summary>
            public class SteamGamesResponse
            {
                public long game_count { get; set; }

                public SteamGame[] games { get; set; }
                /// <summary>
                /// Holds basic data for each game (id and name) to be processed and converted into Game objects.
                /// </summary>
                public class SteamGame
                {
                    public int appid { get; set; }
                    public string name { get; set; }
                }
            }
        }
    }
}
