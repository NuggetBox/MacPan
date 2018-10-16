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
        ConsoleKey input;

        const ConsoleKey
            up = ConsoleKey.UpArrow,
            down = ConsoleKey.DownArrow,
            left = ConsoleKey.LeftArrow,
            right = ConsoleKey.RightArrow;

        public Player()
        {
            Game.GameObjects[5, 5] = this;
        }
        
        public void UpdateDraw()
        {
            ThreadedUpdate();
            ThreadedDraw();
        }

        void ThreadedUpdate()
        {
            input = Console.ReadKey(true).Key;
            Game.GameObjects[Position.X, Position.Y] = null;

            switch (input)
            {
                case up:
                    if (Game.GameObjects[Position.X, Position.Y - 1] == null)
                    {
                        Position = new Point(Position.X, Position.Y - 1);
                    }
                    break;
                case down:
                    if (Game.GameObjects[Position.X, Position.Y + 1] == null)
                    {
                        Position = new Point(Position.X, Position.Y + 1);
                    }
                    break;
                case left:
                    if (Game.GameObjects[Position.X - 1, Position.Y] == null)
                    {
                        Position = new Point(Position.X - 1, Position.Y);
                    }
                    break;
                case right:
                    if (Game.GameObjects[Position.X + 1, Position.Y] == null)
                    {
                        Position = new Point(Position.X + 1, Position.Y);
                    }
                    break;
            }

            Game.GameObjects[Position.X, Position.Y] = this;
        }

        void ThreadedDraw()
        {
            //Console.SetCursorPosition(2 * Position.X, 2 * Position.Y);
            //Console.Write("█");
            //Console.SetCursorPosition(2 * Position.X + 1, 2 * Position.Y);
            //Console.Write("█");
            //Console.SetCursorPosition(2 * Position.X, 2 * Position.Y + 1);
            //Console.Write("█");
            //Console.SetCursorPosition(2 * Position.X + 1, 2 * Position.Y + 1);
            //Console.Write("█");

            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    Console.SetCursorPosition(2 * Position.X + i, 2 * Position.Y + j);
                    Console.Write("█");
                }
            }
        }
    }
}
