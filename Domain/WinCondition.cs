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
