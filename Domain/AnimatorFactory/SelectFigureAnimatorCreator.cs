using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.AnimatorFactory
{
    internal class SelectFigureAnimatorCreator
    {
        public Animator CreateAnimatorByFigure(Figure figure)
        {
            Animator animator = new Animator(figure.Bitmap, true);

            switch (figure.Type)
            {
                case FigureType.Square:
                    animator.SpriteList.Add(Resource1.SelectedSquare);
                    break;
                case FigureType.Circle:
                    animator.SpriteList.Add(Resource1.SelectedCircle);
                    break;
                case FigureType.Pentagon:
                    animator.SpriteList.Add(Resource1.SelectedPentagon);
                    break;
                case FigureType.Hexagon:
                    animator.SpriteList.Add(Resource1.SelectedHexogen);
                    break;
                case FigureType.Triangle:
                    animator.SpriteList.Add(Resource1.SelectedTryangle);
                    break;
            }

            return animator;
        }
    }
}
