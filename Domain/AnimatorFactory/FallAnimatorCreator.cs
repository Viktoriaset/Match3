using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    public class FallAnimatorCreator
    {
        public Animator CreateAnimatorByFigure(Figure figure)
        {
            Animator animator = new Animator(figure.Bitmap, true);
            switch (figure.Type)
            {
                case FigureType.Square:
                    animator.spriteList.Add(Resource1.Rectangle_30_left);
                    animator.spriteList.Add(Resource1.Rectangle_15_left);
                    animator.spriteList.Add(Resource1.Rectangle_1);
                    animator.spriteList.Add(Resource1.Rectangle_15_right);
                    animator.spriteList.Add(Resource1.Rectangle_30_right);
                    break;
                case FigureType.Circle:
                    animator.spriteList.Add(Resource1.Circle_30_left);
                    animator.spriteList.Add(Resource1.Circle_15_left);
                    animator.spriteList.Add(Resource1.Circle);
                    animator.spriteList.Add(Resource1.Circle_15_right);
                    animator.spriteList.Add(Resource1.Circle_30_right);
                    break;
                case FigureType.Pentagon:
                    animator.spriteList.Add(Resource1.Star_30_left);
                    animator.spriteList.Add(Resource1.Star_15_left);
                    animator.spriteList.Add(Resource1.Star_1);
                    animator.spriteList.Add(Resource1.Star_15_right);
                    animator.spriteList.Add(Resource1.Star_30_right);
                    break;
                case FigureType.Hexagon:
                    animator.spriteList.Add(Resource1.Hexogen_30_left);
                    animator.spriteList.Add(Resource1.Hexogen_15_left);
                    animator.spriteList.Add(Resource1.Hexogen);
                    animator.spriteList.Add(Resource1.Hexogen_15_right);
                    animator.spriteList.Add(Resource1.Hexogen_30_right);
                    break;
                case FigureType.Triangle:
                    animator.spriteList.Add(Resource1.Tryangle_30_left);
                    animator.spriteList.Add(Resource1.Tryangle_15_left);
                    animator.spriteList.Add(Resource1.Tryangle);
                    animator.spriteList.Add(Resource1.Tryangle_15_right);
                    animator.spriteList.Add(Resource1.Tryangle_30_right);
                    break;
            }

            return animator;
        }
    }
}
