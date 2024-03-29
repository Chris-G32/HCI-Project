﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Collections.ObjectModel;

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
            string cs = @"URI=file:./data.db";

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
            Debug.WriteLine("Deleting: " + game.Name);
            // Deletes any existing object with the same id first to avoid conflicts
            _cmd.CommandText = $"DELETE FROM games WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();
            // Deletes any tags associated with the game
            _cmd.CommandText = $"DELETE FROM game_tags WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();

            Debug.WriteLine("Inserting: " + game.Name);
            // Inserts the game itself
            if(game.GalleryFolder!= null)
            {
                _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description, header_image_link, icon_image_link, short_desc, playtime, last_played, install_state, screenshots_folder) VALUES ('{game.Game_ID}', '{game.Name}', {(int)game.Launcher_ID}, '{game.Description + " "}', '{game.HeaderImage.ToString()}', '{game.IconImage.ToString()}', '{game.ShortDescription}', {game.PlaytimeHours}, {game._lastplayed}, {(int)game.State}, '{game.GalleryFolder.OriginalString}')";
                _cmd.ExecuteNonQuery();
            }else
            {
                _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description, header_image_link, icon_image_link, short_desc, playtime, last_played, install_state) VALUES ('{game.Game_ID}', '{game.Name}', {(int)game.Launcher_ID}, '{game.Description + " "}', '{game.HeaderImage.ToString()}', '{game.IconImage.ToString()}', '{game.ShortDescription}', {game.PlaytimeHours}, {game._lastplayed}, {(int)game.State})";
                _cmd.ExecuteNonQuery();
            }

            // Inserts all of the games tags
            foreach (string tag in game.Tags)
            {
                _cmd.CommandText = $"INSERT INTO game_tags VALUES ('{game.Game_ID}', '{tag}')";
                _cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGame(Game game)
        {
            Debug.WriteLine("Deleting: " + game.Name);
            // Deletes any existing object with the same id first to avoid conflicts
            _cmd.CommandText = $"DELETE FROM games WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();

            Debug.WriteLine("Inserting: " + game.Name);
            // Inserts the game itself
            

            if (game.GalleryFolder != null)
            {
                Debug.WriteLine(game.GalleryFolder.OriginalString);
                string gallery = game.GalleryFolder.OriginalString.Replace("'", "''");
                _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description, header_image_link, icon_image_link, short_desc, playtime, last_played, install_state, screenshots_folder) VALUES ('{game.Game_ID}', '{game.Name}', {(int)game.Launcher_ID}, '{game.Description + " "}', '{game.HeaderImage.ToString()}', '{game.IconImage.ToString()}', '{game.ShortDescription}', {game.PlaytimeHours}, {game._lastplayed}, {(int)game.State}, '{gallery}')";
                _cmd.ExecuteNonQuery();
            }
            else
            {
                Debug.WriteLine($"No gallery folder for {game.Name}");
                _cmd.CommandText = $"INSERT INTO games (id, name, launcher_id, description, header_image_link, icon_image_link, short_desc, playtime, last_played, install_state) VALUES ('{game.Game_ID}', '{game.Name}', {(int)game.Launcher_ID}, '{game.Description + " "}', '{game.HeaderImage.ToString()}', '{game.IconImage.ToString()}', '{game.ShortDescription}', {game.PlaytimeHours}, {game._lastplayed}, {(int)game.State})";
                _cmd.ExecuteNonQuery();
            }


            // Deletes any tags associated with the game
            _cmd.CommandText = $"DELETE FROM game_tags WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();
            // Deletes the discord link associated with the game
            _cmd.CommandText = $"DELETE FROM links WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();
            // Deletes the game from the hidden list to update it
            _cmd.CommandText = $"DELETE FROM hidden WHERE id='{game.Game_ID}'";
            _cmd.ExecuteNonQuery();

            // Inserts all of the games tags
            foreach (string tag in game.Tags)
            {
                _cmd.CommandText = $"INSERT INTO game_tags VALUES ('{game.Game_ID}', '{tag}')";
                _cmd.ExecuteNonQuery();
            }
            foreach (Uri link in game.SavedLinks)
            {
                // Inserts the discord link
                _cmd.CommandText = $"INSERT INTO links VALUES ('{game.Game_ID}', '{link.ToString()}')";
                _cmd.ExecuteNonQuery();
            }
            if (game.Hidden)
            {
                _cmd.CommandText = $"INSERT INTO hidden VALUES ('{game.Game_ID}')";
                _cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Looks for a game in the database with a given ID and returns the populated Game object
        /// </summary>
        /// <returns> The populated Game object corresponding to the id OR null if not found </returns>
        //public Game ReadGame(string id)
        //{
        //    _cmd.CommandText = $"SELECT * FROM games WHERE id='{id}'";
        //    SQLiteDataReader rdr = _cmd.ExecuteReader();

        //    Game res = null;

        //    if(rdr.Read())
        //    {
        //        string gameID = rdr.GetString(0);
        //        string gameName = rdr.GetString(1);
        //        int launcherID = rdr.GetInt32(2);
        //        string description = rdr.GetString(3);
        //        string headerImage = rdr.GetString(4);
        //        string iconImage = rdr.GetString(5);
        //        string galleryFolder = rdr.GetString(6);
        //        string shortDescription = rdr.GetString(7);
        //        int playtime = rdr.GetInt32(8);
        //        int lastPlayed = rdr.GetInt32(9);
        //        GameState gameState = (GameState)rdr.GetInt32(10); 
        //        res = new Game(gameID, gameName, (LauncherID)launcherID, description, new Uri(headerImage), new Uri(iconImage), shortDescription, playtime, lastPlayed, gameState, new Uri(galleryFolder));
        //    }
        //    rdr.Close();

        //    GetGameTags(res);
        //    GetGameLinks(res);

        //    return res;

        //}

        /// <summary>
        /// Reads a game's related discord server from the database and stores it in the game
        /// </summary>
        /// <param name="game"></param>
        public void GetGameLinks(Game game)
        {
            _cmd.CommandText = $"SELECT link FROM links WHERE id='{game.Game_ID}'";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            while (rdr.Read())
            {
                game.SavedLinks.Add(new Uri(rdr.GetString(0)));
            }
            rdr.Close();
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
        /// Sets the state of a game's Hidden property from the database
        /// </summary>
        /// <param name="game"></param>
        public void GetHidden(Game game)
        {
            _cmd.CommandText = $"SELECT * FROM hidden WHERE id='{game.Game_ID}'";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            if (rdr.Read())
            {
                game.Hidden = true;
            }
            else
            {
                game.Hidden = false;
            }
            rdr.Close();
        }

        /// <summary>
        /// Reads all current games from the database and returns result
        /// </summary>
        /// <returns> A list of all currently existing game objects from the database </returns>
        public void ReadAllGames(ObservableCollection<Game> games, bool showHidden = false)
        {
            _cmd.CommandText = $"SELECT * FROM games";
            SQLiteDataReader rdr = _cmd.ExecuteReader();

            ObservableCollection<Game> tempGames = new ObservableCollection<Game>();

            // Creates a game object from each response from the database and stores to return later
            while (rdr.Read())
            {
                string gameID = rdr.GetString(0);
                string gameName = rdr.GetString(1);
                int launcherID = rdr.GetInt32(2);
                string description = rdr.GetString(3);
                string headerImage = rdr.GetString(4);
                string iconImage = rdr.GetString(5);
                string galleryFolder;
                try
                {
                    galleryFolder = rdr.GetString(6);
                    galleryFolder = galleryFolder.Replace("''", "'");
                }
                catch
                {
                    galleryFolder = null;
                }
                string shortDescription = rdr.GetString(7);
                int playtime = rdr.GetInt32(8);
                int lastPlayed = rdr.GetInt32(9);
                GameState gameState = (GameState)rdr.GetInt32(10);
                tempGames.Add(new Game(gameID, gameName, (LauncherID)launcherID, description, new Uri(headerImage), new Uri(iconImage), shortDescription, playtime, lastPlayed, gameState, galleryFolder));
            }
            rdr.Close();

            // Populates the tags for each game
            foreach (Game game in tempGames)
            {
                GetGameTags(game);
                GetGameLinks(game);
            }


            // Removes hidden games from results if the hidden parameter is not passed, or set to false
            foreach (Game game in tempGames)
            {
                if(showHidden || game.Hidden == false)
                {
                    games.Add(game);
                    continue;
                }
            }
        }

    }
}
