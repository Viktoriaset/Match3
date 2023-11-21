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
    internal class BombBonusCommand : IBonusCommand
    {
        private Timer timer;
        public GameField gameField { private get; set; }

        public BombBonusCommand() 
        {
            bitmap = Resource1.Bomp;
        }

        public override int UseBonus(Point point)
        {
            timer = new Timer();
            timer.Interval = 250;
            timer.Tick += (sender, e) => { 
                timer.Stop();
                Explousion(point); 
            };

            timer.Start();

            return 200; 
        }

        private void Explousion(Point point)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Point element = new Point(point.X + i, point.Y + j);
                        gameField.GetElement(element.X, element.Y).Destroy(element);
                    }
                }
            }
        }
    }
}
