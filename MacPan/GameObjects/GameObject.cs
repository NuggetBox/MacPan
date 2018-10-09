using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    abstract class GameObject
    {
        public Point position;
        public abstract void InitialDraw();
        public abstract void Draw();
        public abstract void Update();
    }
}
