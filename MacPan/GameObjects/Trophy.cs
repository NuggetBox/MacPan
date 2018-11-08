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
            //Value = value;
        }

        public override void Update()
        {
            OldPosition = Position;
        }

        public override void Draw() { }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public void PickUp()
        {
            Erase();
            Game.GameObjects[Position.X, Position.Y] = null;
            Stats.stats["Trophies"].Add(1);
        }
    }
}
