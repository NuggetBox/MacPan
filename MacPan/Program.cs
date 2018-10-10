using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    class Program
    {
        public static Random rng = new Random();
        public static string path;

        static void Main(string[] args)
        {
            path = Environment.CurrentDirectory + "/Stats/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<Button> buttons = new List<Button>();
            Menu menu = new Menu(buttons);

            Console.CursorVisible = false;
            Stats.SaveStats();

            while(true)
            {
                menu.Update();
            }
        }
    }
}
