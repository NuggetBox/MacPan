using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    public abstract class GameObject
    {
        readonly Point offset = new Point(0, 16);

        public Point Position { get; set; }
        public Point OldPosition { get; set; }
        public ConsoleColor Color { get; set; }
        public float MoveDelay { get; set; }

        public virtual void InitialDraw() { }
        public virtual void Update() { }

        public virtual void Draw()
        {
            // If the GameObject's position has changed since the last frame, that GameObject is drawn.
            if (!OldPosition.Equals(Position))
            {
                // To prevent 2 enemies from magically pathing into each other, we must make sure they don't.
                if (this is Enemy)
                {
                    for (int x = -2; x < 3; ++x)
                    {
                        for (int y = -2; y < 3; ++y)
                        {
                            if (Game.GameObjects[Position.X + x, Position.Y + y] == null)
                                continue;
                            if (!(Game.GameObjects[Position.X + x, Position.Y + y] == this))
                                if (Game.GameObjects[Position.X + x, Position.Y + y].Position.Equals(Position))
                                    Position = OldPosition;
                        }
                    }
                }
                Game.GameObjects[OldPosition.X, OldPosition.Y] = null;
                Game.GameObjects[Position.X, Position.Y] = this;

                Erase();
                Console.ForegroundColor = Color;
                for (int i = 0; i < Game.BoxSize.X; ++i)
                {
                    for (int j = 0; j < Game.BoxSize.Y; ++j)
                    {
                        Console.SetCursorPosition(Game.BoxSize.X * Position.X + i + offset.X, Game.BoxSize.Y * Position.Y + j + offset.Y);
                        Console.Write("█");
                        Stats.stats["Boxes"].Add(1);
                    }
                }
            }
        }

        // Erases the GameObject at its previous location.
        public void Erase()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < Game.BoxSize.X; ++i)
            {
                for (int j = 0; j < Game.BoxSize.Y; ++j)
                {
                    Console.SetCursorPosition(Game.BoxSize.X * OldPosition.X + i + offset.X, Game.BoxSize.Y * OldPosition.Y + j + offset.Y);
                    Console.Write("█");
                }
            }
        }
    }
}
