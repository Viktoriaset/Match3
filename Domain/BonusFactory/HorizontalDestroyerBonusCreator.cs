using ThreeInRow.Domain.Bonus;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCreator : IBonusCreator
    {
        private HorizontalDestroyersBonus _horizontalDestroyerPrototype;

        public HorizontalDestroyerBonusCreator()
        {
            _horizontalDestroyerPrototype = new HorizontalDestroyersBonus(Resource1.HorizontalDestroyer, 500);
        }

        public BaseBonus CreateBonus()
        {
            return (HorizontalDestroyersBonus)_horizontalDestroyerPrototype.Clone();
        }
    }
}
