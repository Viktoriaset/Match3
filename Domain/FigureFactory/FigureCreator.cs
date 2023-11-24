using System.Collections.Generic;
using ThreeInRow.Domain;
using ThreeInRow.Domain.AnimatorFactory;

namespace ThreeInRow.Back
{
    internal abstract class FigureCreator
    {
        protected List<Figure> figurePrototypeList = new List<Figure>();
        private FallAnimatorCreator _animatorCreator = new FallAnimatorCreator();
        private SelectFigureAnimatorCreator _selectAnimatorCreator = new SelectFigureAnimatorCreator();
        private DestroyFigureCreator _destroyFigureCreator = new DestroyFigureCreator();

        public FigureCreator()
        {
            figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Pentagon));
            figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Hexagon));
            figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Triangle));
            figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Square));
            figurePrototypeList.Add(CreateFigurePrototypeByType(FigureType.Circle));
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

                    square.FallAnimator = _animatorCreator.CreateAnimatorByFigure(square);
                    square.SelectFigureAnimator = _selectAnimatorCreator.CreateAnimatorByFigure(square);
                    square.DestroyFigureAnimator = _destroyFigureCreator.CreateAnimatorByFigure(square);

                    return square;

                case FigureType.Triangle:
                    Figure triangle = new Figure(
                        100,
                        FigureType.Triangle,
                        Resource1.Tryangle
                    );

                    triangle.FallAnimator = _animatorCreator.CreateAnimatorByFigure(triangle);
                    triangle.SelectFigureAnimator = _selectAnimatorCreator.CreateAnimatorByFigure(triangle);
                    triangle.DestroyFigureAnimator = _destroyFigureCreator.CreateAnimatorByFigure(triangle);

                    return triangle;

                case FigureType.Circle:
                    Figure circle = new Figure(
                        100,
                        FigureType.Circle,
                        Resource1.Circle
                    );

                    circle.FallAnimator = _animatorCreator.CreateAnimatorByFigure(circle);
                    circle.SelectFigureAnimator = _selectAnimatorCreator.CreateAnimatorByFigure(circle);
                    circle.DestroyFigureAnimator = _destroyFigureCreator.CreateAnimatorByFigure(circle);

                    return circle;

                case FigureType.Pentagon:
                    Figure pentagon = new Figure(
                        100,
                        FigureType.Pentagon,
                        Resource1.Star_1
                    );

                    pentagon.FallAnimator = _animatorCreator.CreateAnimatorByFigure(pentagon);
                    pentagon.SelectFigureAnimator = _selectAnimatorCreator.CreateAnimatorByFigure(pentagon);
                    pentagon.DestroyFigureAnimator = _destroyFigureCreator.CreateAnimatorByFigure(pentagon);

                    return pentagon;

                case FigureType.Hexagon:
                    Figure hexagon = new Figure(
                        100,
                        FigureType.Hexagon,
                        Resource1.Hexogen
                    );

                    hexagon.FallAnimator = _animatorCreator.CreateAnimatorByFigure(hexagon);
                    hexagon.SelectFigureAnimator = _selectAnimatorCreator.CreateAnimatorByFigure(hexagon);
                    hexagon.DestroyFigureAnimator = _destroyFigureCreator.CreateAnimatorByFigure(hexagon);

                    return hexagon;
                default:
                    return null;
            }
        }

        public abstract Figure CreateFigure();
    }
}
