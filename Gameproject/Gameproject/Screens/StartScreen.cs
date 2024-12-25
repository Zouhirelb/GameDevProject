using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static Gameproject.Managers.GameStateManager;

namespace Gameproject.Screens
{
    public class StartScreen
    {
        private Texture2D background;
        private Texture2D button;
        private Texture2D message;
        private Rectangle buttonRectangle;
        private Song music;
        private GraphicsDevice graphicsDevice;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public StartScreen(Texture2D background, Texture2D button, Texture2D message, Song music, GraphicsDevice graphicsDevice)
        {
            this.background = background;
            this.button = button;
            this.message = message;
            this.music = music;
            this.graphicsDevice = graphicsDevice; 

            this.buttonRectangle = new Rectangle(540, 360, button.Width, button.Height);
        }
        public void Update()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }

            currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released &&
                buttonRectangle.Contains(currentMouseState.Position))
            {
                GameStateManager.CurrentState = GameState.Playing;
                MediaPlayer.Stop();
            }

            
            previousMouseState = currentMouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                background,
                new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), 
                Color.White
            );

            spriteBatch.Draw(button, buttonRectangle, Color.White);
            spriteBatch.Draw(message, new Vector2(
                (graphicsDevice.Viewport.Width - message.Width) / 2, 
                graphicsDevice.Viewport.Height / 4), 
                Color.White
            );

            spriteBatch.End();
        }

    }
}
