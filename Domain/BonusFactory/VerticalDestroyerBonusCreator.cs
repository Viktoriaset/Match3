using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCreator : BonusCreator
    {
        private VerticalDestroyerBonusCommand _verticalDestroyer;

        public VerticalDestroyerBonusCreator()
        {
            _verticalDestroyer = new VerticalDestroyerBonusCommand(Resource1.VerticalDestroyer);
        }
        public override IBonusCommand CreateBonus()
        {
            return (VerticalDestroyerBonusCommand)_verticalDestroyer.Clone();
        }
    }
}
