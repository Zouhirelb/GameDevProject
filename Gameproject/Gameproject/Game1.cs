using System;
using Comora;
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

        private Texture2D herorechtslooptexture;
        private Texture2D herolinkslooptexture;
        private Texture2D herostiltexture;

        private Texture2D enemyrechtstexture;

        private Texture2D backgroundTexture;
        private Background _background;
        private Camera camera;
        Hero hero;
        private Enemy enemy;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("gras");
            herorechtslooptexture = Content.Load<Texture2D>("character lopen");
            herolinkslooptexture = Content.Load<Texture2D>("linkslopen");
            herostiltexture = Content.Load<Texture2D>("stil");
            enemyrechtstexture = Content.Load<Texture2D>("lava-enemy-rechtslopen");


            
            _background = new Background(backgroundTexture);

            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolinkslooptexture,herorechtslooptexture,herostiltexture, new KeyBoardReader());
            enemy = new Enemy(enemyrechtstexture, Vector2.One);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            enemy.Update(gameTime,hero.Positie);

            this.camera.Position = hero.Positie;
            this.camera.Update(gameTime);

         



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin(this.camera);
            

            _background.Draw(_spriteBatch);

            hero.Draw(_spriteBatch);
            enemy.Draw(_spriteBatch);

            _spriteBatch.End();

           
         
          

            base.Draw(gameTime); 
        }
    }
}
