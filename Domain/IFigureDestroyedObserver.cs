namespace ThreeInRow.Domain
{
    public interface IFigureDestroyedObserver
    {
        void FigureDestroyed(int points);
    }
}