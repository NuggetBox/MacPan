using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Program
    {
        public static Random rng = new Random();

        static void Main(string[] args)
        {
            List<Button> buttons = new List<Button>();
            Menu menu = new Menu(buttons);

            Console.CursorVisible = false;

            while(true)
            {
                menu.Update();
            }
        }
    }
}
