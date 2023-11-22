using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Back
{
    internal abstract class FigureCreator
    {
        protected List<Figure> _figurePrototypeList = new List<Figure>();

        public FigureCreator()
        {
            _figurePrototypeList.Add(new Figure(100, FigureType.Triangle, Resource1.Polygon_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Circle, Resource1.Ellipse_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Square, Resource1.Rectangle_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Pentagon, Resource1.Star_1));
            _figurePrototypeList.Add(new Figure(100, FigureType.Hexagon, Resource1.Hexogen));
        }

        public abstract Figure CreateFigure();
    }
}
