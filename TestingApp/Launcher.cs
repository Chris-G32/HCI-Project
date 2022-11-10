using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp
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
        public abstract Task<List<Game>> FindGames();
        /// <summary>
        /// Populates an instance of Game from the Database or the API
        /// </summary>
        /// <param name="game">The Game which will be populated with data</param>
        public abstract void GetGameInfo(ref Game game);
        public abstract String GetKey();
        // The API key
        protected String _key;
    }
}
