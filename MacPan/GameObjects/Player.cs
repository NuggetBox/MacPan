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
        bool tryingToRespawn;

        Stopwatch moveTimer = new Stopwatch();

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

            if (tryingToRespawn)
                AttemptRespawn();

            if (!moveTimer.IsRunning)
            {
                moveTimer.Start();
            }

            // Detects input only if a key on the keyboard is pressed.
            // Makes it so that the program runs even if we are waiting for Console.ReadKey() input.
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case pause:
                        //pause
                        Statistics.SaveStats();
                        break;

                    // If the interact button (Enter) is pressed. 
                    // We look for any interactable objects around us and interacts with them.
                    case interact:
                        bool interacted = false;

                        for (int i = -1; i < 2; i += 2)
                        {
                            if (Game.GameObjects[Position.X + i, Position.Y] is Trophy)
                            {
                                (Game.GameObjects[Position.X + i, Position.Y] as Trophy).PickUp();
                                ++HeldTrophies;
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Trophy)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Trophy).PickUp();
                                ++HeldTrophies;
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X + i, Position.Y] is Goal)
                            {
                                (Game.GameObjects[Position.X + i, Position.Y] as Goal).SecureTrophy();
                                SecureTrophyProcess(ref interacted);
                                interacted = true;
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + i] is Goal)
                            {
                                (Game.GameObjects[Position.X, Position.Y + i] as Goal).SecureTrophy();
                                SecureTrophyProcess(ref interacted);
                                interacted = true;
                            }
                        }

                        if (!interacted)
                        {
                            Statistics.Stats["InteractNothing"].Add(1);
                        }
                        break;
                }

                // If our movement time has passed a certain move delay, the player is allowed to move.
                if (moveTimer.ElapsedMilliseconds >= MoveDelay)
                {
                    // Checks for collision and moves in the given direction if it is possible.
                    switch (input)
                    {
                        case up:
                            if (Game.GameObjects[Position.X, Position.Y - 1] == null)
                            {
                                Position = new Point(Position.X, Position.Y - 1);
                                Statistics.Stats["Up"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X, Position.Y - 1] is Wall)
                            {
                                Statistics.Stats["Walls"].Add(1);
                            }
                            break;
                        case down:
                            if (Game.GameObjects[Position.X, Position.Y + 1] == null)
                            {
                                Position = new Point(Position.X, Position.Y + 1);
                                Statistics.Stats["Down"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X, Position.Y + 1] is Wall)
                            {
                                Statistics.Stats["Walls"].Add(1);
                            }
                            break;
                        case left:
                            if (Game.GameObjects[Position.X - 1, Position.Y] == null)
                            {
                                Position = new Point(Position.X - 1, Position.Y);
                                Statistics.Stats["Left"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X - 1, Position.Y] is Wall)
                            {
                                Statistics.Stats["Walls"].Add(1);
                            }
                            break;
                        case right:
                            if (Game.GameObjects[Position.X + 1, Position.Y] == null)
                            {
                                Position = new Point(Position.X + 1, Position.Y);
                                Statistics.Stats["Right"].Add(1);
                            }
                            else if (Game.GameObjects[Position.X + 1, Position.Y] is Wall)
                            {
                                Statistics.Stats["Walls"].Add(1);
                            }
                            break;
                        default:
                            break;
                    }

                    // Resets the move timer so that we can move again once the delay has passed.
                    moveTimer.Reset();
                }
            }
        }

        // Secures all carried trophies and wins the game if all trophies were collected.
        public void SecureTrophyProcess(ref bool interacted)
        {
            CollectedTrophies += HeldTrophies;
            HeldTrophies = 0;
            interacted = true;
            Statistics.Stats["Secured"].Add(1);

            if (CollectedTrophies == ReadMap.NumOfTrophies)
            {
                Statistics.Stats["Won"].Add(1);
                Menu.GameRunning = false;
            }
        }

        // Returns all carried trophies from your trophy bar to their original spots.
        public void ReturnTrophies()
        {
            for (int i = ReadMap.TrophyBarOffset + CollectedTrophies; i < ReadMap.TrophyBarOffset + HeldTrophies + CollectedTrophies; ++i)
            {
                if (Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset] != null)
                {
                    (Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset] as Trophy).AttemptGoBack();
                }
            }
            HeldTrophies = 0;
        }

        // Respawns the player as long as nothing is stopping it from doing so.
        public void AttemptRespawn()
        {
            if (Game.GameObjects[startPos.X, startPos.Y] == null)
            {
                Position = startPos;
                base.Draw();
                tryingToRespawn = false;
            }
            else
            {
                tryingToRespawn = true;
            }
        }
    }
}
