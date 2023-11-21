using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCreator : BonusCreator
    {
        private HorizontalDestroyerBonusCommand _horizontalDestroyer;

        public HorizontalDestroyerBonusCreator()
        {
            _horizontalDestroyer = new HorizontalDestroyerBonusCommand(Resource1.HorizontalDestroyer);
        }

        public override IBonusCommand CreateBonus()
        {
                return (HorizontalDestroyerBonusCommand)_horizontalDestroyer.Clone();
        }
    }
}
