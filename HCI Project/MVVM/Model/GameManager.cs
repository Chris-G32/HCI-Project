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
        public List<Game> games { get; set; }

        private Launcher_Steam _steamLauncher;

        public GameManager()
        {
            _steamLauncher = new Launcher_Steam();
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
        /// Finds games on all currently configured launchers
        /// </summary>
        public async Task FindAllGames()
        {
            List<Game> steamGames = await _steamLauncher.FindGames();
            foreach (Game game in steamGames)
            {
                games.Add(game);
            }
        }
    }
}
