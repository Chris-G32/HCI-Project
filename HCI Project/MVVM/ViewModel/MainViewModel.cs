using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace HCI_Project.MVVM.ViewModel
{
    public class MainViewModel
    {
        public RelayCommand GetGames { get; set; }
        public ObservableCollection<String> Games = new ObservableCollection<string>();
        public MainViewModel()
        {
            GetGames = new RelayCommand(o =>
            {
                GetSteamGames();
            });
        }

        //Use Steam Web Api
        //API Key=
        public static List<String> GetSteamGames()
        {
            string steamInstallPath = "C:\\Program Files (x86)\\Steam";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("user-agent", "HCIProject");
            client.DefaultRequestHeaders.Add("accept-encoding", "gzip");
            client.DefaultRequestHeaders.Add("accept", "text/html");

            //This should be secure in future
            const string WEBAPIKEY = "328A00E3906B517D7F065F4D40D5AD3F";
            //My Steam ID
            string steamID = "76561198863942684";
            string link = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={WEBAPIKEY}&steamid={steamID}&include_appinfo=true";


            return null;
        }
    }
}
