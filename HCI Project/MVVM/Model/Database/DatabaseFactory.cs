﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;

namespace HCI_Project.MVVM.Model.Database
{
    public class DatabaseFactory
    {
        /// <summary>
        /// This command builds a SQLite database in the file 'data.db' in the database directory of the model folder.
        /// It should only be run on the first time startup of the app. If run again, it will clear the database and start fresh.
        /// </summary>
        public static void BuildTables(SQLiteCommand cmd)
        {
            // Avoid errors by dropping tables if they already exists
            cmd.CommandText = "DROP TABLE IF EXISTS games";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DROP TABLE IF EXISTS game_tags";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DROP TABLE IF EXISTS settings";
            cmd.ExecuteNonQuery();

            // Creates table for storing game information
            cmd.CommandText = @"CREATE TABLE games(id varchar(20) PRIMARY KEY, name varchar(50), launcher_id INTEGER, description TEXT, images_folder TEXT, screenshots_folder TEXT)";
            cmd.ExecuteNonQuery();

            // Creates table for storing game categories
            cmd.CommandText = @"CREATE TABLE game_tags(id varchar(20), tag varchar(20), FOREIGN KEY (id) REFERENCES games(id))";
            cmd.ExecuteNonQuery();

            // Creates table for storing user setting restore points
            cmd.CommandText = @"CREATE TABLE settings(date_set DATETIME DEFAULT CURRENT_TIMESTAMP PRIMARY KEY)";
            cmd.ExecuteNonQuery();

            Debug.WriteLine("Table 'games' and 'settings' created");
        }
    }
}
