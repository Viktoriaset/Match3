using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

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
            int douwnBorder = gameField._matrixDrawingStartPoint + gameField.rowsCount * gameField._figureCellSize;

            if (positionFirstDestroyr.Y <= gameField._matrixDrawingStartPoint
                && positionSecondDestroyr.Y >= douwnBorder)
            {
                return true;
            }

            for (int i = 0; i < startCoordinate.Y; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).position.Y >= positionFirstDestroyr.Y)
                {
                    gameField.DestroyElement(new Point(startCoordinate.X, i));
                }
            }

            positionFirstDestroyr.Y -= destroyrSpeed;

            for (int i = startCoordinate.Y + 1; i < gameField.rowsCount; i++)
            {
                if (gameField.GetElement(startCoordinate.X, i).position.Y <= positionSecondDestroyr.Y)
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
