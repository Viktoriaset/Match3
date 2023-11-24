using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    internal class VerticalDestroyerBonusCreator : IBonusCreator
    {
        private VerticalDestroyerBonusCommand _verticalDestroyerPrototype;

        public VerticalDestroyerBonusCreator()
        {
            _verticalDestroyerPrototype = new VerticalDestroyerBonusCommand(
                Resource1.VerticalDestroyer,
                500,
                Resource1.destroyer_up,
                Resource1.destroyer_douwn
            );
        }
        public BaseBonus CreateBonus()
        {
            return (VerticalDestroyerBonusCommand)_verticalDestroyerPrototype.Clone();
        }
    }
}
