using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.AnimatorFactory
{
    internal class DestroyFigureCreator
    {
        public Animator CreateAnimatorByFigure(Figure figure)
        {
            Animator animator = new Animator(figure.Bitmap, false);
            switch (figure.Type)
            {
                case FigureType.Square:
                    animator.spriteList.Add(Resource1.Rectangle_destroy_1);
                    animator.spriteList.Add(Resource1.Rectangle_destroy_2);
                    animator.spriteList.Add(Resource1.Rectangle_destroy_3);
                    animator.spriteList.Add(Resource1.Rectangle_destroy_4);
                    break;
                case FigureType.Circle:
                    animator.spriteList.Add(Resource1.Circle_destroy_1);
                    animator.spriteList.Add(Resource1.Circle_destroy_2);
                    animator.spriteList.Add(Resource1.Circle_destroy_3);
                    animator.spriteList.Add(Resource1.Circle_destroy_4);
                    break;
                case FigureType.Pentagon:
                    animator.spriteList.Add(Resource1.Pentagon_destroy_1);
                    animator.spriteList.Add(Resource1.Pentagon_destroy_2);
                    animator.spriteList.Add(Resource1.Pentagon_destroy_3);
                    animator.spriteList.Add(Resource1.Pentagon_destroy_4);
                    break;
                case FigureType.Hexagon:
                    animator.spriteList.Add(Resource1.Hexogen_destroy_1);
                    animator.spriteList.Add(Resource1.Hexogen_destroy_2);
                    animator.spriteList.Add(Resource1.Hexogen_destroy_3);
                    animator.spriteList.Add(Resource1.Hexogen_destroy_4);
                    break;
                case FigureType.Triangle:
                    animator.spriteList.Add(Resource1.Tryangle_destroy_1);
                    animator.spriteList.Add(Resource1.Tryangle_destroy_2);
                    animator.spriteList.Add(Resource1.Tryangle_destroy_3);
                    animator.spriteList.Add(Resource1.Tryangle_destroy_4);
                    break;
            }

            return animator;
        }
    }
}
