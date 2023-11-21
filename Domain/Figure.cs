using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Domain;

namespace ThreeInRow.Back
{
    public class Figure : Shape, ICloneable
    {
        private int _points;
        
        public Figure(int points, ShapeType type, Bitmap bitmap): base(type, bitmap)
        {
            _points = points;
        }

        public override int Destroy(Point position)
        {
            if (Type == ShapeType.Empty) return 0;

            Bitmap = Resource1.Image1;
            Type = ShapeType.Empty;

            return _points;
        }

        public object Clone()
        {
            return new Figure(_points, Type, (Bitmap)Bitmap.Clone());
        }
    }
}
