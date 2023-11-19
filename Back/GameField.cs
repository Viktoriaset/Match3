using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Back
{
    public class GameField
    {
        private int[,] _field;

        public GameField(int rowsCount, int columnsCount)
        {
            _field = new int[rowsCount, columnsCount];
        }

        public void FillRandomElements()
        {

        }


    }
}
