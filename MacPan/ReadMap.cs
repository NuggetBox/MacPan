using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    // Måns
    // This class can read a txt file that has been purposfully made for the game and then translate it into a map.
    public static class ReadMap
    {
        // These values are either determined by the map or are important for how the map is drawn and may also be used in other places in the program.
        public static int NumOfTrophies { get; set; }
        public static int TrophyBarOffset { get; set; } = 2;
        public static int HealthBarOffset { get; set; } = 5;
        public static int MapHeight { get; set; }
        
        // To make the patrol routes work we had to first collect every enemy and patrolpoint in lists.
        static List<Point> enemies;
        static List<PatrolPoint> patrolPoints;

        // This code is called upon to create mthe map.
        public static void InitializeMap()
        {
            enemies = new List<Point>();
            patrolPoints = new List<PatrolPoint>();

            NumOfTrophies = 0;

            string[] lineText;

            // The map is recievd as and array of strings, every string represents a row.
            lineText = System.IO.File.ReadAllLines("Map1.txt");
            // The maps height is the amount of rows.
            MapHeight = lineText.Length;

            // A 2-dimesionall character array that will contain every charcter in the map file.
            char[,] Characters = new char[Game.GridSize.X, lineText.Length];

            // The loop goes trough every character in every string in the text and gives it to the character array.
            for (int i = 0; i < lineText.Length; i++)
            {
                for (int j = 0; j < lineText[i].Length; j++)
                {
                    Characters[j, i] = (lineText[i].ToCharArray())[j];
                }
            }

            // The thisChar varible is the character in the character array which is currently being examined.
            char thisChar;
            // The double for loop goes trough every character.
            for (int i = 0; i < lineText.Length; ++i)
            {
                for (int j = 0; j < lineText[i].Length; ++j)
                {
                    thisChar = Characters[j, i];
                    bool isItANumber = int.TryParse(thisChar.ToString(), out int theOut);

                    // If the character isn't the character equivalent of null, or "-", or "E", or number we vill create it and give it a position.
                    if (thisChar != default(char) && thisChar != '-' && thisChar != 'E' && !isItANumber)
                    {
                        Game.GameObjects[j, i] = FindObjectType(thisChar);
                        Game.GameObjects[j, i].Position = new Point(j, i);

                        // If it's a trophy we vill increase the number of trophies on the map by one.
                        if (thisChar == 'O')
                        {
                            ++NumOfTrophies;
                        }
                    }
                    // If it's and enemy it will be added to the enemy list.
                    if (thisChar == 'E')
                    {
                        enemies.Add(new Point(j, i));
                    }
                    // If it's a number (an enemy's patrolpoint) it will be added to the patrolpoint list.
                    if (isItANumber)
                    {
                        patrolPoints.Add(new PatrolPoint(new Point(j, i), thisChar));
                    }
                }
            }

            // Now we call upon the methods to create the enemies and the GUI elements.
            CreateEnemies();
            CreateTrophyBar();
            CreateHealthBar();
        }

        // This method determines what each character represents.
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

        // The enemies are created here.
        static void CreateEnemies()
        {
            // These loops go trough the patrolpoints and sorts them by the character that represents them in order from 0 and up.
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

            // The enemies are paired up with the patrolpoints by their positions in their lists.
            for (int i = 0; i < enemies.Count; ++i)
            {
                Enemy newEnemy = new Enemy(enemies[i], patrolPoints[i].Position);
                Game.GameObjects[enemies[i].X, enemies[i].Y] = newEnemy;
                Game.GameObjects[enemies[i].X, enemies[i].Y].Position = new Point(enemies[i].X, enemies[i].Y);
            }

            // This system makes it so that a map creator may assign each enemy a unique patrolpoint.
        }

        // A row of dark yellow walls are created. These represents an unfilled trophy bar.
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

        // An health bar is created. 
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

        // When the health decreases this method can be called upon the change the color of the most recently lost health point to dark red. It now represents a lost life.
        public static void UpdateHealthBar()
        {
            Game.GameObjects[Player.HealthPoints + TrophyBarOffset, MapHeight + HealthBarOffset].Color = ConsoleColor.DarkRed;
            Game.GameObjects[Player.HealthPoints + TrophyBarOffset, MapHeight + HealthBarOffset].InitialDraw();
        }
    }
}
