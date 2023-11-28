using ThreeInRow.Back;
using System.Drawing;

namespace ThreeInRow.Domain
{
    public static class DestroyerCreator
    {
        public static Destroyer RightDestroyerCreator(GameField gameField, Point position, int speed)
        {
            Bitmap bitmap = Resource1.destroyer_up;
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return new Destroyer(gameField, position, speed, new Point(1, 0), bitmap);
        }

        public static Destroyer LeftDestroyerCreator(GameField gameField, Point position, int speed)
        {
            Bitmap bitmap = Resource1.destroyer_up;
            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);

            return new Destroyer(gameField, position, speed, new Point(-1, 0), bitmap);
        }

        public static Destroyer UpDestroyerCreator(GameField gameField, Point position, int speed)
        {
            Bitmap bitmap = Resource1.destroyer_up;

            return new Destroyer(gameField, position, speed, new Point(0, -1), bitmap);
        }

        public static Destroyer DouwnDestroyerCreator(GameField gameField, Point position, int speed)
        {
            Bitmap bitmap = Resource1.destroyer_up;
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);

            return new Destroyer(gameField, position, speed, new Point(0, 1), bitmap);
        }
    }
}