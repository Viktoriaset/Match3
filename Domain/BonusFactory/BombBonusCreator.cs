using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain.BonusFactory
{
    internal class BombBonusCreator : IBonusCreator
    {
        private BombBonusCommand _bombBonusPrototype;

        public BombBonusCreator()
        {
            _bombBonusPrototype = new BombBonusCommand(Resource1.Bomp, 800);
        }
        public BaseBonus CreateBonus()
        {
            return (BombBonusCommand)_bombBonusPrototype.Clone();
        }
    }
}
