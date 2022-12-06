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

        private ObservableCollection<Game> _games = new ObservableCollection<Game>();

        public ObservableCollection<Game> Games { get {

                //NO TYRONE V COPS ALLOWED
                foreach (var game in _games)
                {
                    if ((game.Name.ToUpper()).Contains(("Tyrone").ToUpper()))
                    {
                        _games.Remove(game);
                        break;
                    }

                }


                return _games; } }

        public GameManager()
        {
            _steamLauncher = new Steam();
            _db = new DatabaseManager();

            _db.CheckDatabase();

            //Added extra update so that games are populated from stored data
            UpdateFromDB();
            UpdateAll();
        }

        public void SaveAllGamesOnClose()
        {
            foreach(Game game in _games)
            {
                Debug.WriteLine("Saving Game");
                _db.UpdateGame(game);
            }
        }

        public void SaveGame(Game game)
        {
            _db.UpdateGame(game);
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
            _games = new ObservableCollection<Game>();
            _db.ReadAllGames(Games);
            foreach(Game game in _games)
            {
                _steamLauncher.GetGameNews(game);
                _steamLauncher.GetGameAchievments(game);
            }
            SortGamesListByName();
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

        public ObservableCollection<Game> GetRecentlyPlayed()
        {
            ObservableCollection<Game> recentGames = new ObservableCollection<Game>();
            
            foreach (var game in Games)
            {
                recentGames.Add(game);
            }

            for(int x = 0; x < recentGames.Count; x++)
            {
                int newestIndex = x;
                for(int y = x; y < recentGames.Count; y++)
                {
                    if (recentGames[y]._lastplayed > recentGames[newestIndex]._lastplayed)
                    {
                        newestIndex = y;
                    }
                }
                Game temp = recentGames[newestIndex];
                recentGames[newestIndex] = recentGames[x];
                recentGames[x] = temp;
            }

            return recentGames;
        }

        /// <summary>
        /// Sorts Games property by name
        /// </summary>
        public void SortGamesListByName()
        {
            for (int x = 0; x < _games.Count; x++)
            {
                int newestIndex = x;
                for (int y = x; y < _games.Count; y++)
                {
                    if (_games[y].Name.CompareTo(_games[newestIndex].Name) < 0)
                    {
                        newestIndex = y;
                    }
                }
                Game temp = _games[newestIndex];
                _games[newestIndex] = _games[x];
                _games[x] = temp;
            }
        }
    }
}
