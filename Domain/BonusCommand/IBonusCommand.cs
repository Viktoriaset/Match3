using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    public abstract class IBonusCommand
    {
        public Bitmap bitmap;
        public abstract int UseBonus(Point point);
    }
}
