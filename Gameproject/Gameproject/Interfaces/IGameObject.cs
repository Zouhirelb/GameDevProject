using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Interfaces
{
    internal interface IGameObject
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);

    }
}
