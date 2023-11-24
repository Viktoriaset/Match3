using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ThreeInRow.Domain
{
    internal class WinConditionByTime : WinCondition
    {
        private TimeSpan _time;
        private Label _timerLabel;
        public WinConditionByTime(Label pointsLable, int millesecondsTime, Label timerLabel)
            : base(pointsLable)
        {
            _time = TimeSpan.FromMilliseconds(millesecondsTime);
            _timerLabel = timerLabel;
        }

        public bool IsGameFinished(int timeInterval)
        {
            if (_time.TotalMilliseconds > 0)
            {
                _time -= TimeSpan.FromMilliseconds(timeInterval);
                _timerLabel.Text = _time.ToString(@"mm\:ss");

                return false;
            }

            return true;
        }
    }
}
