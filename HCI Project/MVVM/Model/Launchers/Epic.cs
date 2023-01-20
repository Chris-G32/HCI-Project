using HCI_Project.MVVM.Model.Database;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.MVVM.Model.Launchers
{
    public class Epic : Launcher
    {
        private HttpClient client;
        private string _epicid = "";//"76561198863942684";
        private string _epicname = "";
        public Epic()
        {
            client = new HttpClient();
            // This accepts a json file by default
            client.DefaultRequestHeaders.Add("accept", "text/json; charset=utf-8");
            //This communicates our API Key
            client.DefaultRequestHeaders.Add("Authorization", "EPICGAMESWEBAPIKEY");
            _key = GetKey();

        }

        public override bool LaunchGame(Game game) {
            _browser = new System.Windows.Controls.WebBrowser();
            _browser.Navigate(new Uri($"com.epicgames.launcher://apps/{game.Game_ID}?action=launch&silent=true"));
            _browser.Dispose();
            return true;

            //https://www.epicgames.com/id/api/account/LdyOK-4sGS5baJVSDTywnV0CebA/games
        }
        public override Task UpdateGames(DatabaseManager db) {
            
        }
        /// <summary>
        /// Populates a game with a known game id with its information
        /// </summary>
        /// <param name="game">Game to store info to. Passed by reference.</param>
        public override async Task GetGameInfo(Game game)
        {
        }
        protected override string GetKey()
        {

        }
        public override string Name
        {
            get
            {
                return "Epic Games";
            }
        }

        public override LauncherID ID
        {
            get
            {
                return LauncherID.Epic;
            }
        }
    }
}
