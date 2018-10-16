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
        Thread playerThread;

        public static GameObject[,] GameObjects { get; set; }
        public static Point GridSize { get; set; }
        public static Point BoxSize { get; set; }

        public Game()
        {
            GridSize = new Point(118, 31);
            BoxSize = new Point(2, 2);
            GameObjects = new GameObject[GridSize.X, GridSize.Y];

            Console.WindowWidth = 2 * GridSize.X;
            Console.WindowHeight = 2 * GridSize.Y;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            Player player = new Player();
            playerThread = new Thread(new ThreadStart(player.UpdateDraw));
            playerThread.Start();

            //Enemy enemy = new Enemy();
            //GameObjects[80, 17] = enemy;

            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject == null)
                    continue;
                gameObject.InitialDraw();
            }
            Console.SetCursorPosition(0, 0);
        }

        public void UpdateBoard()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject == null)
                    continue;
                gameObject.Update();
            }
        }

        public void DrawBoard()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject == null)
                    continue;
                gameObject.Draw();
            }
        }
    }
}
