﻿using System;
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
        Enemy enemy;
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

            enemy = new Enemy();

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
