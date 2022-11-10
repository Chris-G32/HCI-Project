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
            List<Game> games = await steam.FindGames();
            foreach(Game game in games)
            {
                Console.WriteLine(game.Name + ": " + game.Game_ID);
            }
        }
    }
}
