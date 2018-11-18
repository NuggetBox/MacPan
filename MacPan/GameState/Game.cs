using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace MacPan
{
    class Game
    {
        public static GameObject[,] GameObjects { get; set; }
        public static Point GridSize { get; set; }
        public static Point BoxSize { get; set; }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        
        // Initializes the game, reads and draws the map.
        public Game()
        {
            BoxSize = new Point(2, 1);
            GridSize = new Point(120, 46);
            GameObjects = new GameObject[GridSize.X, GridSize.Y];

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Maximize();
            Console.CursorVisible = false;

            ReadMap.InitializeMap();

            InitializeBoard();

            Console.SetCursorPosition(0, 0);
        }

        // Draws all objects once as the game starts. Objects like walls are only drawn here.
        public void InitializeBoard()
        {
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Program.InGameNameArt);
            Console.ResetColor();

            for (int x = 0; x < GridSize.X; ++x)
            {
                for (int y = 0; y < GridSize.Y; ++y)
                {
                    if (GameObjects[x, y] == null)
                        continue;
                    GameObjects[x, y].InitialDraw();
                }
            }
        }

        // Runs the Update method for all GameObjects. Objects like walls are not updated.
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
        }

        // Draws all GameObjects. Objects like walls are not drawn every frame.
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
        }

        // Maximizes the Console window.
        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
        }
    }
}
