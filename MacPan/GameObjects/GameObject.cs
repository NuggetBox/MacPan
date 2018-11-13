using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    public abstract class GameObject
    {
        readonly Point Offset = new Point(0, 16);

        public Point Position { get; set; }
        public Point OldPosition { get; set; }
        public ConsoleColor Color { get; set; }
        public float MoveDelay { get; set; }

        public virtual void InitialDraw() { }
        public virtual void Update() { }

        public virtual void Draw()
        {
            if (!OldPosition.Equals(Position))
            {
                Game.GameObjects[OldPosition.X, OldPosition.Y] = null;
                Game.GameObjects[Position.X, Position.Y] = this;
                Erase();
                Console.ForegroundColor = Color;
                for (int i = 0; i < Game.BoxSize.X; ++i)
                {
                    for (int j = 0; j < Game.BoxSize.Y; ++j)
                    {
                        Console.SetCursorPosition(Game.BoxSize.X * Position.X + i + Offset.X, Game.BoxSize.Y * Position.Y + j + Offset.Y);
                        Console.Write("█");
                        Stats.stats["Boxes"].Add(1);
                    }
                }
            }
        }

        public void Erase()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < Game.BoxSize.X; ++i)
            {
                for (int j = 0; j < Game.BoxSize.Y; ++j)
                {
                    Console.SetCursorPosition(Game.BoxSize.X * OldPosition.X + i + Offset.X, Game.BoxSize.Y * OldPosition.Y + j + Offset.Y);
                    Console.Write("█");
                }
            }
        }
    }
}
