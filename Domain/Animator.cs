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

        public Animator(Bitmap bitmap)
        {
            staticBitmap = bitmap;
        }

        public void DrawNextSprite(Point position, Graphics g, Size size)
        {
            Rectangle rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
            g.DrawImage(spriteList[bitmapIndex], rectangle);

            bitmapIndex++;
            if (bitmapIndex >= spriteList.Count)
            {
                bitmapIndex = 0;
            }
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
