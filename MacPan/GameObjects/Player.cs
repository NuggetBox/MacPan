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
            right = ConsoleKey.RightArrow,
            interact = ConsoleKey.Spacebar,
            pause = ConsoleKey.Escape;

        int trophyScore;

        public Player()
        {
            Color = ConsoleColor.Cyan;
            MoveDelay = 0.1f;
            Position = new Point(0, 24);
            Game.GameObjects[Position.X, Position.Y] = this;
            Draw();
        }

        public override void InitialDraw()
        {
            base.Draw();
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

                switch (input)
                {
                    case pause:
                        //pause
                        Stats.SaveStats();
                        break;

                    case interact:
                        bool interacted = false;

                        // LOOP ALL OBJECTS AROUND US AND CHECK IF TROPHY

                        for (int i = -1; i < 2; i += 2)
                        {
                            if (Game.GameObjects[Position.X + i, Position.Y] is Trophy)
                            {
                                (Game.GameObjects[Position.X + i, Position.Y] as Trophy).PickUp();
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Trophy)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Trophy).PickUp();
                                interacted = true;
                            }
                        }

                        if (!interacted)
                        {
                            Stats.stats["InteractNothing"].Add(1);
                        }
                        break;
                }

                if (stopwatch.ElapsedMilliseconds >= MoveDelay * 1000)
                {
                    switch (input)
                    {
                        case up:
                            if (Game.GameObjects[Position.X, Position.Y - 1] == null)
                            {
                                Position = new Point(Position.X, Position.Y - 1);
                                Stats.stats["Up"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X, Position.Y - 1] is Wall)
                            {
                                Stats.stats["Walls"].Add(1);
                            }
                            break;
                        case down:
                            if (Game.GameObjects[Position.X, Position.Y + 1] == null)
                            {
                                Position = new Point(Position.X, Position.Y + 1);
                                Stats.stats["Down"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + 1] is Wall)
                            {
                                Stats.stats["Walls"].Add(1);
                            }
                            break;
                        case left:
                            if (Game.GameObjects[Position.X - 1, Position.Y] == null)
                            {
                                Position = new Point(Position.X - 1, Position.Y);
                                Stats.stats["Left"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X - 1, Position.Y] is Wall)
                            {
                                Stats.stats["Walls"].Add(1);
                            }
                            break;
                        case right:
                            if (Game.GameObjects[Position.X + 1, Position.Y] == null)
                            {
                                Position = new Point(Position.X + 1, Position.Y);
                                Stats.stats["Right"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X + 1, Position.Y] is Wall)
                            {
                                Stats.stats["Walls"].Add(1);
                            }
                            break;
                        default:
                            break;
                    }

                    //SKATT PÅ BERÄKNADE POSITION SHIT ETC.

                    stopwatch.Reset();
                }
            }
        }

        //void PickUp(Trophy trophy)
        //{
        //    Game.GameObjects[trophy.Position.X, trophy.Position.Y] = null;
        //    trophyScore += trophy.Value;
        //    ++Stats.totalTrophies;
        //}
    }
}
