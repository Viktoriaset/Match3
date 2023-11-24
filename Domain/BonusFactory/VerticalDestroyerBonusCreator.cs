using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCreator : BonusCreator
    {
        private VerticalDestroyerBonusCommand _verticalDestroyer;

        public VerticalDestroyerBonusCreator()
        {
            _verticalDestroyer = new VerticalDestroyerBonusCommand(
                Resource1.VerticalDestroyer,
                500,
                Resource1.destroyer_up,
                Resource1.destroyer_douwn
            );
        }
        public BaseBonus CreateBonus()
        {
            return (VerticalDestroyerBonusCommand)_verticalDestroyer.Clone();
        }
    }
}
