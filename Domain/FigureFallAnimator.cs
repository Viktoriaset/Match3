using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    public class FigureFallAnimator : Animator
    {
        public FigureFallAnimator(int maxAnimationTickOnAction) : base(maxAnimationTickOnAction)
        {

        }
        public override bool Play()
        {
            return true;
        }
    }
}
