using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    public class Animator: ICloneable
    {
        public List<Bitmap> SpriteList = new List<Bitmap>();
        public Bitmap StaticBitmap;

        private int _bitmapIndex = 0;
        private bool _isEndloop;

        public Animator(Bitmap bitmap, bool isEndloop)
        {
            StaticBitmap = bitmap;
            _isEndloop = isEndloop;
        }

        // returns true when the animation has ended
        public bool DrawNextSprite(Point position, Graphics g, Size size)
        {
            if (_bitmapIndex >= SpriteList.Count)
            {
                if (!_isEndloop)
                {
                    return true;
                }
                _bitmapIndex = 0;
            }

            Rectangle rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
            g.DrawImage(SpriteList[_bitmapIndex], rectangle);

            _bitmapIndex++;
            
            return false;
        }

        public void DrawStaticBitmap(Point position, Graphics g, Size size)
        {
            Rectangle rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
            g.DrawImage(StaticBitmap, rectangle);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
