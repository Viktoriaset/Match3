using System;
using System.Drawing;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class VerticalDestroyerBonusCommand : BaseDestroyerBonus, ICloneable
    {
        public VerticalDestroyerBonusCommand(
            Bitmap bitmap,
            int points,
            Bitmap firstDestroyerBitmap,
            Bitmap secondDestroyerBitmap) : base(bitmap, points, firstDestroyerBitmap, secondDestroyerBitmap)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Draw(Graphics g)
        {
            int douwnBorder = gameField.MatrixDrawingStartPoint + gameField.rowsCount * gameField.FigureCellSize;

            if (positionFirstDestroyr.Y <= gameField.MatrixDrawingStartPoint
                && positionSecondDestroyr.Y >= douwnBorder)
            {
                return true;
            }

            for (int i = 0; i < startCoordinate.Y; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).Position.Y >= positionFirstDestroyr.Y)
                {
                    gameField.DestroyElement(new Point(startCoordinate.X, i));
                }
            }

            positionFirstDestroyr.Y -= destroyrSpeed;

            for (int i = startCoordinate.Y + 1; i < gameField.rowsCount; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).Position.Y <= positionSecondDestroyr.Y)
                {
                    gameField.DestroyElement(new Point(startCoordinate.X, i));
                }
            }

            positionSecondDestroyr.Y += destroyrSpeed;

            DrawDestroyer(positionFirstDestroyr, g, firstDestroyerBitmap);
            DrawDestroyer(positionSecondDestroyr, g, secondDestroyerBitmap);

            return false;
        }
    }
}
