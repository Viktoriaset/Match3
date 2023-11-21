using System.Drawing;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    public abstract class IBonusCommand
    {
        public Bitmap bitmap;
        protected int points;
        public abstract int UseBonus(Point point, GameField gameField);
    }
}
