using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class HorizontalDestroyerBonusCommand : BaseDestroyerBonus, ICloneable
    {
        public HorizontalDestroyerBonusCommand(
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

            int rightBorder = gameField._matrixDrawingStartPoint + gameField.rowsCount * gameField._figureCellSize;

            if (positionFirstDestroyr.X < gameField._matrixDrawingStartPoint
                && positionSecondDestroyr.X > rightBorder)
            {
                return true;
            }

            for (int i = 0; i < startCoordinate.X; i++)
            {
                if (gameField.GetElement(i, startCoordinate.Y).Position.X >= positionFirstDestroyr.X)
                {
                    gameField.DestroyElement(new Point(i, startCoordinate.Y));
                }
            }

            positionFirstDestroyr.X -= destroyrSpeed;

            for (int i = startCoordinate.X + 1; i < gameField.columnsCount; i++)
            {
                if (gameField.GetElement(i, startCoordinate.Y).Position.X <= positionSecondDestroyr.X)
                {
                    gameField.DestroyElement(new Point(i, startCoordinate.Y));
                }
            }

            positionSecondDestroyr.X += destroyrSpeed;

            DrawDestroyer(positionFirstDestroyr, g, firstDestroyerBitmap);
            DrawDestroyer(positionSecondDestroyr, g, secondDestroyerBitmap);

            return false;
        }
    }
}
