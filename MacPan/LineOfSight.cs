using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class LineOfSight
    {
        delegate bool CheckILoop(int firstVar, int secondVar);
        delegate bool CheckJLoop(int firstVar, int secondVar);
        CheckILoop checkI = CheckIfSmaller;
        CheckJLoop checkJ = CheckIfSmaller;

        public List<Point> LOS(GameObject sender, GameObject target)
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
                if (-1 < k && k < 1)
                    horizontal = false;
                if (target.Position.X < sender.Position.X)
                    goingRight = false;
                #endregion

                #region //Former Solution, full length
                /*if (goingRight)
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            rowsFirstPos = sender.Position.X;
                            for (int i = sender.Position.Y; i <= target.Position.Y; ++i)
                            {
                                lineCrossing = (int)((i + 0.5 - m) / k);

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
                                lineCrossing = (int)((i + 0.5 - m) / k);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(j, i));

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
                                lineCrossing = (int)((i - 0.5 - m) / k);

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
                                lineCrossing = (int)((i + 0.5 - m) / k);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(j, i));

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
                            for (int i = sender.Position.Y; i <= target.Position.Y; ++i)
                            {
                                lineCrossing = (int)((i + 0.5 - m) / k);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i <= target.Position.X; ++i)
                            {
                                lineCrossing = (int)((i + 0.5 - m) / k);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(j, i));

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
                                lineCrossing = (int)((i - 0.5 - m) / k);

                                for (int j = rowsFirstPos; j <= lineCrossing; ++j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                        else
                        {
                            rowsFirstPos = sender.Position.Y;
                            for (int i = sender.Position.X; i >= target.Position.X; --i)
                            {
                                lineCrossing = (int)((i + 0.5 - m) / k);

                                for (int j = rowsFirstPos; j >= lineCrossing; --j)
                                    path.Add(new Point(j, i));

                                rowsFirstPos = lineCrossing;
                            }
                        }
                    }
                }
                path.Remove(sender.Position);
                return path;*/
                #endregion

                #region //Establish which math to use
                if (goingRight)
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            checkI = CheckIfSmaller;
                            checkJ = CheckIfSmaller;
                            CalcPath(sender.Position.X, sender.Position.Y, 1, 1);
                        }
                        else
                        {
                            checkI = CheckIfSmaller;
                            checkJ = CheckIfSmaller;
                            CalcPath(sender.Position.Y, sender.Position.X, 1, 1);
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            checkI = CheckIfBigger;
                            checkJ = CheckIfSmaller;
                            CalcPath(sender.Position.X, sender.Position.Y, -1, 1);
                        }
                        else
                        {
                            checkI = CheckIfSmaller;
                            checkJ = CheckIfBigger;
                            CalcPath(sender.Position.Y, sender.Position.X, 1, -1);
                        }
                    }
                }
                else
                {
                    if (lineRising)
                    {
                        if (horizontal)
                        {
                            checkI = CheckIfSmaller;
                            checkJ = CheckIfBigger;
                            CalcPath(sender.Position.X, sender.Position.Y, 1, -1);
                        }
                        else
                        {
                            checkI = CheckIfSmaller;
                            checkJ = CheckIfSmaller;
                            CalcPath(sender.Position.Y, sender.Position.X, 1, 1);
                        }
                    }
                    else
                    {
                        if (horizontal)
                        {
                            checkI = CheckIfBigger;
                            checkJ = CheckIfSmaller;
                            CalcPath(sender.Position.X, sender.Position.Y, -1, 1);
                        }
                        else
                        {
                            checkI = CheckIfBigger;
                            checkJ = CheckIfBigger;
                            CalcPath(sender.Position.Y, sender.Position.X, -1, -1);
                        }
                    }
                }
                #endregion

                List<Point> CalcPath(int shortSide, int longSide, int iMod, int jMod)
                {
                    rowsFirstPos = shortSide;
                    for (int i = longSide; checkI(i, longSide); i += iMod)
                    {
                        lineCrossing = (int)((i + 0.5 - m) / k);

                        for (int j = rowsFirstPos; checkJ(j, lineCrossing); j += jMod)
                            path.Add(new Point(j, i));

                        rowsFirstPos = lineCrossing;
                    }
                    path.Remove(sender.Position);
                    return path;
                }
            }
            return null;
        }

        static bool CheckIfSmaller(int firstVar, int secondVar)
        {
            return firstVar <= secondVar;
        }

        static bool CheckIfBigger(int firstVar, int secondVar)
        {
            return firstVar >= secondVar;
        }
    }
}
