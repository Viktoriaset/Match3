using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCommand : IBonusCommand
    {
        private GameField _gameField;

        public VerticalDestroyerBonusCommand()
        {
            bitmap = Resource1.VerticalDestroyer;
        }

        public void SetGameField(GameField gameField)
        {
            _gameField = gameField;
        }
        public override int UseBonus(Point point)
        {
            int countPoints = 0;
            for (int i = 0; i < _gameField.rowsCount; i++)
            {
                if (i != point.Y)
                {
                   countPoints += _gameField.GetElement(point.X, i).Destroy(new Point(i, point.Y));
                }
            }

            return countPoints;
        }
    }
}
