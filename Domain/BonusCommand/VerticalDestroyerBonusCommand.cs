using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCommand : IBonusCommand, ICloneable
    {
        public VerticalDestroyerBonusCommand(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override int UseBonus(Point point, GameField gameField)
        {
            for (int i = 0; i < gameField.rowsCount; i++)
            {
                if (i != point.Y)
                {
                   gameField.DestroyElement(new Point(i, point.Y));
                }
            }

            return points;
        }
    }
}
