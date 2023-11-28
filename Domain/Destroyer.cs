using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    public class Destroyer : IGameObject
    {
        public Point Position { get; private set; }
        public int Speed { get; private set; }
        public Point Direction { get; private set; }

        private GameField _gameField;
        private Bitmap bitmap;

        public Destroyer(GameField gameField, Point position, int speed, Point direction, Bitmap bitmapSet)
        {
            _gameField = gameField;
            Position = position;
            Speed = speed;
            Direction = direction;
            bitmap = bitmapSet;
        }

        public void Initialization()
        {
            
        }

        public void Update(object sender, PaintEventArgs e)
        {
            Move();
            Draw(e.Graphics);
            DestroyElement();
        }

        private void Move()
        {
            int rightBorder = _gameField.MatrixDrawingStartPoint + _gameField.rowsCount * _gameField.FigureCellSize;

            if (Position.X < _gameField.MatrixDrawingStartPoint
                && Position.X > rightBorder
                && Position.Y < _gameField.MatrixDrawingStartPoint
                && Position.Y > rightBorder)
            {
                return;
            }

            Point newPosition = new Point(Position.X + Speed * Direction.X, Position.Y + Speed * Direction.Y);

            Position = newPosition;
        }

        private void Draw(Graphics g)
        {
            Rectangle rectangle = new Rectangle(
                Position.X,
                Position.Y,
                _gameField.FigureCellSize,
                _gameField.FigureCellSize
            );
            g.DrawImage(bitmap, rectangle);
        }

        private void DestroyElement()
        {
            Point elementCoordinate = _gameField.GetElementCoordinate(Position);
            _gameField.DestroyElement(elementCoordinate);
        }
    }
}
