using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    // Måns
    // The purpose of this class is to determine if enemies can see the player and then give the enemy a straight path to the player if such is the case.
    static class LineOfSight
    {
        // This method recieves a sender and a target och then returns the straightest and shortest path if nothing is in the way or null if something is in the way.
        static public List<Point> LOS(GameObject sender, GameObject target)
        {
            // The path is created.
            List<Point> path = new List<Point>();
            path = StraightPath(sender, target);

            // Path may already be null if the target was not within range.
            if (path == null)
                return null;
            
            // This loop goes trough every steep and checks for an obstacle. If one is found it is determined that the enemy cannot see the player and null is returned.
            for (int i = 0; i < path.Count; ++i)
            {
                if (Game.GameObjects[path[i].X, path[i].Y] != null && !(Game.GameObjects[path[i].X, path[i].Y] is Player))
                {
                    return null;
                }
            }
            // If the path got past the loop there must not be any obstacles in the way and the path can be returned to the enemy.
            return path;
        }

        // This method simply returns the shortest and most direct path to a specified target.
        static public List<Point> StraightPath(GameObject sender, GameObject target)
        {
            int range = 10;
            List<Point> path = new List<Point>();
            float m;
            float k;
            bool lineRising = true;
            bool horizontal = true;
            bool goingRight = true;
            int lineCrossing;
            int rowsFirstPos;

            // Pythagoras theorem is used to determine if the target is within range.
            if (range >= Math.Sqrt(Math.Pow(sender.Position.X - target.Position.X, 2) + Math.Pow(sender.Position.Y - target.Position.Y, 2)))
            {
                // If the target is on the same x or y koordinate the calculation is quite easy.
                if (sender.Position.X == target.Position.X)
                {
                    // It is determined wheter the target is above or below and then a loop adds every step to the path.
                    if (sender.Position.Y > target.Position.Y)
                    {
                        for (int i = sender.Position.Y - 1; i >= target.Position.Y; --i)
                        {
                            path.Add(new Point(sender.Position.X, i));
                        }
                    }
                    else
                    {
                        for (int i = sender.Position.Y + 1; i <= target.Position.Y; ++i)
                        {
                            path.Add(new Point(sender.Position.X, i));
                        }
                    }
                    return path;
                }

                if (sender.Position.Y == target.Position.Y)
                {
                    // It is determined wheter the target is to the right or the left and then a loop adds every step to the path.
                    if (sender.Position.X > target.Position.X)
                    {
                        for (int i = sender.Position.X - 1; i >= target.Position.X; --i)
                        {
                            path.Add(new Point(i, sender.Position.Y));
                        }
                    }
                    else
                    {
                        for (int i = sender.Position.X + 1; i <= target.Position.X; ++i)
                        {
                            path.Add(new Point(i, sender.Position.Y));
                        }
                    }
                    return path;
                }

                // If the target was not simply above, below, to the right, or to the left, then we will have to use linear functions to find the target. m and k of the line is determined trough an equation.
                m = (target.Position.Y * sender.Position.X - target.Position.X * sender.Position.Y) / (sender.Position.X - target.Position.X);
                k = (sender.Position.Y - m) / sender.Position.X;

                // This is done to determine in which way the vector is going so that we may use the most effiecent and appropriate code to create a path.
                #region //Check direction of vector
                if (k < 0)
                    lineRising = false;
                if (!(-1 < k && k < 1))
                    horizontal = false;
                if (target.Position.X < sender.Position.X)
                    goingRight = false;
                #endregion

                // All fo these ifs may look ugly but from what I can tell are completely in line with microsoft standard and were pretty much unavoidable.
                #region //Full length solution
                if (goingRight)
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            // We start by setting the senders position along the vectors longer axis as this rows first position.
                            rowsFirstPos = sender.Position.X;
                            // This loop goes trough every row in the vector. This is done along the shorter axis for maximum efficiency.
                            for (int i = sender.Position.Y; i <= target.Position.Y; ++i)
                            {
                                // The "linecrossing" is the last position in this row that the vector goes trough.
                                lineCrossing = (int)(((i + 0.5 - m) / k) + 0.5);

                                // Every position between the first position in the row and the linecrossing is added to the path.
                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(j, i));

                                // The linecrossing is the next rows first position.
                                rowsFirstPos = lineCrossing;

                                // The 7 other formulas follow the same principle but follow different axis and use slighly different formulas for higher efficiency.
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i <= target.Position.X; ++i)
                            {
                                lineCrossing = (int)(((i + 0.5) * k + m) + 0.5);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(i, j));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            rowsFirstPos = sender.Position.X;
                            for (int i = sender.Position.Y; i >= target.Position.Y; --i)
                            {
                                lineCrossing = (int)(((i - 0.5 - m) / k) + 0.5);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i <= target.Position.X; ++i)
                            {
                                lineCrossing = (int)(((i + 0.5) * k + m) + 0.5);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(i, j));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                    }
                }
                else
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            rowsFirstPos = sender.Position.X;
                            for (int i = sender.Position.Y; i >= target.Position.Y; --i)
                            {
                                lineCrossing = (int)(((i - 0.5 - m) / k) + 0.5);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i >= target.Position.X; --i)
                            {
                                lineCrossing = (int)(((i - 0.5) * k + m) + 0.5);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(i, j));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            rowsFirstPos = sender.Position.X;
                            for (int i = sender.Position.Y; i <= target.Position.Y; ++i)
                            {
                                lineCrossing = (int)(((i + 0.5 - m) / k) + 0.5);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i >= target.Position.X; --i)
                            {
                                lineCrossing = (int)(((i - 0.5) * k + m) + 0.5);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(i, j));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                    }
                }
                // The path should not contain the senders own position.
                path.Remove(sender.Position);
                return path;
                #endregion
            }
            return null;
        }
    }
}
