using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
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
