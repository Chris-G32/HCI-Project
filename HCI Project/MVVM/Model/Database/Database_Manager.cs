using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace HCI_Project.MVVM.Model.Database
{
    public class Database_Manager
    {

        SQLiteConnection _con;
        SQLiteCommand _cmd;

        /// <summary>
        /// Establishes connection to SQLite database file on object instantiation
        /// </summary>
        public Database_Manager()
        {
            // Specifies file directory
            string cs = @"URI=file:../../../data.db";

            // connect to database
            _con = new SQLiteConnection(cs);
            _con.Open();

            _cmd = new SQLiteCommand(_con);
        }

        /// <summary>
        /// Inserts a Game object into the database, regardless of whether it was there before or not
        /// </summary>
        public void Insert_Game(Game game)
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
        public Game Read_Game(string id)
        {
            _cmd.CommandText = $"SELECT * FROM games WHERE id='{id}'";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            Game res = null;

            if(rdr.Read())
            {
                string game_id = rdr.GetString(0);
                string game_name = rdr.GetString(1);
                int launcher_id = rdr.GetInt32(2);
                string description = rdr.GetString(3);
                res = new Game(game_id, game_name, (LauncherID)launcher_id);
                res.Description = description;
            }

            return res;

        }

    }
}
