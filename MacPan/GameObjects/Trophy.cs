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
            if (wantsToGoBack == true)
                AttemptGoBack();

            OldPosition = Position;
        }

        public override void Draw() { }

        public override void InitialDraw()
        {
            base.Draw();
        }

        public void PickUp()
        {
            oGPos = Position;
            Position = new Point( ReadMap.TrophyBarOffset + Player.HeldTrophies + Player.CollectedTrophies, ReadMap.MapHeight + ReadMap.TrophyBarOffset);
            base.Draw();
            Stats.stats["Trophies"].Add(1);
        }

        public void AttemptGoBack()
        {
            if (Game.GameObjects[oGPos.X, oGPos.Y] == null)
            {
                Position = oGPos;
                base.Draw();
            }
            else
            {
                wantsToGoBack = true;
            }
        }
    }
}
