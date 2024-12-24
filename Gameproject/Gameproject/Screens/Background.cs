using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject
{
    public class Background
    {
        private Texture2D _texture;
        private Rectangle _mapBounds;

        public Background(Texture2D texture, Rectangle mapBounds)
        {
            _texture = texture;
            _mapBounds = mapBounds;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition, Viewport viewport)
        {
            int textureWidth = _texture.Width;
            int textureHeight = _texture.Height;

            int startX = (int)(cameraPosition.X - viewport.Width * 2) / textureWidth * textureWidth;
            int startY = (int)(cameraPosition.Y - viewport.Height * 2) / textureHeight * textureHeight;

            int endX = (int)(cameraPosition.X + viewport.Width / 2);
            int endY = (int)(cameraPosition.Y + viewport.Height / 2);

            for (int x = startX; x <= endX; x += textureWidth)
            {
                for (int y = startY; y <= endY; y += textureHeight)
                {
                    spriteBatch.Draw(_texture, new Vector2(x, y), Color.White);
                }
            }
        }
    }
}
