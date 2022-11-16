using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_Project.MVVM.Model
{
    public enum LauncherID
    {
        Steam,
        Epic
    }
    public abstract class Launcher
    {
        public abstract string Name { get; }
        public abstract LauncherID ID { get; }

        public abstract bool LaunchGame(Game game);

        /// <summary>
        /// Find and return the List of Game objects which the user owns on this launcher
        /// </summary>
        /// <returns>List of Games owned by user</returns>
        public abstract Task<List<Game>> FindGames();

        /// <summary>
        /// Populates an instance of Game from the Database or the API
        /// </summary>
        /// <param name="game">The Game which will be populated with data</param>
        public abstract void GetGameInfo(ref Game game);

        /// <summary>
        /// Reads the key from the corresponding file and returns it
        /// </summary>
        /// <returns>The API Key for the launcher</returns>
        protected abstract String GetKey();
        // The API key
        protected String _key;
        protected WebBrowser browser;
    }
}
