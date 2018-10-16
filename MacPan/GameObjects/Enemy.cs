using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Enemy : GameObject
    {
        public override void Update()
        {            
            int moveDir = 0;
            int move = 2;
            moveDir = Program.rng.Next(1, 5);
            Console.ReadKey(true);
            Game.GameObjects[Position.X, Position.Y] = null;
            
            switch (moveDir)
            {
                case 1:
                    if (Game.GameObjects[Position.X,Position.Y+move] == null)
                    {
                        Position = new Point(Position.X, Position.Y + move);
                    }
                    break;
                case 2:
                    if (Game.GameObjects[Position.X, Position.Y - move] == null)
                    {
                        Position = new Point(Position.X, Position.Y-move);
                    }
                    break;
                case 3:
                    if (Game.GameObjects[Position.X + move, Position.Y] == null)
                    {
                        Position = new Point(Position.X + move, Position.Y);
                    }
                    break;
                case 4:
                    if (Game.GameObjects[Position.X - move, Position.Y] == null)
                    {
                        Position = new Point(Position.X - move, Position.Y);
                    }
                    break;

            }
            Game.GameObjects[Position.X, Position.Y] = this;
        }

        public override void Draw()
        {
            int startX = Position.X + 1;
            int startY = Position.Y + 1;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(startX, startY);
            Console.Write("██");
            Console.SetCursorPosition(startX, startY+1);
            Console.Write("██");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
