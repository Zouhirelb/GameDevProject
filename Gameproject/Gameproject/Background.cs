using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject
{
    public class Background
    {
        private Texture2D _texture;
       

        public Background(Texture2D texture)
        {
            _texture = texture;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
                
                    spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
                
            
        }
    }
}
