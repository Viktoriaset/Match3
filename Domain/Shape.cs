using System.Drawing;

namespace ThreeInRow.Domain
{
    public abstract class Shape
    {
        private Size _startSize;
        private Size _size;
        public Bitmap Bitmap { get; set; }
        public ShapeType Type { get; protected set; }

        public Shape(ShapeType type, Bitmap bitmap)
        {
            Bitmap = bitmap;
            _startSize = new Size(60, 60);
            _size = _startSize;
            Type = type;
        }

        public abstract int Destroy(Point position);

        public void Draw(Graphics g, Point position)
        {
            Rectangle rectangle = new Rectangle(position.X, position.Y, _size.Width, _size.Height);
            g.DrawImage(Bitmap, rectangle);
        }

        public void Select()
        {
            _size.Width += (_size.Width * 20) / 100;
            _size.Height += (_size.Height * 20) / 100;
        }

        public void UnSelect()
        {
            _size = _startSize;
        }

        public Size GetSize()
        {
            return _size;
        }

        public static bool operator ==(Shape a, Shape b)
        {
            return a.Type == b.Type;
        }

        public static bool operator !=(Shape a, Shape b)
        {
            return a.Type != b.Type;
        }
    }

    public enum ShapeType
    {
        Circle,
        Square,
        Triangle,
        Pentagon,
        Hexagon,
        Empty
    }
}
