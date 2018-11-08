﻿using System;
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

        int heldTrophyScore;
        int collectedTrophyScore;
        bool hasRunIncreaseTrophies = false;

        public Player()
        {
            Color = ConsoleColor.Cyan;
            MoveDelay = 0.1f;
            Game.GameObjects[Position.X, Position.Y] = this;
            Draw();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < ReadMap.NumOfTrophies; i++)
            {
                Console.SetCursorPosition(8 + i * 4, 38);
                Console.Write("████");
                Console.SetCursorPosition(8 + i * 4, 39);
                Console.Write("████");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public void UpdateHeldBar()
        {
            for (int i = 0; i < ReadMap.NumOfTrophies; ++i)
            {
                if (collectedTrophyScore < i && i <= collectedTrophyScore + heldTrophyScore)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(8 + i * 4, 36);
                    Console.Write("████");
                    Console.SetCursorPosition(8 + i * 4, 37);
                    Console.Write("████");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }

        public void SecureTrophies()
        {
            collectedTrophyScore += heldTrophyScore;
            heldTrophyScore = 0;
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
                                FoundTrophy();
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Trophy)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Trophy).PickUp();
                                FoundTrophy();
                            }

                            void FoundTrophy()
                            {
                                ++heldTrophyScore;
                                UpdateHeldBar();
                                interacted = true;
                            }

                            if (Game.GameObjects[Position.X + i, Position.Y] is Goal || Game.GameObjects[Position.X, Position.Y + i] is Goal)
                            {
                                SecureTrophies();
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
    }
}
