using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    // █

    class Program
    {
        public static Random rng = new Random();
        public static string Path { get; private set; }

        static void Main(string[] args)
        {
            Path = Environment.CurrentDirectory;
            
            
            Menu menu = new Menu(0);

            Console.CursorVisible = false;
            Stats.SaveStats();

            while (true)
            {
                menu.Update();
            }
        }
    }
}
