using System;
using Gameproject.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D herolooptexture;
        private Texture2D herostiltexture;
        private Texture2D backgroundTexture;
        private Background _background;
        Hero hero;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("gras");
            herolooptexture = Content.Load<Texture2D>("character lopen");
            herostiltexture = Content.Load<Texture2D>("stil");

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            _background = new Background(backgroundTexture, screenWidth, screenHeight);

            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolooptexture,herostiltexture, new KeyBoardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _background.Draw(_spriteBatch);

            hero.Draw(_spriteBatch);


            _spriteBatch.End();

           
         
          

            base.Draw(gameTime); 
        }
    }
}
