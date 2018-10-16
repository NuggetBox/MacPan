using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Enemy : GameObject
    {
        public override void InitialDraw(int xSize, int ySize)
        {

        }

        public override void Draw()
        {

        }

        public List<Point> LineOfSight (Player player)
        {
            if (player.Position.X == Position.X)
            {
                if (player.Position.Y > Position.Y)
                {

                }
                else
                {

                }
            }

            bool rising;
            float m;
            float k;

            /*m = (playerY * guardX - playerX * guardY) / (guardX - playerX);
            k = (guardY - m) / guardX;*/

            return null;
        }
    }
}
