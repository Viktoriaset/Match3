using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    public class Animator
    {
        int bitmapIndex = 0;
        public List<Bitmap> spriteList = new List<Bitmap>();
        public Bitmap staticBitmap;
        private IHolderBitmap _holderBitmap;
        private bool _isPlaing = false;
        private Timer _timer;

        public Animator(Bitmap bitmap, IHolderBitmap holderBitmap)
        {
            staticBitmap = bitmap;
            _holderBitmap = holderBitmap;
        }

        public void Start(Timer timer)
        {
            if (!_isPlaing)
            {
                _timer = timer;
                _timer.Tick += Play;
                _isPlaing = true;
                return;
            }
        }

        public void Play(object sender, EventArgs e)
        {
            _holderBitmap.SetBitmap(spriteList[bitmapIndex]);

            bitmapIndex++;
            if (bitmapIndex >= spriteList.Count)
            {
                bitmapIndex = 0;
            }
        }

        public void Stop(IHolderBitmap bitmap)
        {
            if (_isPlaing)
            {
                bitmapIndex = 0;
                bitmap.SetBitmap(staticBitmap);
                _isPlaing = false;
                _timer.Tick -= Play;
            }
            
        }
    }
}
