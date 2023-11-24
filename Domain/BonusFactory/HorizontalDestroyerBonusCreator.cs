using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class HorizontalDestroyerBonusCreator : BonusCreator
    {
        private HorizontalDestroyerBonusCommand _horizontalDestroyer;

        public HorizontalDestroyerBonusCreator()
        {
            _horizontalDestroyer = new HorizontalDestroyerBonusCommand(
                Resource1.HorizontalDestroyer,
                500,
                Resource1.destroyer_left,
                Resource1.destroyer_right
            );
        }

        public BaseBonus CreateBonus()
        {
            return (HorizontalDestroyerBonusCommand)_horizontalDestroyer.Clone();
        }
    }
}
