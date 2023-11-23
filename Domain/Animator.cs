using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    public class Animator: ICloneable
    {
        int bitmapIndex = 0;
        public List<Bitmap> spriteList = new List<Bitmap>();
        public Bitmap staticBitmap;
        private bool _isEndloop;

        public Animator(Bitmap bitmap, bool isEndloop)
        {
            staticBitmap = bitmap;
            _isEndloop = isEndloop;
        }

        // returns true when the animation has ended
        public bool DrawNextSprite(Point position, Graphics g, Size size)
        {
            if (bitmapIndex >= spriteList.Count)
            {
                if (!_isEndloop)
                {
                    return true;
                }
                bitmapIndex = 0;
            }

            Rectangle rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
            g.DrawImage(spriteList[bitmapIndex], rectangle);

            bitmapIndex++;
            

            return false;
        }

        public void DrawStaticBitmap(Point position, Graphics g, Size size)
        {
            Rectangle rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
            g.DrawImage(staticBitmap, rectangle);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
