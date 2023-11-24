using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.BonusCommand
{
    internal class BombBonusCommand : BaseBonus, ICloneable
    {
        private Timer _timer;

        public BombBonusCommand(Bitmap bitmap, int points) : base(bitmap, points)
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Draw(Graphics g)
        {
            return true;
        }

        public override int UseBonus(Point point, GameField gameField, Timer timer)
        {
            _timer = new Timer();
            _timer.Interval = 250;
            _timer.Tick += (sender, e) => { 
                _timer.Stop();
                Explousion(point, gameField); 
            };

            _timer.Start();

            return points; 
        }

        private void Explousion(Point point, GameField gameField)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Point element = new Point(point.X + i, point.Y + j);
                        gameField.DestroyElement(element);
                    }
                }
            }
        }
    }
}
