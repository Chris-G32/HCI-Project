using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace HCI_Project.MVVM.Model.Database
{
    public class Database_factory
    {
        /// <summary>
        /// This command builds a SQLite database in the file 'data.db' in the database directory of the model folder.
        /// It should only be run on the first time startup of the app. If run again, it will clear the database and start fresh.
        /// </summary>
        public void BuildTables()
        {
            // Specifies file directory
            string cs = @"URI=file:../../../data.db";

            // connect to database
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            // Avoid errors by dropping the table if it already exists
            cmd.CommandText = "DROP TABLE IF EXISTS games";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE games(id varchar(20) PRIMARY KEY, name varchar(50), launcher_id varchar(10), description TEXT, images TEXT, last_played DATETIME DEFAULT CURRENT_TIMESTAMP)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table 'games' created");
        }
    }
}
