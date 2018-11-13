using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacPan
{
    public static class Menu
    {
        static ConsoleKey
                input,
                select = ConsoleKey.Enter,
                up = ConsoleKey.UpArrow,
                down = ConsoleKey.DownArrow;
                //left = ConsoleKey.LeftArrow,
                //right = ConsoleKey.RightArrow;

        static List<Button> buttons = new List<Button>();
        static Button selected;

        static int index, curMenu;

        static public void MenuCreator(int menuIndex)
        {
            curMenu = menuIndex;
            buttons.Clear();

            if (menuIndex == 0)
            {
                buttons.Add(new Button("Play", StartGame, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Stats", ViewStats, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Quit Game", QuitGame, ConsoleColor.White, ConsoleColor.Black));
            }
            if (menuIndex == 1)
            {
                buttons.Add(new Button("Level 1", OpenMap, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Back", Back, ConsoleColor.White, ConsoleColor.Black));
            }
            if (menuIndex == 2)
            {
                buttons.Add(new Button("Back", Back, ConsoleColor.White, ConsoleColor.Black));
                buttons.Add(new Button("Reset", ResetStats, ConsoleColor.Red, ConsoleColor.Black));
            }

            index = 0;
            selected = buttons[index];
            selected.backColor = ConsoleColor.DarkGray;
        }

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

        static public void StartGame()
        {
            MenuCreator(1);
        }

        static public void ResetStats()
        {
            Stats.ResetStats();
        }

        static public void ViewStats()
        {
            MenuCreator(2);
            Stats.stats["Stats"].Add(1);
        }

        static public void QuitGame()
        {
            Environment.Exit(0);
        }

        static public void OpenMap()
        {
            if (index == 0)
            {
                Game game = new Game();

                while (true)
                {
                    game.UpdateBoard();
                    game.DrawBoard();
                    Stats.stats["Frames"].Add(1);
                    Stats.SaveStats();
                }
            }
        }

        static public void Back()
        {
            MenuCreator(0);
        }

        static public void ButtonUpdate(List<Button> buttons)
        {
            Console.Clear();

            if (curMenu == 2)
            {
                Stats.SaveStats();
                Data data = FileWrite.Read(Program.Path + Stats.statsPath);

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

        public void DrawButton()
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            Console.WriteLine(Label);
            Console.ResetColor();
        }

        public void Push()
        {
            Console.Clear();
            function.Invoke();
            Stats.stats["Buttons"].Add(1);
        }
    }
}