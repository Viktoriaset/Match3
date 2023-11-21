using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    internal class WinCondition : IFigureDestroyedObserver
    {
        private int _totalPoints = 0;
        public void FigureDestroyed(int points)
        {
            _totalPoints += points;
        }
    }
}
