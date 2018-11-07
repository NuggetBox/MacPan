using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacPan
{
    class Menu
    {
        ConsoleKey
                input,
                select = ConsoleKey.Enter,
                up = ConsoleKey.UpArrow,
                down = ConsoleKey.DownArrow;
                //left = ConsoleKey.LeftArrow,
                //right = ConsoleKey.RightArrow;

        List<Button> buttons;
        Button selected;

        int index = 0;

        public Menu(int menuIndex)
        {
            buttons = new List<Button>();
            MenuConstructor(menuIndex);
        }

        public void MenuConstructor(int menuIndex)
        {
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
                
            }

            index = 0;
            selected = buttons[index];
            selected.backColor = ConsoleColor.DarkGray;
        }

        public void Update()
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

        public void StartGame()
        {
            MenuConstructor(1);
        }

        public void ViewStats()
        {
            MenuConstructor(2);
            Stats.stats["Stats"].Add(1);
        }

        public void QuitGame()
        {
            Environment.Exit(0);
        }

        public void OpenMap()
        {
            if (index == 0)
            {
                Game game = new Game();

                while (true)
                {
                    game.UpdateBoard();
                    game.DrawBoard();
                    Stats.stats["Frames"].Add(1);
                }
            }
        }

        public void Back()
        {
            MenuConstructor(0);
        }

        public void ButtonUpdate(List<Button> buttons)
        {
            Console.Clear();
            foreach (Button button in buttons)
            {
                button.DrawButton();
            }
        }
    }

    class Button
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
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Push()
        {
            Console.Clear();
            function.Invoke();
            Stats.stats["Buttons"].Add(1);
        }
    }
}