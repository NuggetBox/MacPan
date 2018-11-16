using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Goal : GameObject
    {
        public Goal()
        {
            Color = ConsoleColor.Green;
        }

        public override void Update()
        {
            OldPosition = Position;
        }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public override void Draw() { }

        public void SecureTrophy(int heldTrophies, int collectedTrophies)
        {
            for (int i = ReadMap.TrophyBarOffset + collectedTrophies; i < ReadMap.TrophyBarOffset + heldTrophies + collectedTrophies; ++i)
            {
                if (Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset] != null)
                {
                    Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset].Position = new Point(i, ReadMap.MapHeight + ReadMap.TrophyBarOffset + 1);
                    Game.GameObjects[i, ReadMap.MapHeight + ReadMap.TrophyBarOffset].InitialDraw();
                }
            }
        }
    }
}
