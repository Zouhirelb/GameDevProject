using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static Gameproject.Managers.GameStateManager;

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
        public void Update()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                GameStateManager.CurrentState = GameState.StartScreen;
                MediaPlayer.Stop(); 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                background,
                new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
                Color.White
            );

            Vector2 messagePosition = new Vector2(
                (graphicsDevice.Viewport.Width - message.Width) / 2,
                (graphicsDevice.Viewport.Height - message.Height) / 2
            );
            spriteBatch.Draw(message, messagePosition, Color.White);

            spriteBatch.End();
        }
    }
}
}
