using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MacPan
{
    // █

    class Program
    {
        public static Stopwatch gameTime = new Stopwatch();
        public static Random rng = new Random();
        public static string Path { get; private set; }
        public static string GameName { get; private set; }
        public static string GameNameArt { get; private set; }
        public static string GameNameArt2 { get; private set; }

        static void Main(string[] args)
        {
            Path = Environment.CurrentDirectory;
            Console.ForegroundColor = ConsoleColor.White;
            GameName = "MacPan";
            GameNameArt = "\n  █     █     █      ████   ███     █     █   █" +
                          "\n  ██   ██    █ █    █      █   █   █ █    ██  █" +
                          "\n  █ █ █ █   █   █   █      ████   █   █   █ █ █" +
                          "\n  █  █  █  █     █   ████  █     █     █  █  ██";

            GameNameArt2 = "\n           █     █     █      ████   ███     █     █   █" +
                           "\n           ██   ██    █ █    █      █   █   █ █    ██  █" +
                           "\n           █ █ █ █   █   █   █      ████   █   █   █ █ █" +
                           "\n           █  █  █  █     █   ████  █     █     █  █  ██";

            Menu.MenuCreator(0);
            Stats.AddStats();

            Console.Title = GameName;
            Console.CursorVisible = false;
            gameTime.Start();

            Console.WriteLine(GameNameArt);

            Console.WriteLine("\nPlease change the font to 'Consolas' and the font size to '16' to avoid any issues");
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);

            while (true)
            {
                Menu.Update();
            }
        }
    }
}
