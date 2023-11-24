using ThreeInRow.Back;

namespace ThreeInRow.Domain.FigureFactory
{
    internal class EmptyFigureCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            return new Figure(100, FigureType.Empty, Resource1.Empty);
        }
    }
}
