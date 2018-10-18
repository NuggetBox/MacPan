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
            OldPosition = Position;

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

            // Om det är en skatt på vår beräknade position ska vi plocka upp och sätta tillbaka position till OldPosition innan vi ritar ut.
        }

        void ThreadedDraw()
        {
            base.Draw();
        }

        public override void Update() { }
        public override void Draw() { }
    }
}
