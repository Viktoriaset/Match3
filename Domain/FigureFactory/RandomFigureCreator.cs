namespace ThreeInRow.Back
{
    internal class RandomFigureCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            return (Figure)figurePrototypeList.GetRandomElement().Clone();
        }
    }
}
