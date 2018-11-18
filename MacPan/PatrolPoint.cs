using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    // Merely a container for a point as well as a character. Required in the readmap class when creating enemies.
    class PatrolPoint
    {
        public Point Position { get; set; }
        public Char PatrolPointIndex { get; set; }

        public PatrolPoint(Point position, Char patrolPointIndex)
        {
            Position = position;
            PatrolPointIndex = patrolPointIndex;
        }
    }
}
