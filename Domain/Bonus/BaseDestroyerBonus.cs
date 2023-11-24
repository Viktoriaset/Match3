using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal abstract class BaseDestroyerBonus : BaseBonus
    {
        protected Point positionFirstDestroyr;
        protected Point positionSecondDestroyr;
        protected Point startCoordinate;
        protected Bitmap firstDestroyerBitmap;
        protected Bitmap secondDestroyerBitmap;
        protected GameField gameField;
        protected int destroyrSpeed = 20;

        public BaseDestroyerBonus(
            Bitmap bitmap,
            int points,
            Bitmap firstDestroyerBitmapSet,
            Bitmap secondDestroyerBitmapSet) : base(bitmap, points)
        {
            firstDestroyerBitmap = firstDestroyerBitmapSet;
            secondDestroyerBitmap = secondDestroyerBitmapSet;
        }

        public override int UseBonus(Point point, GameField gameFieldSet, Timer timer)
        {
            gameField = gameFieldSet;

            Figure figure = gameField.GetElement(point.X, point.Y);
            positionFirstDestroyr = new Point(figure.Position.X, figure.Position.Y);
            positionSecondDestroyr = new Point(figure.Position.X, figure.Position.Y);

            startCoordinate = new Point(point.X, point.Y);

            return points;
        }

        protected void DrawDestroyer(Point position, Graphics g, Bitmap bitmap)
        {
            Rectangle rectangle = new Rectangle(
                position.X,
                position.Y,
                gameField.FigureCellSize,
                gameField.FigureCellSize
            );
            g.DrawImage(bitmap, rectangle);
        }
    }
}
