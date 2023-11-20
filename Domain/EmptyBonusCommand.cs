using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    internal class EmptyBonusCommand : IBonusCommand
    {
        public EmptyBonusCommand()
        {
            bitmap = Resource1.Empty;
        }
        public override int UseBonus(Point point)
        {
            return 0 ;
        }
    }
}
