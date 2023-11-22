using System.Collections.Generic;
using System.Drawing;

namespace ThreeInRow.Domain
{
    public abstract class Animator
    {
        public List<Bitmap> spriteList = new List<Bitmap>();
        public int maxAnimationTickOnAction;
        public int animationTickCounter = 0;

        public Animator(int maxAnimationTickOnAction)
        {
            this.maxAnimationTickOnAction = maxAnimationTickOnAction;
        }
        public abstract bool Play();
    }
}
