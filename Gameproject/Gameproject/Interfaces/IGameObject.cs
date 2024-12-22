using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Interfaces
{
    internal interface IGameObject
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

         Vector2 Positie { get; set; } 
        int Breedte { get; }    
        int Hoogte { get; }     

        Rectangle BoundingBox { get; }

    }
}
