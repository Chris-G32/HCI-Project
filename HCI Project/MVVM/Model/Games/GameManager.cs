using HCI_Project.MVVM.Model.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.MVVM.Model
{
    public class GameManager
    {
        private static Steam _steamLauncher;

        private static DatabaseManager _db;

        private List<Game> _games = new List<Game>();

        public List<Game> Games { get { return _games; } }

        public GameManager()
        {
            _steamLauncher = new Steam();
            _db = new DatabaseManager();

            _db.CheckDatabase();

            //Added extra update so that games are populated from stored data
            UpdateFromDB();
            UpdateAll();
        }

        /// <summary>
        /// Launches any game using the proper launcher
        /// </summary>
        public void LaunchGame(Game game)
        {
            if (game.Launcher_ID == LauncherID.Steam)
                _steamLauncher.LaunchGame(game);
            else
                Debug.WriteLine("Game Launcher not yet implemented...");
                
        }

        /// <summary>
        /// Updates games on all currently configured launchers in the database
        /// </summary>
        public async void UpdateAll()
        {
            await _steamLauncher.UpdateGames(_db);
            UpdateFromDB();
            return;
        }


        public void UpdateFromDB()
        {
            _games = _db.ReadAllGames();
        }

        /// <summary>
        /// Search the list of all games to return any that match the given query in their name
        /// </summary>
        /// <param name="query"> The string to search for </param>
        /// <returns> A list of games whose names contain the query </returns>
        public ObservableCollection<Game> SearchByName(string query)
        {
            ObservableCollection<Game> res = new ObservableCollection<Game>();

            query = (query == null) ? "" : query;

            foreach(var game in _games)
            {
                if ((game.Name.ToLower()).Contains(query.ToLower()))
                {
                    res.Add(game);
                }
            }

            return res;
        }
    }
}
