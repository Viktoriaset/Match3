using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCommand : IBonusCommand
    {
        private GameField _gameField;

       public HorizontalDestroyerBonusCommand()
        {
            bitmap = Resource1.HorizontalDestroyer;
        }

        public void SetGameField(GameField gameField)
        {
            _gameField = gameField;
        }

        public override int UseBonus(Point point)
        {
            int countPoints = 0;
            for (int i = 0; i < _gameField.columnsCount; i++)
            {
                if (i != point.X)
                {
                    countPoints += _gameField.GetElement(i, point.Y).Destroy(new Point(i, point.Y));
                }
            }

            return countPoints;
        }
    }
}
