﻿using ThreeInRow.Domain.Bonus;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCreator : IBonusCreator
    {
        private VerticalDestroyersBonus _verticalDestroyerPrototype;

        public VerticalDestroyerBonusCreator()
        {
            _verticalDestroyerPrototype = new VerticalDestroyersBonus(Resource1.VerticalDestroyer, 500);
        }
        public BaseBonus CreateBonus()
        {
            return (VerticalDestroyersBonus)_verticalDestroyerPrototype.Clone();
        }
    }
}
