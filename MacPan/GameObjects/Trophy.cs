using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPan
{
    class Trophy : GameObject
    {
        Point oGPos;
        bool wantsToGoBack;

        public Trophy()
        {
            Color = ConsoleColor.Yellow;
        }

        public override void Update()
        {
            OldPosition = Position;

            if (wantsToGoBack == true)
                AttemptGoBack();
        }

        public override void Draw() { }

        public override void InitialDraw()
        {
            base.Draw();
        }

        // Method for picking up a trophy.
        // Adds 1 to your trophy score and adds the trophy to your trophy bar.
        public void PickUp()
        {
            oGPos = Position;
            Position = new Point( ReadMap.TrophyBarOffset + Player.HeldTrophies + Player.CollectedTrophies, ReadMap.MapHeight + ReadMap.TrophyBarOffset);
            base.Draw();
            Statistics.stats["Trophies"].Add(1);
        }

        // Attempts to return a trophy you are holding to its original position.
        // If an enemy happens to path there, the trophy is spawned there when it is able to.
        public void AttemptGoBack()
        {
            if (Game.GameObjects[oGPos.X, oGPos.Y] == null)
            {
                Position = oGPos;
                base.Draw();
                wantsToGoBack = false;
                Statistics.stats["Returned"].Add(1);
            }
            else
            {
                wantsToGoBack = true;
            }
        }
    }
}
