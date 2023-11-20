using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Back
{
    public class Figure : Cell, ICloneable
    {
        private int points;
        public FigureType _type { get; private set; }
        public Bitmap _bitmap { get; set; }
        private Size _startSize;
        private Size _size;

        public Figure(int points, FigureType type, Bitmap bitmap)
        {
            this.points = points;
            _type = type;
            _bitmap = bitmap;
            _startSize = new Size(60, 60);
            _size = _startSize;
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

        public override int Destroy()
        {
            _bitmap = Resource1.Image1;
            _type = FigureType.Empty;
            return points;
        }

        public object Clone()
        {
            return new Figure(points, _type, (Bitmap)_bitmap.Clone());
        }

        public static bool operator ==(Figure a, Figure b)
        {
            return a._type == b._type;
        }

        public static bool operator !=(Figure a, Figure b)
        {
            return a._type != b._type;
        }

        public Size GetSize()
        {
            return _size;
        }
    }

    public enum FigureType
    {
        Circle,
        Square,
        Triangle,
        Pentagon,
        Hexagon, 
        Empty
    }
}
