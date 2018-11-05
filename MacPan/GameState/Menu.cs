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

        public Menu(List<Button> buttons)
        {
            buttons.Add(new Button("Start Game", StartGame, ConsoleColor.White, ConsoleColor.Black));
            buttons.Add(new Button("Stats", ViewStats, ConsoleColor.White, ConsoleColor.Black));
            buttons.Add(new Button("Quit Game", QuitGame, ConsoleColor.White, ConsoleColor.Black));

            this.buttons = buttons;
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
            ++Stats.gamesStarted;
            Game game = new Game();

            try
            {
                while (true)
                {
                    game.UpdateBoard();
                    game.DrawBoard();
                    ++Stats.frames;
                }
            }
            catch
            {
                ++Stats.crashed;
                Stats.SaveStats();
            }
        }

        public void ViewStats()
        {
            // Skriv ut alla stats
            ++Stats.statsViewed;
        }

        public void QuitGame()
        {
            ++Stats.quit;
            Stats.timePlayed += Program.gameTime.ElapsedMilliseconds;
            Stats.SaveStats();


            Program.gameTime.Reset();
            Environment.Exit(0);
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
            ++Stats.buttonsPressed;
            Console.Clear();
            function.Invoke();
        }
    }
}