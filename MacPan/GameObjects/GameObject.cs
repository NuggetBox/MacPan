using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    public abstract class GameObject
    {
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }

        public virtual void InitialDraw() { }
        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
