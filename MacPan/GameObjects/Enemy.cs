﻿using System;
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

        Pathfinding pathfinding = new Pathfinding();

        public override void Update()
        {
            pathfinding.AStar();
            int moveDir = 0;
            moveDir = Program.rng.Next(1, 5);

            Console.ReadKey(true);
            OldPosition = Position;


            //switch (moveDir)
            //{
            //    case 1:
            //        if (Game.GameObjects[Position.X, Position.Y + 1] == null)
            //        {
            //            Position = new Point(Position.X, Position.Y + 1);
            //        }
            //        break;
            //    case 2:
            //        if (Game.GameObjects[Position.X, Position.Y - 1] == null)
            //        {
            //            Position = new Point(Position.X, Position.Y - 1);
            //        }
            //        break;
            //    case 3:
            //        if (Game.GameObjects[Position.X + 1, Position.Y] == null)
            //        {
            //            Position = new Point(Position.X + 1, Position.Y);
            //        }
            //        break;
            //    case 4:
            //        if (Game.GameObjects[Position.X - 1, Position.Y] == null)
            //        {
            //            Position = new Point(Position.X - 1, Position.Y);
            //        }
            //        break;
            //}

            // Om det är en player på vår utberäknade position så ska vi inte flytta dit utan busta player, och stanna kvar på OldPosition
        }
        class Pathfinding
        {
            public void AStar()
            {
                Console.Title = "A* Pathfinding";

                // algorithm

                Location current = null;
                var start = new Location { X = 1, Y = 2 };
                var target = new Location { X = 2, Y = 5 };
                var openList = new List<Location>();
                var closedList = new List<Location>();
                int g = 0;
                string[] map = new string[10];

                // start by adding the original position to the open list
                openList.Add(start);

                while (openList.Count > 0)
                {
                    // get the square with the lowest F score
                    var lowest = openList.Min(l => l.F);
                    current = openList.First(l => l.F == lowest);

                    // add the current square to the closed list
                    closedList.Add(current);

                    // show current square on the map
                    Console.SetCursorPosition(current.X, current.Y);
                    Console.Write('.');
                    Console.SetCursorPosition(current.X, current.Y);
                    System.Threading.Thread.Sleep(1000);

                    // remove it from the open list
                    openList.Remove(current);

                    // if we added the destination to the closed list, we've found a path
                    if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                        break;

                    var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
                    g++;

                    foreach (var adjacentSquare in adjacentSquares)
                    {
                        // if this adjacent square is already in the closed list, ignore it
                        if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                                && l.Y == adjacentSquare.Y) != null)
                            continue;

                        // if it's not in the open list...
                        if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                                && l.Y == adjacentSquare.Y) == null)
                        {
                            // compute its score, set the parent
                            adjacentSquare.G = g;
                            adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;

                            // and add it to the open list
                            openList.Insert(0, adjacentSquare);
                        }
                        else
                        {
                            // test if using the current G score makes the adjacent square's F score
                            // lower, if yes update the parent because it means it's a better path
                            if (g + adjacentSquare.H < adjacentSquare.F)
                            {
                                adjacentSquare.G = g;
                                adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                                adjacentSquare.Parent = current;
                            }
                        }
                    }
                }

                // assume path was found; let's show it
                while (current != null)
                {
                    Console.SetCursorPosition(current.X, current.Y);
                    Console.Write('_');
                    Console.SetCursorPosition(current.X, current.Y);
                    current = current.Parent;
                    System.Threading.Thread.Sleep(1000);
                }

                // end

                Console.ReadLine();
            }

            static List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map)
            {
                var proposedLocations = new List<Location>()
            {
                new Location { X = x, Y = y - 1 },
                new Location { X = x, Y = y + 1 },
                new Location { X = x - 1, Y = y },
                new Location { X = x + 1, Y = y },
            };

                return proposedLocations.Where(l => map[l.Y][l.X] == ' ' || map[l.Y][l.X] == 'B').ToList();
            }

            static int ComputeHScore(int x, int y, int targetX, int targetY)
            {
                return Math.Abs(targetX - x) + Math.Abs(targetY - y);
            }
        }
    }
    class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }
}
