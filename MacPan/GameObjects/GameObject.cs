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

        public virtual void InitialDraw(int xSize, int ySize) { }
        public virtual void Draw(int xSize, int ySize) { }
        public virtual void Update(GameObject[,] gameObjects) { }
    }
}
