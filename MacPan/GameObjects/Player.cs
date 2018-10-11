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
            Update();
            Draw();
        }

        public new void Update()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
        }

        public new void Draw()
        {

        }
    }
}
