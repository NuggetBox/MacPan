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
        public static Player Singleton { get; set; }
        public static int HealthPoints { get; set; }
        public static int MaxHealth { get; private set; }
        public static int HeldTrophies { get; set; }
        public static int CollectedTrophies { get; set; }

        Point startPos;
        bool onVent;

        Stopwatch stopwatch = new Stopwatch();

        ConsoleKey input;

        const ConsoleKey
            up = ConsoleKey.UpArrow,
            down = ConsoleKey.DownArrow,
            left = ConsoleKey.LeftArrow,
            right = ConsoleKey.RightArrow,
            interact = ConsoleKey.Spacebar,
            pause = ConsoleKey.Escape;

        public Player()
        {
            HealthPoints = 3;
            MaxHealth = HealthPoints;
            Color = ConsoleColor.Cyan;
            MoveDelay = 100;
            Draw();

            if (Singleton != null)
            {
                throw new Exception("Player initialized multiple times");
            }

            Singleton = this;
        }

        public override void InitialDraw()
        {
            startPos = Position;
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
                                (Game.GameObjects[Position.X + i, Position.Y] as Trophy).PickUp(HeldTrophies, CollectedTrophies);
                                ++HeldTrophies;
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Trophy)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Trophy).PickUp(HeldTrophies, CollectedTrophies);
                                ++HeldTrophies;
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X + i, Position.Y] is Goal)
                            {
                                (Game.GameObjects[Position.X + i, Position.Y] as Goal).SecureTrophy(HeldTrophies, CollectedTrophies);
                                SecureTrophyProcess(ref interacted);
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Goal)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Goal).SecureTrophy(HeldTrophies, CollectedTrophies);
                                SecureTrophyProcess(ref interacted);
                            }
                        }

                        if (!interacted)
                        {
                            Stats.stats["InteractNothing"].Add(1);
                        }
                        break;
                }

                if (stopwatch.ElapsedMilliseconds >= MoveDelay)
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

                    stopwatch.Reset();
                }
            }
        }

        public void SecureTrophyProcess(ref bool interacted)
        {
            CollectedTrophies += HeldTrophies;
            HeldTrophies = 0;
            interacted = true;
            Stats.stats["Secured"].Add(1);

            if (CollectedTrophies == /*ReadMap.NumOfTrophies*/0)
            {
                Stats.stats["Won"].Add(1);
                Menu.GameRunning = false;
            }
        }

        public void ReturnTrophies()
        {
            for (int i = ReadMap.TrophyBarOffset + CollectedTrophies; i < ReadMap.TrophyBarOffset + HeldTrophies + CollectedTrophies; ++i)
            {
                if (Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset] != null)
                {
                    (Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset] as Trophy).GoBack();
                }
            }
            HeldTrophies = 0;
        }

        public void Respawn()
        {
            Position = startPos;
            base.Draw();
        }
    }
}
