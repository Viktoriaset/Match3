using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    internal class WinCondition : IFigureDestroyedObserver
    {
        public int TotalPoints { get; private set; } = 0;
        private Label _pointsLable;

        public WinCondition(Label pointsLable)
        {
            _pointsLable = pointsLable;
        }

        public void FigureDestroyed(int points)
        {
            TotalPoints += points;
            _pointsLable.Text = "Очки: " + TotalPoints.ToString();
        }
    }
}
