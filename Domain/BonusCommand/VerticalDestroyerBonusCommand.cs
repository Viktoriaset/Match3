using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class VerticalDestroyerBonusCommand : BaseBonus, ICloneable
    {
        public VerticalDestroyerBonusCommand(Bitmap bitmap, int points) : base(bitmap, points)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override int UseBonus(Point point, GameField gameField)
        {
            for (int i = 0; i < gameField.rowsCount; i++)
            {
                if (i != point.Y)
                {
                   gameField.DestroyElement(new Point(point.X, i));
                }
            }

            return points;
        }
    }
}
