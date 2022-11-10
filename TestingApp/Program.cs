using System;
using System.Threading.Tasks;

namespace TestingApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Launcher_Steam steam = new Launcher_Steam();
            Game test_game = new Game();
            test_game.Game_ID = 1714040;
            test_game.Name = "Super Auto Pets";
            steam.LaunchGame(test_game);
            await steam.FindGames();
        }
    }
}
