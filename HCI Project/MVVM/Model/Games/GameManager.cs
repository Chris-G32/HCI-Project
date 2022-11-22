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
        private static Launcher_Steam _steamLauncher;

        private static Database_Manager _db;

        public GameManager()
        {
            _steamLauncher = new Launcher_Steam();
            _db = new Database_Manager();
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
