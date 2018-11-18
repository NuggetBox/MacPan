using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    // Differs from a wall in three ways. Different name, different color, and it cointains the SecureTrophy method.
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

        // This method is called upon when the player interacts with a goal and it goes trough every object on the row where the unsecured trophies are supposed to be and moves them down to the row where the secured tophies are supposed to be.
        public void SecureTrophy()
        {
            for (int i = ReadMap.TrophyBarOffset + Player.CollectedTrophies; i < ReadMap.TrophyBarOffset + Player.HeldTrophies + Player.CollectedTrophies; ++i)
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
