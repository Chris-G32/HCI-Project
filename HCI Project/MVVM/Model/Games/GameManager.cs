using HCI_Project.MVVM.Model.Database;
using System;
using System.Collections.Generic;
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

        public GameManager()
        {
            _steamLauncher = new Steam();
            _db = new DatabaseManager();
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
        public async Task UpdateAll()
        {
            await _steamLauncher.UpdateGames(_db);
        }
    }
}
