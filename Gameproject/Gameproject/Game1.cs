using Comora;
using Gameproject.Enemies.behavior;
using Gameproject.Enemies;
using Gameproject.Input;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            SpriteFont font = Content.Load<SpriteFont>("Font");
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

            _borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _background = new Background(backgroundTexture);

            hp = new HP(font);
            

            InitializeGameObjects();

            uiManager = new UIManager(font, hero);

        }

        private void InitializeGameObjects()
        {
            hero = new Hero(herolinkslooptexture,herorechtslooptexture,herostiltexture, new KeyBoardReader());

            enemyManager = new EnemyManager();

            for (int i = 0; i <10; i++)
            {
                Random = new Random();
                enemyManager.AddEnemy(new Skeleton(skeletonRightTexture, skeletonLeftTexture, skeletonIdleTexture, skeletonDeathTexture, skeletonAttackRightTexture, skeletonAttackLeftTexture, new Vector2(Random.Next(-1000, 1000), Random.Next(-1000, 1000)), new SkeletonBehavior()));
                enemyManager.AddEnemy(new Monster(monsterrechtstexture, monsterlinkstexture, monsterDeathTexture, new Vector2(Random.Next(-1000,1000), Random.Next(-1000, 1000)), new MonsterBehavior()));
                enemyManager.AddEnemy(new Magician(FireballRightTexture,FireballLeftTexture,magicianRightTexture, magicianLeftTexture, magicianIdleTexture, magicianDeathTexture, magicianAttackRightTexture, magicianAttackLeftTexture, new Vector2(Random.Next(-1000, 1000), Random.Next(-1000, 1000)), new MagicianBehavior()));

                //wizard bijvoegen
            }



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
            FireballManager.GetInstance().Update(gameTime);

            CollisionManager.Instance.CheckCollisions();

            this.camera.Position = hero.Positie;
            this.camera.Update(gameTime);

            base.Update(gameTime);
        }
        //private void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangle, int thickness, Color color)
        //{
        //    spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
        //    spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);
        //    spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
        //    spriteBatch.Draw(_borderTexture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        //}
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen); 
                    
            _spriteBatch.Begin(this.camera);
                        
            _background.Draw(_spriteBatch);

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

            uiManager.Draw(_spriteBatch,hero);
                   
            base.Draw(gameTime); 
        }
    }
}
