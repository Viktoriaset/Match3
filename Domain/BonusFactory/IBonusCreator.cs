using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    public interface IBonusCreator
    {
        BaseBonus CreateBonus();
    }
}
