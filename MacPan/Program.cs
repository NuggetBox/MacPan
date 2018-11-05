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

        static void Main(string[] args)
        {
            Path = Environment.CurrentDirectory;

            List<Button> buttons = new List<Button>();
            Menu menu = new Menu(buttons);

            Console.CursorVisible = false;
            gameTime.Start();

            while (true)
            {
                menu.Update();
            }
        }
    }
}
