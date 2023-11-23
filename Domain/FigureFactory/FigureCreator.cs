using System.Collections.Generic;
using ThreeInRow.Domain;
using ThreeInRow.Domain.AnimatorFactory;

namespace ThreeInRow.Back
{
    internal abstract class FigureCreator
    {
        protected List<Figure> _figurePrototypeList = new List<Figure>();
        private FallAnimatorCreator animatorCreator = new FallAnimatorCreator();
        private SelectFigureAnimatorCreator selectAnimatorCreator = new SelectFigureAnimatorCreator();

        public FigureCreator()
        {
            _figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Pentagon));
            _figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Hexagon));
            _figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Triangle));
            _figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Square));
            _figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Circle));
        }

        private Figure CreateFigurePrototypeByType(FigureType type)
        {

            switch (type)
            {
                case FigureType.Square:
                    Figure square = new Figure(
                        100,
                        FigureType.Square,
                        Resource1.Rectangle_1
                    );
                    square.FallAnimator = animatorCreator.CreateAnimatorByFigure(square);
                    square.SelectFigureAnimator = selectAnimatorCreator.CreateAnimatorByFigure(square);
                    return square;

                case FigureType.Triangle:
                    Figure triangle = new Figure(
                        100,
                        FigureType.Triangle,
                        Resource1.Tryangle
                    );
                    triangle.FallAnimator = animatorCreator.CreateAnimatorByFigure(triangle);
                    triangle.SelectFigureAnimator = selectAnimatorCreator.CreateAnimatorByFigure(triangle);
                    return triangle;

                case FigureType.Circle:
                    Figure circle = new Figure(
                        100,
                        FigureType.Circle,
                        Resource1.Circle
                    );
                    circle.FallAnimator = animatorCreator.CreateAnimatorByFigure(circle);
                    circle.SelectFigureAnimator = selectAnimatorCreator.CreateAnimatorByFigure(circle);
                    return circle;

                case FigureType.Pentagon:
                    Figure pentagon = new Figure(
                        100,
                        FigureType.Pentagon,
                        Resource1.Star_1
                    );
                    pentagon.FallAnimator = animatorCreator.CreateAnimatorByFigure(pentagon);
                    pentagon.SelectFigureAnimator = selectAnimatorCreator.CreateAnimatorByFigure(pentagon);
                    return pentagon;

                case FigureType.Hexagon:
                    Figure hexagon = new Figure(
                        100,
                        FigureType.Hexagon,
                        Resource1.Hexogen
                    );
                    hexagon.FallAnimator = animatorCreator.CreateAnimatorByFigure(hexagon);
                    hexagon.SelectFigureAnimator = selectAnimatorCreator.CreateAnimatorByFigure(hexagon);
                    return hexagon;
                default:
                    return null;
            }
        }

        public abstract Figure CreateFigure();
    }
}
