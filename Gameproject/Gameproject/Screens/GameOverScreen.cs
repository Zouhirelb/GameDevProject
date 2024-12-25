using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Gameproject.Screens
{
    public class GameOverScreen
    {
        private Texture2D background;
        private Texture2D message;
        private Song music;
        private GraphicsDevice graphicsDevice;

        public GameOverScreen(Texture2D background, Texture2D message, Song music, GraphicsDevice graphicsDevice)
        {
            this.background = background;
            this.message = message;
            this.music = music;
            this.graphicsDevice = graphicsDevice;
        }
    }
}
