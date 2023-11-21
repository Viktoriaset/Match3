using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    public abstract class BonusCreator
    {
        public abstract IBonusCommand CreateBonus();
    }
}
