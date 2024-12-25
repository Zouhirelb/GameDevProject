using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject.Screens
{
    public class StartScreen
    {
        private Texture2D background;
        private Texture2D button;
        private Texture2D message;
        private Rectangle buttonRectangle;
        private SoundEffect music;
        private SoundEffectInstance musicInstance;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public StartScreen(Texture2D background, Texture2D button, Texture2D message, SoundEffect music)
        {
            this.background = background;
            this.button = button;
            this.message = message;
            this.music = music;

            this.musicInstance = music.CreateInstance();
            this.musicInstance.IsLooped = true;

            this.buttonRectangle = new Rectangle(540, 360, button.Width, button.Height);
        }

    }
}
