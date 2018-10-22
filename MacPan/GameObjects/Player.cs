using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MacPan
{
    class Player : GameObject
    {
        Stopwatch stopwatch = new Stopwatch();

        ConsoleKey input;

        const ConsoleKey
            up = ConsoleKey.UpArrow,
            down = ConsoleKey.DownArrow,
            left = ConsoleKey.LeftArrow,
            right = ConsoleKey.RightArrow;

        public Player()
        {
            Color = ConsoleColor.Cyan;
            MoveDelay = 0.1f;
            Position = new Point(10, 10);
            Game.GameObjects[Position.X, Position.Y] = this;
            Draw();
        }
        
        public override void Update()
        {
            OldPosition = Position;

            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }

            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true).Key;

                if (stopwatch.ElapsedMilliseconds >= MoveDelay * 1000)
                {
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

                    // SKATT PÅ BERÄKNADE POSITION SHIT ETC.

                    stopwatch.Reset();
                }
            }
        }
    }
}
