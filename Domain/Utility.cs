using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeInRow.Domain
{
    public static class Utility
    {
        public static IScene scene;
        public static void CreateGameObject(IGameObject gameObject)
        {
            gameObject.Initialization();
            scene.AddGameObject(gameObject);
        }

        public static void DestroyGameObject(IGameObject gameObject)
        {
            scene.DestroyGameObject(gameObject);
        }
    }
}
