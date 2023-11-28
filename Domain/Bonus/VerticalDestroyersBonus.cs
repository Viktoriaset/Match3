
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;
using ThreeInRow.Domain.BonusCommand;
using ThreeInRow.Domain.Extensions;

namespace ThreeInRow.Domain.Bonus
{
    public class VerticalDestroyersBonus : BaseBonus
    {
        private int destroyerSpeed = 20;

        public VerticalDestroyersBonus(Bitmap bitmap, int pointsSet) : base(bitmap, pointsSet)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Draw(Graphics g)
        {
            return true;
        }

        public override int UseBonus(Point point, GameField gameField, Timer timer)
        {
            Figure figure = gameField.GetElement(point.X, point.Y);

            Destroyer firstDestroyer
                = DestroyerCreator.UpDestroyerCreator(gameField, figure.Position.Copy(), destroyerSpeed);

            Destroyer secondDestroyer
                = DestroyerCreator.DouwnDestroyerCreator(gameField, figure.Position.Copy(), destroyerSpeed);

            Utility.CreateGameObject(firstDestroyer);
            Utility.CreateGameObject(secondDestroyer);

            return points;
        }
    }
}