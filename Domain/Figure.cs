using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Domain;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Back
{
    public class Figure : ICloneable, IHolderBitmap
    {
        private int _points;
        private Size _startSize;
        private Size _size;
        public Animator FallAnimator;
        public BaseBonus Bonus;
        public Point position;

        public FigureType Type { get; private set; }
        public Bitmap Bitmap { get;  set; }

        public Figure(int points, FigureType type, Bitmap bitmap)
        {
            _points = points;
            Type = type;
            Bitmap = bitmap;
            _startSize = new Size(60, 60);
            _size = _startSize;
        }

        public void Draw(Graphics g)
        {
            if (Type == FigureType.Empty) return;

            Rectangle rectangle = new Rectangle(position.X, position.Y, _size.Width, _size.Height);
            g.DrawImage(Bitmap, rectangle);
            if (HasBonus())
            {
                g.DrawImage(Bonus.bitmap, rectangle);
            }
        }

        public BaseBonus ExtractBonus()
        {
            BaseBonus bonus = Bonus;
            Bonus = null;
            return bonus;
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

        public bool HasBonus()
        {
            return Bonus == null ? false : true;
        }

        public int Destroy()
        {
            if (Type == FigureType.Empty) return 0;

            Bitmap = Resource1.Image1;
            Type = FigureType.Empty;
            Bonus = null;

            return _points;
        }

        public object Clone()
        {
            return new Figure(_points, Type, (Bitmap)Bitmap.Clone());
        }

        public static bool operator ==(Figure a, Figure b)
        {
            if (a.Type == FigureType.Empty || b.Type == FigureType.Empty) return false;
            return a.Type == b.Type;
        }

        public static bool operator !=(Figure a, Figure b)
        {
            if (a.Type == FigureType.Empty || b.Type == FigureType.Empty) return true;
            return a.Type != b.Type;
        }

        public Size GetSize()
        {
            return _size;
        }

        public void SetBitmap(Bitmap bitmap)
        {
            Bitmap = bitmap;
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
