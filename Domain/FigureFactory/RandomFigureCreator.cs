using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Back
{
    internal class RandomFigureCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            return (Figure)figurePrototypeList.GetRandomElement().Clone();
        }
    }
}
