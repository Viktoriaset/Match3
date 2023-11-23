using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class VerticalDestroyerBonusCommand : BaseBonus, ICloneable
    {
        private Point positionUpDestroyr;
        private Point positionDouwnDestroyr;
        private Point startCoordinate;
        private GameField gameField;
        private int destroyrSpeed = 10;

        public VerticalDestroyerBonusCommand(Bitmap bitmap, int points) : base(bitmap, points)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override int UseBonus(Point point, GameField gameField, Timer timer)
        {
            Figure figure = gameField.GetElement(point.X, point.Y);
            positionUpDestroyr = new Point(figure.position.X, figure.position.Y);
            positionDouwnDestroyr = new Point(figure.position.X, figure.position.Y);

            startCoordinate = new Point(point.X, point.Y);
            this.gameField = gameField;

            return points;
        }

        public override bool Draw(Graphics g)
        {
            int douwnBorder = gameField._matrixDrawingStartPoint + gameField.rowsCount * gameField._figureCellSize;

            if (positionUpDestroyr.Y <= gameField._matrixDrawingStartPoint
                && positionDouwnDestroyr.Y >= douwnBorder)
            {
                return true;
            }

            for (int i = 0; i < startCoordinate.Y; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).position.Y >= positionUpDestroyr.Y)
                {
                    gameField.DestroyElement(new Point(startCoordinate.X, i));
                }
            }

            positionUpDestroyr.Y -= destroyrSpeed;

            for (int i = startCoordinate.Y + 1; i < gameField.rowsCount; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).position.Y <= positionDouwnDestroyr.Y)
                {
                    gameField.DestroyElement(new Point(startCoordinate.X, i));
                }
            }

            positionDouwnDestroyr.Y += destroyrSpeed;

            DrawDestroyer(positionUpDestroyr, g);
            DrawDestroyer(positionDouwnDestroyr, g);

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
            g.DrawImage(bitmap, rectangle );
        }
    }
}
