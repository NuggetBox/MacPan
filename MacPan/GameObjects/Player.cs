using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacPan
{
    class Player : GameObject
    {
        public Player()
        {

        }
        
        public void UpdateDraw()
        {
            ThreadedUpdate();
            ThreadedDraw();
        }

        void ThreadedUpdate()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
        }

        void ThreadedDraw()
        {

        }
    }
}
