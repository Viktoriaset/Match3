using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInRow.Back;

namespace ThreeInRow.Domain.FigureFactory
{
    internal class EmptyFigureCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            return new Figure(100, FigureType.Empty, Resource1.Empty);
        }
    }
}
