using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D texture;
        private Rectangle deelrectangle;
        private int schuifop_x;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            deelrectangle = new Rectangle(schuifop_x, 0,48,73);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("character lopen");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            _spriteBatch.Draw(texture,  new Vector2(10, 10),deelrectangle, Color.White);


            _spriteBatch.End();

            schuifop_x += 49;
            if (schuifop_x>342)
            {
                schuifop_x = 0;
            }
            deelrectangle.X = schuifop_x;
         
          

            base.Draw(gameTime); 
        }
    }
}
