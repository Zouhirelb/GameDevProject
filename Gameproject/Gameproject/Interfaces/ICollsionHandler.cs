using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject.Interfaces
{
    internal interface ICollsionHandler
    {
        void HandleCollision(IGameObject objA, IGameObject objB);
    }
}
