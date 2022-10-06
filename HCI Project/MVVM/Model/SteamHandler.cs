using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HCI_Project.MVVM.Model
{
    public class SteamHandler
    {
        private HttpClient client = new HttpClient();

        private SteamUser user;
        public SteamHandler() { }
        public bool SignIn()
        {
            
            //Steam ID is given after authentication in
            
            return true;
        }
        public void GetGames()
        {

        }
        
    }

    struct SteamUser
    {
        public string steamID;

        //Authentication info
        public string steamUserName;
        public string steamPassword;//Need to figure out secure storage for this
    }
}
