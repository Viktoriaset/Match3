using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    public interface IGameObject
    {
        void Initialization();
        void Update(object sender, PaintEventArgs e);
    }
}
