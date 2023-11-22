using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCreator : BonusCreator
    {
        private HorizontalDestroyerBonusCommand _horizontalDestroyer;

        public HorizontalDestroyerBonusCreator()
        {
            _horizontalDestroyer = new HorizontalDestroyerBonusCommand(Resource1.HorizontalDestroyer);
        }

        public BaseBonus CreateBonus()
        {
            return (HorizontalDestroyerBonusCommand)_horizontalDestroyer.Clone();
        }
    }
}
