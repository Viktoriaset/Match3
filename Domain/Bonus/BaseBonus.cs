using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    public abstract class BaseBonus: ICloneable
    {
        public Bitmap Bitmap;
        protected int points;

        public BaseBonus(Bitmap bitmap, int pointsSet)
        {
            Bitmap = bitmap;
            this.points = pointsSet;
        }

        public abstract object Clone();
    
        public abstract int UseBonus(Point point, GameField gameFieldSet, Timer timer);

        public abstract bool Draw(Graphics g);
    }
}
