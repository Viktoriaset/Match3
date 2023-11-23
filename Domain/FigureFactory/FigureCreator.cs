using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Domain;

namespace ThreeInRow.Back
{
    internal abstract class FigureCreator
    {
        protected List<Figure> _figurePrototypeList = new List<Figure>();
        private AnimatorCreator animatorCreator = new AnimatorCreator();

        public FigureCreator()
        {
            _figurePrototypeList.Add(new Figure(100, FigureType.Triangle, Resource1.Tryangle));
            _figurePrototypeList.Add(new Figure(100, FigureType.Circle, Resource1.Circle));
            _figurePrototypeList.Add(new Figure(100, FigureType.Square, Resource1.Rectangle_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Pentagon, Resource1.Star_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Hexagon, Resource1.Hexogen));
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
                    return square;

                case FigureType.Triangle:
                    Figure triangle = new Figure(
                        100, 
                        FigureType.Triangle, 
                        Resource1.Rectangle_1
                    );
                    triangle.FallAnimator = animatorCreator.CreateAnimatorByFigure(triangle);
                    return triangle;

                case FigureType.Circle:
                    Figure circle = new Figure(
                        100, 
                        FigureType.Circle, 
                        Resource1.Rectangle_1
                    );
                    circle.FallAnimator = animatorCreator.CreateAnimatorByFigure(circle);
                    return circle;

                case FigureType.Pentagon:
                    Figure pentagon = new Figure(
                        100, 
                        FigureType.Pentagon, 
                        Resource1.Rectangle_1
                    );
                    pentagon.FallAnimator = animatorCreator.CreateAnimatorByFigure(pentagon);
                    return pentagon;

                case FigureType.Hexagon:
                    Figure hexagon =  new Figure(
                        100, 
                        FigureType.Hexagon, 
                        Resource1.Rectangle_1
                    );
                    hexagon.FallAnimator = animatorCreator.CreateAnimatorByFigure(hexagon);
                    return hexagon;
                default:
                    return null;
            }
            
        }

        public abstract Figure CreateFigure();
    }
}
