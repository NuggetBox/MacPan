using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Goal : GameObject
    {
        public Goal()
        {
            Color = ConsoleColor.Green;
        }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public override void Draw()
        {

        }
    }
}
