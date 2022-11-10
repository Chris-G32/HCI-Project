using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Launcher_Steam steam = new Launcher_Steam();
            Game test_game = new Game(1714040, "Super Auto Pets");
            steam.LaunchGame(test_game);
            List<Game> games = await steam.FindGames();
            foreach(Game game in games)
            {
                Console.WriteLine(game.Name + ": " + game.Game_ID);
            }
        }
    }
}
