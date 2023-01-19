//DONE WITH RESTRUCTURE AND COMMENT
using HCI_Project.MVVM.Model.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.MVVM.Model
{
    public class GameManager
    {
        /// <summary>
        /// Provides methods for interacting with the steam api to source game information and user info.
        /// </summary>
        private static Steam _steamLauncher;
        //Support updating steamId Jankily

        /// <summary>
        /// Provides methods for updating the database to the manager
        /// </summary>
        private static DatabaseManager _db;

        /// <summary>
        /// All of the games from all launchers. Not sorted in any particular order.
        /// </summary>
        private ObservableCollection<Game> _games = new ObservableCollection<Game>();

        /// <summary>
        /// All of the games from all launchers. Not sorted in any particular order.
        /// </summary>
        public ObservableCollection<Game> Games { 
            get { return _games; }
        }

        /// <summary>
        /// Creates a new game manager. This is capable of managing the database and relaying data to where it needs to go.
        /// </summary>
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
        /// Updates all games to database upon closing the app
        /// </summary>
        public void SaveAllGamesOnClose()
        {
            foreach(Game game in _games)
            {
                Debug.WriteLine("Saving Game");
                _db.UpdateGame(game);
            }
        }

        /// <summary>
        /// Saves a game to the database
        /// </summary>
        public void SaveGame(Game game)
        {
            _db.UpdateGame(game);
        }

        /// <summary>
        /// Launches any game using the proper launcher
        /// </summary>
        public void LaunchGame(Game game)
        {
            switch (game.Launcher_ID)
            {
                case LauncherID.Steam:
                {
                    _steamLauncher.LaunchGame(game);
                    break;
                }
                case LauncherID.Epic:
                     goto default; 
                default:
                {
                    Debug.WriteLine("Game Launcher not yet implemented...");
                    break;
                }
            }
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

        /// <summary>
        /// Gets the most recently played games regardless of launcher
        /// </summary>
        /// <returns>A list of the most recently played games in order of most recent to least recent</returns>
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
