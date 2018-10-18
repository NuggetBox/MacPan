using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Enemy : GameObject
    {
        public Enemy()
        {
            Color = ConsoleColor.DarkRed;
            Position = new Point(10, 10);
            Game.GameObjects[Position.X, Position.Y] = this;
            Draw();
        }

        public override void Update()
        {            
            int moveDir = 0;
            moveDir = Program.rng.Next(1, 5);

            Console.ReadKey(true);
            OldPosition = Position;

            switch (moveDir)
            {
                case 1:
                    if (Game.GameObjects[Position.X, Position.Y + 1] == null)
                    {
                        Position = new Point(Position.X, Position.Y + 1);
                    }
                    break;
                case 2:
                    if (Game.GameObjects[Position.X, Position.Y - 1] == null)
                    {
                        Position = new Point(Position.X, Position.Y - 1);
                    }
                    break;
                case 3:
                    if (Game.GameObjects[Position.X + 1, Position.Y] == null)
                    {
                        Position = new Point(Position.X + 1, Position.Y);
                    }
                    break;
                case 4:
                    if (Game.GameObjects[Position.X - 1, Position.Y] == null)
                    {
                        Position = new Point(Position.X - 1, Position.Y);
                    }
                    break;
            }

            // Om det är en player på vår utberäknade position så ska vi inte flytta dit utan busta player, och stanna kvar på OldPosition
        }
    }
}
