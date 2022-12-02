using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using HCI_Project.MVVM.Model.Database;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HCI_Project.MVVM.Model
{
    public class Steam:Launcher
    {
        // Client used for API Requests
        private HttpClient client;

        //76561197960265728 +32bit steam id, found at C:\Program Files (x86)\Steam\userdata is the 64bit id
        private string _steamid = "76561198863942684";
        private string _steamname = "";
        public Steam()
        {
            //_browser=new System.Windows.Controls.WebBrowser();
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
            string res;
            using (FileStream fileStream = File.Open("../../.././steam.secret", FileMode.Open, FileAccess.Read))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
                res = streamReader.ReadLine();
            return res;
        }

        public override bool LaunchGame(Game game)
        {
            _browser = new System.Windows.Controls.WebBrowser();
            _browser.Navigate(new Uri("steam://rungameid/" + game.Game_ID));
            _browser.Dispose();
            game.State = GameState.INSTALLED;
            return true;
        }

        /// <summary>
        /// Updates all entries related to the Steam launcher in the database that is passed in to the function.
        /// </summary>
        public async override Task UpdateGames(DatabaseManager db)
        {
            // If the user's SteamID has not been found yet, wait for it to be found first
            if (_steamid == "")
                await PopulateSteamID();

            // Call the IPlayerService API to get the user's owned games' ids and names
            var resp = await client.GetStringAsync("https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key=" + _key + "&steamid=" + _steamid + "&include_appinfo=true");
            // Converts the API response into usable data (see subclasses below)
            SteamGames games = JsonSerializer.Deserialize<SteamGames>(resp);

            // Converts the deserialized data into Game objects (generic) with the proper Game_IDs, Names, and Launcher_IDs
            foreach (var game in games.response.games)
            {
                // Removes all apostrophes from a games title due to issues with database
                while(game.name.Contains("'"))
                {
                    int index = game.name.IndexOf("'");
                    game.name = game.name.Remove(index, 1);
                }
                Game tempGame = new Game(game.appid.ToString(), game.name, LauncherID.Steam);
                tempGame.IconImage = new Uri("http://media.steampowered.com/steamcommunity/public/images/apps/" + game.appid.ToString() + "/" + game.img_icon_url + ".jpg");
                // Populates the Game object with more detailed data from the API
                await GetGameInfo(tempGame);
                db.InsertGame(tempGame);
            }
        }

        /// <summary>
        /// Converts a Steam name into a SteamID usable within the Steam API. Stores it in the field _steamid
        /// </summary>
        public async Task PopulateSteamID()
        {
            char[] id = new char[29];
            int x;
            int i = 0;
            char ch;
            //read in the file containing the users steam id
            StreamReader reader;
            reader = new StreamReader(@"C:\Program Files (x86)\Steam\config\loginusers.vdf");
            while (i < 29)
            {
                //read in the file
                ch = (char)reader.Read();
                // converts each character into its int value
                x = ch - '0';

                // loop that goes through the first 29 spcaes in file and reads in only the steam id by making sure sure each character read in is a digit from 1-9
                if (x <= 9 && x >= 0 && i < 29)
                {
                    // puts each digit of the ssteamid into the array
                    id[i] = ch;

                }
                i++;
            }

            //converts the char array into string
            string temp = new string(id);

            // set the steam id to the strong read in from file
            _steamid = temp;


            //var idResp = await client.GetStringAsync("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key=" + _key + "&vanityurl=" + _steamname);
            //SteamID id = JsonSerializer.Deserialize<SteamID>(idResp);
            //_steamid = id.response.steamid;
        }

        /// <summary>
        /// Populates a game with a known game id with its information
        /// </summary>
        /// <param name="game">Game to store info to. Passed by reference.</param>
        public override async Task GetGameInfo(Game game)
        {
            // Gets game info from steam api
            try
            {
                var resp = await client.GetStringAsync("https://store.steampowered.com/api/appdetails?appids=" + game.Game_ID);

                // Parses the json response using JTokens due to complex response nature
                JToken outer = JToken.Parse(resp);
                JObject inner = outer[game.Game_ID].Value<JObject>();
                if (inner["success"].Value<bool>() == false)
                {
                    return;
                }
                JObject data = inner["data"].Value<JObject>();

                // Sets game info from the parsed response
                game.HeaderImage = new Uri(data["header_image"].Value<string>());
                game.Short_Description = data["short_description"].Value<string>();

                while (game.Short_Description.Contains("'"))
                {
                    int index = game.Short_Description.IndexOf("'");
                    game.Short_Description = game.Short_Description.Remove(index, 1);
                }

                // Adds each genre of the game as a tag
                try
                {
                    foreach (var k in data["genres"])
                    {
                        game.Tags.Add(k["description"].Value<string>());
                    }
                }
                catch
                {
                    Debug.WriteLine("Error! No tags!");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }

            //foreach(var k in data)
            //{
            //    Debug.WriteLine("**************************************************************************");
            //    Debug.WriteLine(k);
            //}
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
                /// Holds basic data for each game (id, name, image icon hash) to be processed and converted into Game objects.
                /// </summary>
                public class SteamGame
                {
                    public long appid { get; set; }
                    public string name { get; set; }
                    public string img_icon_url { get; set; }
                }
            }
        }
    }
}
