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
    public class Figure : ICloneable
    {
        private int _points;
        private Size _startSize;
        private Size _size;

        public Animator FallAnimator;
        public Animator SelectFigureAnimator;
        public BaseBonus Bonus;
        public Point position;
        public bool IsFalling = false;
        public bool IsSelected = false;
        public bool IsDestroyd = false;

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

            if (IsFalling)
            {
                FallAnimator.DrawNextSprite(position, g, _size);
            }
            else if (IsSelected)
            {
                SelectFigureAnimator.DrawNextSprite(position, g, _size);
            }
            else
            {
                FallAnimator.DrawStaticBitmap(position, g, _size);
            }

            if (HasBonus())
            {
                Rectangle rectangle = new Rectangle(position.X, position.Y, _size.Width, _size.Height);
                g.DrawImage(Bonus.bitmap, rectangle);
            }
        }

        public BaseBonus ExtractBonus()
        {
            BaseBonus bonus = Bonus;
            Bonus = null;
            return bonus;
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
            return MemberwiseClone();
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
