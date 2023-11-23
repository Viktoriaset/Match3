using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class HorizontalDestroyerBonusCommand : BaseBonus, ICloneable
    {
        private Point positionLeftDestroyr;
        private Point positionRightDestroyr;
        private Point startCoordinate;
        private GameField gameField;
        private int destroyrSpeed = 10;

        public HorizontalDestroyerBonusCommand(Bitmap bitmap, int points) : base(bitmap, points)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override int UseBonus(Point point, GameField gameField, Timer timer)
        {
            Figure figure = gameField.GetElement(point.X, point.Y);
            positionLeftDestroyr = new Point(figure.position.X, figure.position.Y);
            positionRightDestroyr = new Point(figure.position.X, figure.position.Y);

            startCoordinate = new Point(point.X, point.Y);
            this.gameField = gameField;

            return points;
        }

        public override bool Draw(Graphics g)
        {

            int rightBorder = gameField._matrixDrawingStartPoint + gameField.rowsCount * gameField._figureCellSize;

            if (positionLeftDestroyr.X < gameField._matrixDrawingStartPoint
                && positionRightDestroyr.X > rightBorder)
            {
                return true;
            }

            for (int i = 0; i < startCoordinate.X; i++)
            {
                if (gameField.GetElement(i, startCoordinate.Y).position.X >= positionLeftDestroyr.X)
                {
                    gameField.DestroyElement(new Point(i, startCoordinate.Y));
                }
            }

            positionLeftDestroyr.X -= destroyrSpeed;

            for (int i = startCoordinate.X + 1; i < gameField.columnsCount; i++)
            {
                if (gameField.GetElement(i, startCoordinate.Y).position.X <= positionRightDestroyr.X)
                {
                    gameField.DestroyElement(new Point(i, startCoordinate.Y));
                }
            }

            positionRightDestroyr.X += destroyrSpeed;

            DrawDestroyer(positionLeftDestroyr, g);
            DrawDestroyer(positionRightDestroyr, g);

            return false;
        }

        private void DrawDestroyer(Point position, Graphics g)
        {
            Rectangle rectangle = new Rectangle(
                position.X,
                position.Y,
                gameField._figureCellSize,
                gameField._figureCellSize
            );
            g.DrawImage(bitmap, rectangle);
        }
    }
}
