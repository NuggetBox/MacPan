using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    static class LineOfSight
    {
        // Denna metod tar emot en sändare och ett mål och returnerar då antingen fågelvägen om inget är ivägen eller null om något är ivägen.
        static public List<Point> LOS(GameObject sender, GameObject target)
        {
            List<Point> path = new List<Point>();
            path = StraightPath(sender, target);

            if (path == null)
                return null;
            
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
            int range = 10;
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
            }
            return null;
        }
    }
}
