using System;
using System.Drawing;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class HorizontalDestroyerBonusCommand : BaseBonus, ICloneable
    {
        public HorizontalDestroyerBonusCommand(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override int UseBonus(Point point, GameField gameField)
        {
            for (int i = 0; i < gameField.columnsCount; i++)
            {
                if (i != point.X)
                {
                    gameField.DestroyElement(new Point(i, point.Y));
                }
            }

            return points;
        }
    }
}
