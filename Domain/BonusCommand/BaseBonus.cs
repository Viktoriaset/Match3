using System;
using System.Drawing;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    public abstract class BaseBonus: ICloneable
    {
        public Bitmap bitmap;
        protected int points;

        public abstract object Clone();
    
        public abstract int UseBonus(Point point, GameField gameField);
    }
}
