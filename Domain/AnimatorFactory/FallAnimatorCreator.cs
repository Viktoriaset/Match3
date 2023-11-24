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
                    animator.SpriteList.Add(Resource1.Rectangle_30_left);
                    animator.SpriteList.Add(Resource1.Rectangle_15_left);
                    animator.SpriteList.Add(Resource1.Rectangle_1);
                    animator.SpriteList.Add(Resource1.Rectangle_15_right);
                    animator.SpriteList.Add(Resource1.Rectangle_30_right);
                    break;
                case FigureType.Circle:
                    animator.SpriteList.Add(Resource1.Circle_30_left);
                    animator.SpriteList.Add(Resource1.Circle_15_left);
                    animator.SpriteList.Add(Resource1.Circle);
                    animator.SpriteList.Add(Resource1.Circle_15_right);
                    animator.SpriteList.Add(Resource1.Circle_30_right);
                    break;
                case FigureType.Pentagon:
                    animator.SpriteList.Add(Resource1.Star_30_left);
                    animator.SpriteList.Add(Resource1.Star_15_left);
                    animator.SpriteList.Add(Resource1.Star_1);
                    animator.SpriteList.Add(Resource1.Star_15_right);
                    animator.SpriteList.Add(Resource1.Star_30_right);
                    break;
                case FigureType.Hexagon:
                    animator.SpriteList.Add(Resource1.Hexogen_30_left);
                    animator.SpriteList.Add(Resource1.Hexogen_15_left);
                    animator.SpriteList.Add(Resource1.Hexogen);
                    animator.SpriteList.Add(Resource1.Hexogen_15_right);
                    animator.SpriteList.Add(Resource1.Hexogen_30_right);
                    break;
                case FigureType.Triangle:
                    animator.SpriteList.Add(Resource1.Tryangle_30_left);
                    animator.SpriteList.Add(Resource1.Tryangle_15_left);
                    animator.SpriteList.Add(Resource1.Tryangle);
                    animator.SpriteList.Add(Resource1.Tryangle_15_right);
                    animator.SpriteList.Add(Resource1.Tryangle_30_right);
                    break;
            }

            return animator;
        }
    }
}
