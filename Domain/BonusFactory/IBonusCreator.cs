using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Domain.BonusCommand;

namespace ThreeInRow.Domain
{
    public interface IBonusCreator
    {
        BaseBonus CreateBonus();
    }
}
