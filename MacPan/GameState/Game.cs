using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacPan
{
    class Game
    {
        public static GameObject[,] GameObjects { get; set; }
        public static Point GridSize { get; set; }
        public static Point BoxSize { get; set; }

        public Game()
        {
            GridSize = new Point(120, 31);
            BoxSize = new Point(2, 2);
            GameObjects = new GameObject[GridSize.X, GridSize.Y];

            Player player = new Player();
            Thread playerThread = new Thread(new ThreadStart(player.UpdateDraw));

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.InitialDraw();
            }
        }

        public void UpdateBoard()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update();
            }
        }

        public void DrawBoard()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw();
            }
        }
    }
}
