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
        private Texture2D enemylinkstexture;

        private Texture2D backgroundTexture;
        private Background _background;

        private Camera camera;
        Hero hero;
        private Enemy enemy;

        private Texture2D _borderTexture;
        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 736;
            _graphics.PreferredBackBufferWidth = 1280;
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
            enemylinkstexture = Content.Load<Texture2D>("lava-enemy-linkslopen");

            _borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _background = new Background(backgroundTexture);

            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolinkslooptexture,herorechtslooptexture,herostiltexture, new KeyBoardReader());
            enemy = new Enemy(enemyrechtstexture,enemylinkstexture, new Vector2(400, 400));
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
        private void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangle, int thickness, Color color)
        {
            // Bovenste rand
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            // Onderste rand
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);
            // Linkerrand
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            // Rechterrand
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin(this.camera);
            

            _background.Draw(_spriteBatch);

            hero.Draw(_spriteBatch);
            enemy.Draw(_spriteBatch);

            DrawBorder(_spriteBatch, hero.BoundingBox, 2, Color.Red);
            DrawBorder(_spriteBatch, enemy.BoundingBox, 2, Color.Red);

            _spriteBatch.End();

           
         
          

            base.Draw(gameTime); 
        }
    }
}
