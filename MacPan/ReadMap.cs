using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    public static class ReadMap
    {
        public static int NumOfTrophies { get; set; }
        public static int TrophyBarOffset { get; set; } = 2;
        public static int HealthBarOffset { get; set; } = 5;
        public static int MapHeight { get; set; }
        
        static List<Point> enemies;
        static List<PatrolPoint> patrolPoints;

        public static void InitializeMap()
        {
            enemies = new List<Point>();
            patrolPoints = new List<PatrolPoint>();

            NumOfTrophies = 0;

            string[] lineText;

            lineText = System.IO.File.ReadAllLines("Map1.txt");
            MapHeight = lineText.Length;

            char[,] Characters = new char[Game.GridSize.X, lineText.Length];

            for (int i = 0; i < lineText.Length; i++)
            {
                for (int j = 0; j < lineText[i].Length; j++)
                {
                    Characters[j, i] = (lineText[i].ToCharArray())[j];
                }
            }

            char thisChar;
            for (int i = 0; i < lineText.Length; ++i)
            {
                for (int j = 0; j < lineText[i].Length; ++j)
                {
                    thisChar = Characters[j, i];
                    bool isItANumber = int.TryParse(thisChar.ToString(), out int theOut);

                    if (thisChar != default(char) && thisChar != '-' && thisChar != 'E' && !isItANumber)
                    {
                        Game.GameObjects[j, i] = FindObjectType(thisChar);
                        Game.GameObjects[j, i].Position = new Point(j, i);

                        if (thisChar == 'O')
                        {
                            ++NumOfTrophies;
                        }
                    }
                    if (thisChar == 'E')
                    {
                        enemies.Add(new Point(j, i));
                    }
                    if (isItANumber)
                    {
                        patrolPoints.Add(new PatrolPoint(new Point(j, i), thisChar));
                    }
                }
            }

            CreateEnemies();
            CreateTrophyBar();
            CreateHealthBar();
        }

        static GameObject FindObjectType(Char thisChar)
        {
            if (thisChar == '█')
            {
                return new Wall();
            }
            if (thisChar == 'O')
            {
                return new Trophy();
            }
            if (thisChar == 'G')
            {
                return new Goal();
            }
            if (thisChar == 'P')
            {
                return new Player();
            }
            return null;
        }

        static void CreateEnemies()
        {
            for (int i = 0; i < patrolPoints.Count; ++i)
            {
                for (int j = i; j < patrolPoints.Count; ++j)
                {
                    if (int.Parse((patrolPoints[j].PatrolPointIndex).ToString()) == i)
                    {
                        PatrolPoint tempPatrolPointStorage = patrolPoints[j];
                        patrolPoints.RemoveAt(j);
                        patrolPoints.Insert(i, tempPatrolPointStorage);
                    }
                }
            }

            for (int i = 0; i < enemies.Count; ++i)
            {
                Enemy newEnemy = new Enemy(enemies[i], patrolPoints[i].Position);
                Game.GameObjects[enemies[i].X, enemies[i].Y] = newEnemy;
                Game.GameObjects[enemies[i].X, enemies[i].Y].Position = new Point(enemies[i].X, enemies[i].Y);
            }
        }

        static void CreateTrophyBar()
        {
            int yellowX;
            int yellowY = MapHeight + TrophyBarOffset + 1;
            for (int i = 0; i < NumOfTrophies; ++i)
            {
                yellowX = TrophyBarOffset + i;
                Game.GameObjects[yellowX, yellowY] = new Wall
                {
                    Position = new Point(yellowX, yellowY),
                    Color = ConsoleColor.DarkYellow
                };
            }
        }

        static void CreateHealthBar()
        {
            int Y = MapHeight + HealthBarOffset;
            for (int i = 0; i < Player.MaxHealth; ++i)
            {
                Game.GameObjects[i + TrophyBarOffset, Y] = new Wall
                {
                    Position = new Point(i + TrophyBarOffset, Y),
                    Color = ConsoleColor.Red
                };
            }
        }

        public static void UpdateHealthBar()
        {
            Game.GameObjects[Player.HealthPoints + TrophyBarOffset, MapHeight + HealthBarOffset].Color = ConsoleColor.DarkRed;
            Game.GameObjects[Player.HealthPoints + TrophyBarOffset, MapHeight + HealthBarOffset].InitialDraw();
        }
    }
}
