using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    static class LineOfSight
    {
        #region // Old delegates declaration
        delegate bool CheckILoop(int firstVar, int secondVar);
        delegate bool CheckJLoop(int firstVar, int secondVar);
        static CheckILoop CheckI { get; set; } = CheckIfSmaller;
        static CheckJLoop CheckJ { get; set; } = CheckIfSmaller;
        #endregion

        static public List<Point> LOS(GameObject sender, GameObject target)
        {
            List<Point> path = new List<Point>();
            path = StraightPath(sender, target);

            if (path == null)
                return null;

            // -1 problem kan gå in i varandra
            for (int i = 0; i < path.Count; ++i)
            {
                if (Game.GameObjects[path[i].X, path[i].Y] != null && !(Game.GameObjects[path[i].X, path[i].Y] is Player))
                {
                    return null;
                }
            }
            return path;
        }

        static public List<Point> StraightPath(GameObject sender, GameObject target)
        {
            int range = 20;
            List<Point> path = new List<Point>();
            float m;
            float k;
            bool lineRising = true;
            bool horizontal = true;
            bool goingRight = true;
            int lineCrossing;
            int rowsFirstPos;


            if (range >= Math.Sqrt(Math.Pow(sender.Position.X - target.Position.X, 2) + Math.Pow(sender.Position.Y - target.Position.Y, 2)))
            {
                if (sender.Position.X == target.Position.X)
                {
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

                m = (target.Position.Y * sender.Position.X - target.Position.X * sender.Position.Y) / (sender.Position.X - target.Position.X);
                k = (sender.Position.Y - m) / sender.Position.X;

                #region //Check direction of vector
                if (k < 0)
                    lineRising = false;
                if (!(-1 < k && k < 1))
                    horizontal = false;
                if (target.Position.X < sender.Position.X)
                    goingRight = false;
                #endregion

                #region //Full length solution
                if (goingRight)
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            rowsFirstPos = sender.Position.X;
                            for (int i = sender.Position.Y; i <= target.Position.Y; ++i)
                            {
                                lineCrossing = (int)(((i + 0.5 - m) / k) + 0.5);

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
                path.Remove(sender.Position);
                return path;
                #endregion

                #region //Old small solution
                /*
                if (goingRight)
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            CheckI = CheckIfSmaller;
                            CheckJ = CheckIfSmaller;
                            return CalcPath(sender.Position.X, sender.Position.Y, 1, 1);
                        }
                        else
                        {
                            CheckI = CheckIfSmaller;
                            CheckJ = CheckIfSmaller;
                            return CalcPath(sender.Position.Y, sender.Position.X, 1, 1);
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            CheckI = CheckIfBigger;
                            CheckJ = CheckIfSmaller;
                            return CalcPath(sender.Position.X, sender.Position.Y, -1, 1);
                        }
                        else
                        {
                            CheckI = CheckIfSmaller;
                            CheckJ = CheckIfBigger;
                            return CalcPath(sender.Position.Y, sender.Position.X, 1, -1);
                        }
                    }
                }
                else
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            CheckI = CheckIfSmaller;
                            CheckJ = CheckIfBigger;
                            return CalcPath(sender.Position.X, sender.Position.Y, 1, -1);
                        }
                        else
                        {
                            CheckI = CheckIfSmaller;
                            CheckJ = CheckIfSmaller;
                            return CalcPath(sender.Position.Y, sender.Position.X, 1, 1);
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            CheckI = CheckIfBigger;
                            CheckJ = CheckIfSmaller;
                            return CalcPath(sender.Position.X, sender.Position.Y, -1, 1);
                        }
                        else
                        {
                            CheckI = CheckIfBigger;
                            CheckJ = CheckIfBigger;
                            return CalcPath(sender.Position.Y, sender.Position.X, -1, -1);
                        }
                    }
                }

                List<Point> CalcPath(int longSide, int shortSide, int iMod, int jMod)
                {
                    rowsFirstPos = longSide;
                    for (int i = shortSide; CheckI(i, shortSide); i += iMod)
                    {
                        lineCrossing = (int)((i + 0.5 - m) / k);

                        for (int j = rowsFirstPos; CheckJ(j, lineCrossing); j += jMod)
                            path.Add(new Point(j, i));

                        rowsFirstPos = lineCrossing;
                    }
                    path.Remove(sender.Position);

                    for (int i = 0; i < path.Count - 1; ++i)
                    {
                        if (Game.GameObjects[path[i].X, path[i].Y] != null)
                        {
                            return null;
                        }
                    }

                    return path;
                }
                */
                #endregion
            }
            return null;
        }
        #region // Old delegate changer methods
        static bool CheckIfSmaller(int firstVar, int secondVar)
        {
            return firstVar <= secondVar;
        }

        static bool CheckIfBigger(int firstVar, int secondVar)
        {
            return firstVar >= secondVar;
        }
        #endregion
    }
}
