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
            GridSize = new Point(118, 31);
            BoxSize = new Point(2, 2);
            GameObjects = new GameObject[GridSize.X + 1, GridSize.Y + 1];

            Player player = new Player();
            Thread playerThread = new Thread(new ThreadStart(player.UpdateDraw));
            Console.WindowWidth = 2 * GridSize.X;
            Console.WindowHeight = 2 * GridSize.Y;

            for (int x = 0; x < GridSize.X; ++x)
            {
                for (int y = 0; y < GridSize.Y; ++y)
                {
                    if (x == 0 || y == 0 || x == GridSize.X - 1 || y == GridSize.Y - 1)
                    {
                        GameObjects[x, y] = new Wall();
                    }
                }
            }

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
