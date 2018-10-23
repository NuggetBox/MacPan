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
            if (!Directory.Exists(Path + @"/Stats/"))
            {
                Directory.CreateDirectory(Path + @"/Stats/");
            }

            List<Button> buttons = new List<Button>();
            Menu menu = new Menu(buttons);

            Console.CursorVisible = false;
            Stats.SaveStats();

            while (true)
            {
                menu.Update();
            }
        }
    }
}
