﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MacPan
{
    class Enemy : GameObject
    {
        Stopwatch moveTimer = new Stopwatch();

        List<Point> patrolPoints = new List<Point>();
        List<Point> path = new List<Point>();

        Point step;

        int patrolIndex = 0;

        public Enemy(Point position, Point patrolPoint)
        {
            Color = ConsoleColor.DarkRed;
            Position = position;
            Draw();

            // It's own position as well as the patrolpoint asigned to it by the readmap class are set as patrolpoints.
            patrolPoints.Add(Position);
            patrolPoints.Add(patrolPoint);
        }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public override void Update()
        {
            OldPosition = Position;
            if (!moveTimer.IsRunning)
            {
                moveTimer.Start();
            }

            // If the move timer has passed a certain move delay the enemy is allowed to move.
            if (moveTimer.ElapsedMilliseconds >= MoveDelay)
            {
                // Calls the Line Of Sight method and decides if it can see the player or not.
                List<Point> prePath = LineOfSight.LOS(this, Player.Singleton);
                MoveDelay = 200;

                // If the enemy sees the player, the enemy follows the calculated path towards him.
                if (prePath != null)
                {
                    MoveDelay = 100;
                    path = prePath;
                    Walk();
                }
                // If we do not see the player,
                else
                {
                    // we keep following our latest path towards the player.
                    if (path.Count > 0)
                    {
                        Walk();
                    }
                    // if we have arrived at the players latest known location, or if we simply don't see him, we continue patrolling between our patrol points.
                    else
                    {
                        Patrol();
                    }
                }

                moveTimer.Reset();
            }
        }

        // Moves the enemy to the next step in its given path.
        void Walk()
        {
            if (path.Count != 0)
            {
                step = path[0];
                path.RemoveAt(0);
            }

            // If the enemy attempts to walk into the player, the player has been busted and has to respawn.
            if (step.Equals(Player.Singleton.Position))
            {
                // PLAYER BUSTED.
                Player.Singleton.ReturnTrophies();
                Player.HealthPoints--;
                ReadMap.UpdateHealthBar();
                Statistics.Stats["Busted"].Add(1);

                if (Player.HealthPoints == 0)
                {
                    Player.CollectedTrophies = 0;
                    Menu.GameRunning = false;
                }

                Player.Singleton.AttemptRespawn();
            }
            else
            {
                if (Game.GameObjects[step.X, step.Y] == null)
                    Position = step;
            }
        }

        // Patrols between two patrolpoints, following the A* algorithm.
        void Patrol()
        {
            if (path.Count == 0)
            {
                if (patrolIndex == 0)
                {
                    path = AStarPathFinding(patrolPoints[1]);
                }
                
                if (patrolIndex == 1)
                {
                    path = AStarPathFinding(patrolPoints[0]);
                }

                patrolIndex = (patrolIndex + 1) % 2;
            }
            else
            {
                path = AStarPathFinding(patrolPoints[patrolIndex]);
            }

            Walk();
        }

        #region A* Pathfinding

        // The method which controlls the A* part of the pathfinding algorithms.
        public List<Point> AStarPathFinding(Point target1)
        {
            List<Point> path = new List<Point>();
            Location current = null;
            var start = new Location { X = Position.X, Y = Position.Y };
            Location target = new Location {X = target1.X, Y = target1.Y };
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                // Get the square with the lowest F score.
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // Add the current square to the closed list.
                closedList.Add(current);

                // Remove it from the open list.
                openList.Remove(current);

                // If we added the destination to the closed list, we've found a path.
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y);
                g++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // If this adjacent square is already in the closed list, ignore it.
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) != null)
                        continue;

                    // If it's not in the open list...
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) == null)
                    {

                        // Compute its score, set the parent.
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        // And add it to the open list.
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // Test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path.
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            // Returns the optimal path to target.
            while (current != null)
            {
                path.Add(new Point(current.X, current.Y));
                Debug.Write(path);
                current = current.Parent;
            }
            // End of the algorithm.
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }

        // Checks which squares are accesable based on the given criteria.
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

        // Computes the H score of a given position.
        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
    // Class made with the express purpose of storing the values relevant for a* positioning.
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
