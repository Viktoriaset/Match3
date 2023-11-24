namespace ThreeInRow.Domain
{
    public interface IFigureDestroyedObservable
    {
        void Subscribe(IFigureDestroyedObserver observer);
        void Unsubscribe(IFigureDestroyedObserver observer);
        void FigureDestroyed(int points);
    }
}
