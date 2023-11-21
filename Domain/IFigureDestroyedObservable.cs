using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    public interface IFigureDestroyedObservable
    {
        void Subscribe(IFigureDestroyedObserver observer);
        void Unsubscribe(IFigureDestroyedObserver observer);
        void FigureDestroyed(int points);
    }
}
