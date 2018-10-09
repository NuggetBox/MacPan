using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Game
    {
        GameObject[,] gameObjects;
        public int xGridSize = 150, yGridSize = 50;
        public int xBoxSize = 1, yBoxSize = 1;

        public Game()
        {

        }

        public void DrawBoard()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(xBoxSize, yBoxSize);
            }
        }

        public void UpdateBoard()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameObjects);
            }
        }


    }
}
