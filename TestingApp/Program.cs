using System;

namespace TestingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Launcher_Steam steam = new Launcher_Steam();
            Console.WriteLine(steam.GetKey());
        }
    }
}
