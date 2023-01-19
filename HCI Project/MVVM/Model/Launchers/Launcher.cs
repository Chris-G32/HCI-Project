//DONE WITH RESTRUCTURE AND COMMENT
using HCI_Project.MVVM.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_Project.MVVM.Model
{
    /// <summary>
    /// Enum type for distinguishing between the launchers
    /// </summary>
    public enum LauncherID
    {
        Steam,
        Epic
    }
    /// <summary>
    /// Provides base requirements expected of all inheriting launcher classes
    /// </summary>
    public abstract class Launcher
    {
        public abstract string Name { get; }
        public abstract LauncherID ID { get; }

        /// <summary>
        /// Launches a game that is owned on a given launcher
        /// </summary>
        /// <returns>True if successfully launched, false otherwise</returns>
        public abstract bool LaunchGame(Game game);

        /// <summary>
        /// Finds all games owned by a user on the corresponding launcher, then updates all entries for those 
        /// games in the database that is passed in to the function.
        /// </summary>
        /// <param name="db"> The database to store the games in </param>
        public abstract Task UpdateGames(DatabaseManager db);

        /// <summary>
        /// Populates an instance of Game from the the API
        /// </summary>
        /// <param name="game">The Game which will be populated with data</param>
        public abstract Task GetGameInfo(Game game);

        /// <summary>
        /// Reads the key from the corresponding file and returns it
        /// </summary>
        /// <returns>The API Key for the launcher</returns>
        protected abstract string GetKey();
        
        // The API key, may not always be necessary
        protected string _key;

        //Allows for games to be launched properly using the commands in browser eg: steam://launch
        protected WebBrowser _browser;
    }
}
