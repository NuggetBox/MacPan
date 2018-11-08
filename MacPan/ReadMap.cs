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

        public static void InitializeMap()
        {
            string[] lineText = new string[Game.GridSize.Y];

            lineText = System.IO.File.ReadAllLines(Program.Path + "/Maps/Map1.txt");

            char[,] Characters = new char[Game.GridSize.X, Game.GridSize.Y];

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

                    if (thisChar != default(char) && thisChar != '-' && thisChar != 'G' && thisChar != 'V' && thisChar != 'E')
                    {
                        Game.GameObjects[j, i] = FindObjectType(thisChar);
                        Game.GameObjects[j, i].Position = new Point(j, i);
                        
                        if (thisChar == 'O')
                        {
                            ++NumOfTrophies;
                        }
                    }
                }
            }
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
            if (thisChar == 'E')
            {
                return new Enemy();
            }
            if (thisChar == 'G')
            {
                //return new Goal();
            }
            if (thisChar == 'V')
            {
                //return new Vent();
            }
            if (thisChar == 'P')
            {
                return new Player();
            }
            return null;
        }
    }
}
