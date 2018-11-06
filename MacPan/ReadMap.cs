using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class ReadMap
    {
        public ReadMap()
        {
            string[] lineText = new string[31];
            lineText = System.IO.File.ReadAllLines(@"C:\Users\mans.berggren1\Source\Repos\MacPan\MacPan\bin\Debug\Maps\Map1.txt");

            char[][] lines = new char[Game.GridSize.Y][];

            for (int i = 0; i < lineText.Length; ++i)
            {
                lines[i] = lineText[i].ToCharArray();
            }

            Char thisChar;
            for (int i = 0; i < Game.GridSize.Y; ++i)
            {
                for (int j = 0; j < Game.GridSize.X; ++j)
                {
                    thisChar = lines[i][j];

                    if (thisChar != default(char) && thisChar != '-')
                    {
                        //Game.GameObjects[j, i] = FindObjectType(thisChar);
                    }
                }
            }
        }

        /*GameObject FindObjectType(Char thisChar)
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

            }
            if (thisChar == 'G')
            {

            }
            if (thisChar == 'V')
            {

            }
            if (thisChar == 'P')
            {

            }
        }*/
    }
}
