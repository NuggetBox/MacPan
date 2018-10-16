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
        Point oldPos;

        ConsoleKey input;

        const ConsoleKey
            up = ConsoleKey.UpArrow,
            down = ConsoleKey.DownArrow,
            left = ConsoleKey.LeftArrow,
            right = ConsoleKey.RightArrow;

        public Player()
        {
            Color = ConsoleColor.Cyan;
            Position = new Point(0, 0);
            Game.GameObjects[Position.X, Position.Y] = this;
            ThreadedDraw();
        }
        
        public void UpdateDraw()
        {
            while (true)
            {
                ThreadedUpdate();
                ThreadedDraw();
            }
        }

        void ThreadedUpdate()
        {
            input = Console.ReadKey(true).Key;
            Game.GameObjects[Position.X, Position.Y] = null;
            oldPos = Position;

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

            // Om det är en skatt på vår beräknade position ska vi plocka upp och sätta tillbaka position till oldPos innan vi ritar ut.

            Game.GameObjects[Position.X, Position.Y] = this;
        }

        void ThreadedDraw()
        {
            Erase();

            Console.ForegroundColor = Color;
            for (int i = 0; i < Game.BoxSize.X; ++i)
            {
                for (int j = 0; j < Game.BoxSize.Y; ++j)
                {
                    Console.SetCursorPosition(Game.BoxSize.X * Position.X + i, Game.BoxSize.Y * Position.Y + j);
                    Console.Write("█");
                }
            }
        }

        void Erase()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < Game.BoxSize.X; ++i)
            {
                for (int j = 0; j < Game.BoxSize.Y; ++j)
                {
                    Console.SetCursorPosition(Game.BoxSize.X * oldPos.X + i, Game.BoxSize.Y * oldPos.Y + j);
                    Console.Write("█");
                }
            }
        }
    }
}
