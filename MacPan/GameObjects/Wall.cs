using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Wall : GameObject
    {
        readonly ConsoleColor wallColor = ConsoleColor.DarkGray;

        public override void InitialDraw()
        {
            Console.ForegroundColor = wallColor;

            for (int x = 0; x < Game.GridSize.X; ++x)
            {
                for (int y = 0; y < Game.GridSize.Y; ++y)
                {
                    if (Game.GameObjects[x, y] == null)
                        continue;
                    if (Game.GameObjects[x, y] == this)
                        Position = new Point(x, y);
                }
            }

            for (int i = 0; i < Game.BoxSize.X; ++i)
            {
                for (int j = 0; j < Game.BoxSize.Y; ++j)
                {
                    Console.SetCursorPosition(2 * Position.X + i, 2 * Position.Y + j);
                    Console.Write("█");
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
