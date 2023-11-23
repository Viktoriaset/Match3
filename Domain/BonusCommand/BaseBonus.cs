using System;
using System.Drawing;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    public abstract class BaseBonus: ICloneable
    {
        public Bitmap bitmap;
        protected int points;

        public BaseBonus(Bitmap bitmap, int points)
        {
            this.bitmap = bitmap;
            this.points = points;
        }

        public abstract object Clone();
    
        public abstract int UseBonus(Point point, GameField gameField);
    }
}
