using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    abstract class GameObject
    {
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }

        public abstract void InitialDraw(int xSize, int ySize);
        public abstract void Draw(int xSize, int ySize);
        public abstract void Update(GameObject[,] gameObjects);
    }
}
