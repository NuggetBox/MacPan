using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MacPan
{
    class Game
    {
        Player player;
        Enemy enemy;
        //Thread playerThread;
        //Thread inputThread;

        public static GameObject[,] GameObjects { get; set; }
        public static Point GridSize { get; set; }
        public static Point BoxSize { get; set; }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        public Game()
        {
            BoxSize = new Point(2, 2);
            GridSize = new Point(120, 31);
            GameObjects = new GameObject[GridSize.X, GridSize.Y];
      
            Console.SetWindowSize(2 * 120, 2 * 31 + 1);
            Console.SetBufferSize(2 * 120 + 2, 2 * 31 + 1);
            Maximize();
            Console.CursorVisible = false;

            //Player player = new Player();
            //playerThread = new Thread(new ThreadStart(player.UpdateDraw));
            //playerThread.Start();

            //inputThread = new Thread(new ThreadStart(InputManager.CheckInput));
            //inputThread.Start();

            player = new Player();
            Trophy trophy = new Trophy();
            //enemy = new Enemy();

            for (int x = 0; x < GridSize.X; ++x)
            {
                for (int y = 0; y < GridSize.Y; ++y)
                {
                    if (GameObjects[x, y] == null)
                        continue;
                    GameObjects[x, y].InitialDraw();
                }
            }

            //foreach (GameObject gameObject in GameObjects)
            //{
            //    if (gameObject == null)
            //        continue;
            //    gameObject.InitialDraw();
            //}

            Console.SetCursorPosition(0, 0);
        }

        public void UpdateBoard()
        {
            for (int x = 0; x < GridSize.X; ++x)
            {
                for (int y = 0; y < GridSize.Y; ++y)
                {
                    if (GameObjects[x, y] == null)
                        continue;
                    GameObjects[x, y].Update();
                }
            }

            //foreach (GameObject gameObject in GameObjects)
            //{
            //    if (gameObject == null)
            //        continue;
            //    gameObject.Update();
            //}
        }

        public void DrawBoard()
        {
            for (int x = 0; x < GridSize.X; ++x)
            {
                for (int y = 0; y < GridSize.Y; ++y)
                {
                    if (GameObjects[x, y] == null)
                        continue;
                    GameObjects[x, y].Draw();
                }
            }

            //foreach (GameObject gameObject in GameObjects)
            //{
            //    if (gameObject == null)
            //        continue;
            //    gameObject.Draw();
            //}
        }

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); 
        }
    }
}
