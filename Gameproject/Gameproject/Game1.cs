﻿using Comora;
using Gameproject.Enemies.behavior;
using Gameproject.Enemies;
using Gameproject.Input;
using Gameproject.Managers;
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

        private Texture2D monsterrechtstexture;
        private Texture2D monsterlinkstexture;

        private Texture2D backgroundTexture;
        private Background _background;

        private HP hp;
        private Camera camera;
        private Hero hero;
        private Monster enemy;

        private Texture2D _borderTexture;

        private CollisionManager collisionmanager;
        private UIManager uiManager;
        private EnemyManager enemyManager;
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

            var collisionHandler = new CollisionManager();
            collisionmanager = new CollisionManager(collisionHandler);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            enemyManager = new EnemyManager();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont font = Content.Load<SpriteFont>("Font");
            backgroundTexture = Content.Load<Texture2D>("gras");
            herorechtslooptexture = Content.Load<Texture2D>("character lopen");
            herolinkslooptexture = Content.Load<Texture2D>("linkslopen");
            herostiltexture = Content.Load<Texture2D>("stil");

            monsterrechtstexture = Content.Load<Texture2D>("lava-enemy-rechtslopen");
            monsterlinkstexture = Content.Load<Texture2D>("lava-enemy-linkslopen");

            enemyManager.AddEnemy(new Monster(monsterrechtstexture, monsterlinkstexture, new Vector2(300, 200), new MonsterBehavior()));
            uiManager = new UIManager(font, hero);
            hp = new HP(font);

            _borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _background = new Background(backgroundTexture);

            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolinkslooptexture,herorechtslooptexture,herostiltexture, new KeyBoardReader());
            enemy = new Monster(monsterrechtstexture,monsterlinkstexture, new Vector2(400, 400));

            collisionmanager.RegisterObject(hero);
            collisionmanager.RegisterObject(enemy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            enemy.Update(gameTime,hero.Positie);

            collisionmanager.CheckCollisions();

            this.camera.Position = hero.Positie;
            this.camera.Update(gameTime);

         

            base.Update(gameTime);
        }
        private void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangle, int thickness, Color color)
        {
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);
            spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
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

            uiManager.Draw(_spriteBatch,hero);
         
          

            base.Draw(gameTime); 
        }
    }
}
