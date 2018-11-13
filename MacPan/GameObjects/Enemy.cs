using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MacPan
{
    class Enemy : GameObject
    {
        Stopwatch stopwatch = new Stopwatch();

        List<Point> patrolPoints;
        List<Point> steps;

        public Enemy()
        {
            Color = ConsoleColor.DarkRed;
            MoveDelay = 100;
            Position = new Point(15, 10);
            Game.GameObjects[Position.X, Position.Y] = this;

            Draw();
        }
        public override void Update()
        {
            //Console.ReadKey(true);
            OldPosition = Position;
            //steps = PathFinding(new Point(14,11));
            steps = PathFinding(new Point(51, 11));
            for (int i = 0; i < steps.Count; i++)
            {
                stopwatch.Start();
                if (stopwatch.ElapsedMilliseconds >= MoveDelay)
                {
                    Position = steps[i];
                    stopwatch.Restart();
                }
            }
        }

        #region Pathfinding

        public List<Point> PathFinding(Point target1)
        {
            List<Point> path = new List<Point>();

            // algorithm
            Location current = null;
            var start = new Location { X = Position.X, Y = Position.Y };
            Location target = new Location {X = target1.X, Y = target1.Y };
            
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;

            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list
                closedList.Add(current);

                //show current square on the map
                //Console.SetCursorPosition(current.X * Game.BoxSize.X, current.Y * Game.BoxSize.Y);
                //Console.Write('.');
                //Console.SetCursorPosition(current.X * Game.BoxSize.X, current.Y * Game.BoxSize.Y);
                //Position = new Point(current.X, current.Y);

                // remove it from the open list
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y);
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
                path.Add(new Point(current.X, current.Y));
                Debug.Write(path);
                //Console.SetCursorPosition(current.X * Game.BoxSize.X, current.Y * Game.BoxSize.Y);
                //Console.Write("_");
                //Console.SetCursorPosition(current.X * Game.BoxSize.X, current.Y * Game.BoxSize.Y);
                current = current.Parent;
                //System.Threading.Thread.Sleep(1000);
            }
            // end
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }

        static List<Location> GetWalkableAdjacentSquares(int x, int y)
        {
            var proposedLocations = new List<Location>()
            {

                new Location { X = x, Y = y - 1 },
                new Location { X = x, Y = y + 1 },
                new Location { X = x - 1, Y = y },
                new Location { X = x + 1, Y = y },
            };
            
            return proposedLocations.Where(l => Game.GameObjects[l.X,l.Y] == null).ToList();
        }

        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }

    public class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }
    #endregion

}
