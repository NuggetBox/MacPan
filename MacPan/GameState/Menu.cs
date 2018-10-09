using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Menu
    {
        public Menu(List<Button> buttons)
        {

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

        public void QuitGame()
        {
            Environment.Exit(0);
        }

        public void Update()
        {

        }
    }

    class Button
    {
        public string Label { get; }
        Action function;

        public Button(string label, Action function)
        {
            Label = label;
            this.function = function;
        }

        public void Trigger() => function.Invoke();
    }
}