using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain.Extensions
{
    internal static class PointCopyExtension
    {
        public static Point Copy(this Point source)
        {
            return new Point(source.X, source.Y);
        }
    }
}
