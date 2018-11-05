using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Trophy : GameObject
    {
        public int Value { get; private set; }

        public Trophy(/*int value*/)
        {
            Color = ConsoleColor.Yellow;
            Position = new Point(20, 20);
            Game.GameObjects[Position.X, Position.Y] = this;
            Draw();
            //Value = value;
        }
    }
}
