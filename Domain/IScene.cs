using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInRow.Domain
{
    public interface IScene
    {
        void AddGameObject(IGameObject go);
        void DestroyGameObject(IGameObject go);
    }
}
