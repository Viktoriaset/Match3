using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCreator : IBonusCreator
    {
        private HorizontalDestroyerBonusCommand _horizontalDestroyerPrototype;

        public HorizontalDestroyerBonusCreator()
        {
            _horizontalDestroyerPrototype = new HorizontalDestroyerBonusCommand(
                Resource1.HorizontalDestroyer,
                500,
                Resource1.destroyer_left,
                Resource1.destroyer_right
            );
        }

        public BaseBonus CreateBonus()
        {
            return (HorizontalDestroyerBonusCommand)_horizontalDestroyerPrototype.Clone();
        }
    }
}
