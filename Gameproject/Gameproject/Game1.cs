﻿using Comora;
using Gameproject.Enemies.behavior;
using Gameproject.Enemies;
using Gameproject.Input;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Gameproject.Screens;
using Microsoft.Xna.Framework.Audio;

namespace Gameproject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D heroAttacklefttexture;
        private Texture2D heroAttackrighttexture;
        private Texture2D herorechtslooptexture;
        private Texture2D herolinkslooptexture;
        private Texture2D herostiltexture;

        private Texture2D monsterrechtstexture;
        private Texture2D monsterlinkstexture;
        private Texture2D monsterDeathTexture;

        private Texture2D backgroundTexture;
        private Background _background;

        private HP hp;
        private Camera camera;
        private Hero hero;
  
        private Texture2D _borderTexture;

        private UIManager uiManager;
        private EnemyManager enemyManager;
        private FireballManager fireballManager;
        private Random Random;

        private Texture2D skeletonDeathTexture;
        private Texture2D skeletonAttackRightTexture;
        private Texture2D skeletonAttackLeftTexture;
        private Texture2D skeletonIdleTexture;
        private Texture2D skeletonLeftTexture;
        private Texture2D skeletonRightTexture;

        private Texture2D magicianDeathTexture;
        private Texture2D magicianAttackRightTexture;
        private Texture2D magicianAttackLeftTexture;
        private Texture2D FireballRightTexture;
        private Texture2D FireballLeftTexture;
        private Texture2D magicianIdleTexture;
        private Texture2D magicianLeftTexture;
        private Texture2D magicianRightTexture;

        private Texture2D startBackground;
        private Texture2D startButton;
        private Texture2D startMessage;
        private SoundEffect startMusic;
        private SoundEffectInstance startMusicInstance;

        private Texture2D gameoverBackground;
        private Texture2D gameoverMessage;
        private SoundEffect gameoverMusic;
        private SoundEffectInstance gameoverMusicInstance;

        private Rectangle startButtonRectangle; 
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private Rectangle WorldBounds = new Rectangle(0, 0, 2560, 1472);
        private Rectangle mapBounds = new Rectangle(0, 0, 10060, 10072);

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

            this.camera = new Camera(_graphics.GraphicsDevice);

         

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            startBackground = Content.Load<Texture2D>("Blackscreen");
            startButton = Content.Load<Texture2D>("BTN PLAY");
            startMessage = Content.Load<Texture2D>("Start");
            startMusic = Content.Load<SoundEffect>("Startgamesound");

            gameoverBackground = Content.Load<Texture2D>("Blackscreen");
            gameoverMessage = Content.Load<Texture2D>("GameOver");
            gameoverMusic = Content.Load<SoundEffect>("Gameoversound");

            startButtonRectangle = new Rectangle(540, 360, startButton.Width, startButton.Height);

            SpriteFont font = Content.Load<SpriteFont>("Font");
            HP.Instance.Initialize(font);
            backgroundTexture = Content.Load<Texture2D>("gras");
           
            herorechtslooptexture = Content.Load<Texture2D>("character lopen");
            herolinkslooptexture = Content.Load<Texture2D>("linkslopen");
            heroAttacklefttexture = Content.Load<Texture2D>("hero-attack-left");
            heroAttackrighttexture = Content.Load<Texture2D>("hero-attack-right");
            herostiltexture = Content.Load<Texture2D>("stil");

            monsterrechtstexture = Content.Load<Texture2D>("lava-enemy-rechtslopen");
            monsterlinkstexture = Content.Load<Texture2D>("lava-enemy-linkslopen");
            monsterDeathTexture = Content.Load<Texture2D>("lava-enemy-death");

            skeletonRightTexture = Content.Load<Texture2D>("Skeleton_Run_Right");
            skeletonLeftTexture = Content.Load<Texture2D>("Skeleton_Run_Left");
            skeletonIdleTexture = Content.Load<Texture2D>("Skeleton_Idle");
            skeletonAttackRightTexture = Content.Load<Texture2D>("Skeleton_Attack_Right");
            skeletonAttackLeftTexture = Content.Load<Texture2D>("Skeleton_Attack_left");
            skeletonDeathTexture = Content.Load<Texture2D>("Skeleton_Dead");

            magicianRightTexture = Content.Load<Texture2D>("Wizard_Run_Right");
            magicianLeftTexture = Content.Load<Texture2D>("Wizard_Run_Left");
            magicianIdleTexture = Content.Load<Texture2D>("Wizard_Idle");
            magicianAttackRightTexture = Content.Load<Texture2D>("Wizard_Fireball_Attack_Right");
            magicianAttackLeftTexture = Content.Load<Texture2D>("Wizard_Fireball_Attack_Left");
            magicianDeathTexture = Content.Load<Texture2D>("Wizard_Dead");

            FireballLeftTexture = Content.Load<Texture2D>("FireBall_Left");
            FireballRightTexture = Content.Load<Texture2D>("FireBall_Right");
            LevelManager.Instance.Initialize(
            monsterrechtstexture,
            monsterlinkstexture,
            monsterDeathTexture,
            skeletonRightTexture,
            skeletonLeftTexture,
            skeletonIdleTexture,
            skeletonDeathTexture,
            skeletonAttackRightTexture,
            skeletonAttackLeftTexture,
            magicianRightTexture,
            magicianLeftTexture,
            magicianIdleTexture,
            magicianDeathTexture,
            magicianAttackRightTexture,
            magicianAttackLeftTexture,
            FireballRightTexture,
            FireballLeftTexture
            );

            _borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _background = new Background(backgroundTexture, mapBounds);

            

            InitializeGameObjects();

            uiManager = new UIManager(font, hero);

        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolinkslooptexture,herorechtslooptexture,herostiltexture,heroAttacklefttexture,heroAttackrighttexture, new KeyBoardReader());

            enemyManager = new EnemyManager();


            LevelManager.Instance.InitializeWaves();   


            CollisionManager.Instance.RegisterObject(hero);

            foreach (var enemy in enemyManager.GetEnemies())
            {
                CollisionManager.Instance.RegisterObject(enemy);
            }
        
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
            hero.Update(gameTime);

            enemyManager.Update(gameTime, hero.Positie);
            LevelManager.Instance.Update(gameTime);
            FireballManager.GetInstance().Update(gameTime);

            CollisionManager.Instance.CheckCollisions();
            hero.Positie = new Vector2(
                MathHelper.Clamp(hero.Positie.X, WorldBounds.Left, WorldBounds.Right - hero.Breedte),
                MathHelper.Clamp(hero.Positie.Y, WorldBounds.Top, WorldBounds.Bottom - hero.Hoogte)
            );

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
                    
            _spriteBatch.Begin(this.camera);

            _background.Draw(_spriteBatch, camera.Position, GraphicsDevice.Viewport);

            hero.Draw(_spriteBatch);

            foreach (var enemy in enemyManager.GetEnemies())
            {
                enemy.Draw(_spriteBatch);
            }


           //DrawBorder(_spriteBatch, hero.BoundingBox, 2, Color.Blue);

            //foreach (var enemy in enemyManager.GetEnemies())
            //{
            //    DrawBorder(_spriteBatch, enemy.BoundingBox, 2, Color.Red);
            //}
            FireballManager.GetInstance().Draw(_spriteBatch);           
            _spriteBatch.End();

            uiManager.Draw(_spriteBatch, GraphicsDevice.Viewport);
                   
            base.Draw(gameTime); 
        }
    }
}
