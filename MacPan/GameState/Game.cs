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
        Thread playerThread;

        public static GameObject[,] GameObjects { get; set; }
        public static Point GridSize { get; set; }
        public static Point BoxSize { get; set; }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        public Game()
        {
            GridSize = new Point(120, 31);
            BoxSize = new Point(2, 2);
            GameObjects = new GameObject[GridSize.X, GridSize.Y];

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Maximize();
            Console.CursorVisible = false;

            Player player = new Player();
            playerThread = new Thread(new ThreadStart(player.UpdateDraw));
            playerThread.Start();

            //Enemy enemy = new Enemy();

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

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); 
        }
    }
}
