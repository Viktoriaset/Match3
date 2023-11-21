using System;
using System.Drawing;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCommand : IBonusCommand, ICloneable
    {
        public HorizontalDestroyerBonusCommand(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public object Clone()
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
