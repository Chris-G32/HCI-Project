using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace HCI_Project.MVVM.Model.Database
{
    public class DatabaseManager
    {

        SQLiteConnection _con;
        SQLiteCommand _cmd;

        /// <summary>
        /// Establishes connection to SQLite database file on object instantiation
        /// </summary>
        public DatabaseManager()
        {
            // Specifies file directory
            string cs = @"URI=file:../../../data.db";

            // connect to database
            _con = new SQLiteConnection(cs);
            _con.Open();

            _cmd = new SQLiteCommand(_con);
        }

        /// <summary>
        /// Checks if any tables exist in the database
        /// </summary>
        /// <returns> True if any tables exist, otherwise false </returns>
        public void CheckDatabase()
        {
            try
            {
                _cmd.CommandText = $"SELECT count(*) FROM sqlite_master WHERE type='table'";
                SQLiteDataReader rdr = _cmd.ExecuteReader();

                if(rdr.Read())
                {
                    if (rdr.GetInt32(0) > 0)
                    {
                        rdr.Close();
                        return;
                    }
                }
                rdr.Close();
                DatabaseFactory.BuildTables(_cmd);
            }
            catch
            {
                DatabaseFactory.BuildTables(_cmd);
                return;
            }
        }

        /// <summary>
        /// Inserts a Game object into the database, regardless of whether it was there before or not
        /// </summary>
        public void InsertGame(Game game)
        {
            // Deletes any existing object with the same id first to avoid conflicts
            _cmd.CommandText = $"DELETE FROM games WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();

            _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description) VALUES ('{game.Game_ID}', '{game.Name}', {(int) game.Launcher_ID}, '{game.Description}')";
            _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Looks for a game in the database with a given ID and returns the populated Game object
        /// </summary>
        /// <returns> The populated Game object corresponding to the id OR null if not found </returns>
        public Game ReadGame(string id)
        {
            _cmd.CommandText = $"SELECT * FROM games WHERE id='{id}'";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            Game res = null;

            if(rdr.Read())
            {
                string gameID = rdr.GetString(0);
                string gameName = rdr.GetString(1);
                int launcherID = rdr.GetInt32(2);
                string description = rdr.GetString(3);
                res = new Game(gameID, gameName, (LauncherID)launcherID);
                res.Description = description;
            }
            rdr.Close();

            GetGameTags(res);

            return res;

        }

        /// <summary>
        /// Populates a game object with the respective tags from the separate table
        /// </summary>
        public void GetGameTags(Game game)
        {
            _cmd.CommandText = $"SELECT tag FROM game_tags WHERE id='{game.Game_ID}'";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            while (rdr.Read())
            {
                game.Tags.Add(rdr.GetString(0));
            }
            rdr.Close();
        }

        /// <summary>
        /// Reads all current games from the database and returns result
        /// </summary>
        /// <returns> A list of all currently existing game objects from the database </returns>
        public List<Game> ReadAllGames()
        {
            List<Game> res = new List<Game>();

            _cmd.CommandText = $"SELECT * FROM games";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            // Creates a game object from each response from the database and stores to return later
            while (rdr.Read())
            {
                string gameID = rdr.GetString(0);
                string gameName = rdr.GetString(1);
                int launcherID = rdr.GetInt32(2);
                string description = rdr.GetString(3);
                res.Add(new Game(gameID, gameName, (LauncherID)launcherID, description));
            }
            rdr.Close();

            // Populates the tags for each game
            foreach (Game game in res)
            {
                GetGameTags(game);
            }

            // Returns the list of games
            return res;
        }

        //public List<Game> SearchGames(string name = null, string)

    }
}
