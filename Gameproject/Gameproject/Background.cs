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
        private int _screenWidth;
        private int _screenHeight;

        public Background(Texture2D texture, int screenWidth, int screenHeight)
        {
            _texture = texture;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int textureWidth = _texture.Width;
            int textureHeight = _texture.Height;

            // Herhaal de achtergrond over het scherm
            for (int x = 0; x < _screenWidth; x += textureWidth)
            {
                for (int y = 0; y < _screenHeight; y += textureHeight)
                {
                    spriteBatch.Draw(_texture, new Vector2(x, y), Color.White);
                }
            }
        }
    }
}
