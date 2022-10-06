using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO.Compression;
using HCI_Project.Utilities;
using HCI_Project.MVVM.Model;
using System.Windows.Controls;

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    {
        public RelayCommand GetGames { get; set; }
        public SteamHandler steamHandler=new SteamHandler();

        private String _gamesList="";
        public String GamesList { get { return _gamesList; } set { _gamesList = value;OnPropertyChanged(); } }

        private static object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            GetGames = new RelayCommand(o =>
            {
                GetSteamGames();
            });
        }

        //Use Steam Web Api
        //API Key=
        public async void GetSteamGames()
        {
            string steamInstallPath = "C:\\Program Files (x86)\\Steam";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("user-agent", "HCIProject");
            client.DefaultRequestHeaders.Add("accept-encoding", "gzip");
            client.DefaultRequestHeaders.Add("accept", "text/html,application/json");

            //This should be secure in future
            const string WEBAPIKEY = "328A00E3906B517D7F065F4D40D5AD3F";
            //My Steam ID
            string steamID = "76561198863942684";
            string link = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={WEBAPIKEY}&steamid={steamID}&include_appinfo=true";
            
            var userGamesCompressed=await client.GetByteArrayAsync(link);
            //"steam://rungameid/1714040"
            WebBrowser a = new WebBrowser();
            a.Navigate("steam://rungameid/111");
            a.Dispose();
            client.Dispose();
            var gamesAsBytes = GZipCompressor.Decompress(userGamesCompressed);
            //GamesList = Encoding.UTF8.GetString(gamesAsBytes);
            var gamesDoc=JsonDocument.Parse(Encoding.UTF8.GetString(gamesAsBytes));
            var allGameData=gamesDoc.RootElement.GetProperty("response").GetProperty("games");
            var len = allGameData.GetArrayLength();
            for (int i=0;i<len;i++)
            {
                GamesList+=allGameData[i].GetProperty("name")+": "+ allGameData[i].GetProperty("appid")+"\n";
            }
            
            Debug.WriteLine("");
        }
    }
}
