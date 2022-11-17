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
            _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description) VALUES ('{game.Game_ID}', '{game.Name}', {game.Launcher_ID}, '{game.Description}')";
            _cmd.ExecuteNonQuery();
        }

    }
}
