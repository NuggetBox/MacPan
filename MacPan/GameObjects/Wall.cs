using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    // A wall.
    class Wall : GameObject
    {
        public Wall()
        {
            Color = ConsoleColor.Gray;
        }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public override void Draw() { }
    }
}
