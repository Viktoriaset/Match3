using System;
using System.Collections.Generic;
using System.Drawing;
using ThreeInRow.Domain;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Back
{
    public class Figure : ICloneable
    {
        public Animator FallAnimator;
        public Animator SelectFigureAnimator;
        public Animator DestroyFigureAnimator;
        public BaseBonus Bonus;
        public Point Position;
        public bool IsFalling = false;
        public bool IsSelected = false;
        public bool IsDestroyd = false;

        private int _points;
        private Size _startSize;
        private Size _size;

        public FigureType Type { get; private set; }
        public Bitmap Bitmap;

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
                FallAnimator.DrawNextSprite(Position, g, _size);
            }
            else if (IsDestroyd)
            {
                if (DestroyFigureAnimator.DrawNextSprite(Position, g, _size))
                {
                    Bitmap = Resource1.Image1;
                    Type = FigureType.Empty;
                    IsDestroyd = false;
                }
            }
            else if (IsSelected)
            {
                SelectFigureAnimator.DrawNextSprite(Position, g, _size);
            }
            else
            {
                FallAnimator.DrawStaticBitmap(Position, g, _size);
            }

            if (HasBonus())
            {
                Rectangle rectangle = new Rectangle(Position.X, Position.Y, _size.Width, _size.Height);
                g.DrawImage(Bonus.Bitmap, rectangle);
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

            if (IsDestroyd) return 0;

            IsDestroyd = true;
            Bonus = null;

            return _points;
        }

        public object Clone()
        {
            Figure figure = new Figure(_points, Type, (Bitmap)Bitmap.Clone());
            figure.FallAnimator = (Animator)FallAnimator.Clone();
            figure.DestroyFigureAnimator = (Animator)DestroyFigureAnimator.Clone();
            figure.SelectFigureAnimator = (Animator)SelectFigureAnimator.Clone();

            return figure;
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

        public override bool Equals(object obj)
        {
            return obj is Figure figure &&
                   EqualityComparer<Point>.Default.Equals(Position, figure.Position) &&
                   Type == figure.Type;
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
