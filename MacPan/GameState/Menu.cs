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
        public Button selected;
        public Menu(List<Button> buttons)
        {
            ConsoleKey 
                input, 
                select = ConsoleKey.Enter,
                up = ConsoleKey.UpArrow, 
                down = ConsoleKey.DownArrow, 
                left = ConsoleKey.LeftArrow, 
                right = ConsoleKey.RightArrow;

            int index = 0;
            selected = buttons[index];

            while (true)
            {
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
            }
        }

        public void StartGame()
        {
            Game game = new Game();

            while (true)
            {
                Console.CursorVisible = false;
                game.UpdateBoard();
                game.DrawBoard();
            }
        }

        public void Stats()
        {
            // Skriv ut alla stats
        }

        public void QuitGame()
        {
            Environment.Exit(0);
        }

        public void Update(List<Button> buttons)
        {
            foreach(Button button in buttons)
            {
                button.Update();
            }
        }
    }

    class Button
    {
        public string Label { get; }

        Action function;
        ConsoleColor backColor, foreColor;

        public Button(string label, Action function, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Label = label;
            this.function = function;
            this.backColor = backColor;
            this.foreColor = foreColor;
        }

        public void Update()
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
        }
    }
}