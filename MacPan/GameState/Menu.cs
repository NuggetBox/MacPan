using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MacPan
{
    public static class Menu
    {
        static ConsoleKey
                input,
                select = ConsoleKey.Enter,
                up = ConsoleKey.UpArrow,
                down = ConsoleKey.DownArrow;

        static Stopwatch stopwatch = new Stopwatch();

        static List<Button> buttons = new List<Button>();
        static Button selected;

        static int index, curMenu;
        public static bool GameRunning { get; set; }

        static float autoSave = 1000;

        // Creates a given menu with interactable buttons.
        static public void MenuCreator(int menuIndex)
        {
            curMenu = menuIndex;
            buttons.Clear();

            if (menuIndex == 0)
            {
                buttons.Add(new Button("Play", LevelSelect, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Stats", ViewStats, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Quit Game", QuitGame, ConsoleColor.White, ConsoleColor.Black));
            }
            if (menuIndex == 1)
            {
                buttons.Add(new Button("Level 1", OpenMap, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Back", MainMenu, ConsoleColor.White, ConsoleColor.Black));
            }
            if (menuIndex == 2)
            {
                buttons.Add(new Button("Back", MainMenu, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Reset", ResetStats, ConsoleColor.Red, ConsoleColor.Black));
            }
            if (menuIndex == 3)
            {
                buttons.Add(new Button("You Win! Press Enter to return to the main menu.", MainMenu, ConsoleColor.White, ConsoleColor.Black));
            }
            if (menuIndex == 4)
            {
                buttons.Add(new Button("You Lose! Press Enter to return to the main menu.", MainMenu, ConsoleColor.White, ConsoleColor.Black));
            }

            index = 0;
            selected = buttons[index];
            selected.backColor = ConsoleColor.DarkGray;
        }

        // Updates the buttons according to the users input.
        static public void Update()
        {
            ButtonUpdate(buttons);
            input = Console.ReadKey(true).Key;

            if (input == select)
            {
                selected.Push();
            }
            if (input == up)
            {
                if (index > 0)
                {
                    --index;
                }
            }
            if (input == down)
            {
                if (index < buttons.Count - 1)
                {
                    ++index;
                }
            }
            selected = buttons[index];

            for (int i = 0; i < buttons.Count; ++i)
            {
                if (buttons[i] != selected)
                    buttons[i].backColor = ConsoleColor.Black;
            }
            selected.backColor = ConsoleColor.DarkGray;
        }

        // Opens up the level selector.
        static public void LevelSelect()
        {
            MenuCreator(1);
        }

        // Resets the players stats.
        static public void ResetStats()
        {
            Statistics.ResetStats();
        }

        // Views the players stats.
        static public void ViewStats()
        {
            MenuCreator(2);
            Statistics.stats["Stats"].Add(1);
        }

        // Quits the game.
        static public void QuitGame()
        {
            Environment.Exit(0);
        }

        // Opens the given map and starts the game.
        static public void OpenMap()
        {
            if (index == 0)
            {
                stopwatch.Start();
                Game game = new Game();
                Statistics.stats["Games"].Add(1);
                GameRunning = true;

                while (GameRunning)
                {
                    game.UpdateBoard();
                    game.DrawBoard();
                    Statistics.stats["Frames"].Add(1);
                    if (stopwatch.ElapsedMilliseconds >= autoSave)
                    {
                        Statistics.SaveStats();
                        stopwatch.Restart();
                    }
                }
                if (Player.HealthPoints == 0)
                {
                    Player.Singleton = null;
                    game = null;
                    MenuCreator(4);
                }
                else
                {
                    Player.Singleton = null;
                    game = null;
                    MenuCreator(3);
                }
            }
        }

        // Returns to the main menu.
        static public void MainMenu()
        {
            MenuCreator(0);
        }

        // Writes out the "Game Name Art" and all the buttons.
        static public void ButtonUpdate(List<Button> buttons)
        {
            Console.Clear();

            if (curMenu == 2)
            {
                Statistics.SaveStats();
                Data data = FileWrite.Read(Program.Path + Statistics.statsPath);

                foreach (KeyValuePair<string, Stat> stat in data.stats)
                {
                    Console.WriteLine(stat.Value.Name + ": " + stat.Value.Value + " " + stat.Value.Unit);
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Program.GameNameArt + "\n");
                Console.ResetColor();
            }

            foreach (Button button in buttons)
            {
                button.DrawButton();
            }
        }
    }

    // A class for holding an interactable button.
    public class Button
    {
        public string Label { get; }

        Action function;
        public ConsoleColor backColor, foreColor;

        public Button(string label, Action function, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Label = label;
            this.function = function;
            this.backColor = backColor;
            this.foreColor = foreColor;
        }

        // Draws the button with the given background- and foreground color.
        public void DrawButton()
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            Console.WriteLine(Label);
            Console.ResetColor();
        }

        // Activates the buttons given method.
        public void Push()
        {
            Console.Clear();
            function.Invoke();
            Statistics.stats["Buttons"].Add(1);
        }
    }
}