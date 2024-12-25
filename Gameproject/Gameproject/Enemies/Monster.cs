using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Enemies;
using Gameproject.Interfaces;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject { 


        public class Monster : Enemy,IHealth
    {
            public Texture2D Runrighttexture;
            public Texture2D Runlefttexture;
            public Texture2D CurrentTexture;
            public Texture2D deadtexture;
            
            public Animation.Animations deathanimation;
            public Animation.Animations Runrightanimation;
            public Animation.Animations Runleftanimation;
            public Animation.Animations Currentanimation;
            
            IEnemybehavior behavior;
            private int health = 30;
            private bool isDying;
            private float deathTimer = 0f;
            public override int DamageToHero => 2;
            public int Health
            {
                get => health;
                set => health = value;
            }
            public bool IsDead => isDying;

        public Monster(Texture2D textureright, Texture2D textureleft, Texture2D deadtexture, Vector2 startPosition, IEnemybehavior behavior) : base(startPosition,behavior)
            {
                this.Runrighttexture = textureright;
                this.deadtexture = deadtexture;
                this.Runlefttexture = textureleft;
                this.behavior = behavior;

            deathanimation = new Animation.Animations();
            Runleftanimation = new Animation.Animations();
            Runrightanimation = new Animation.Animations();

                int[] runpixels = { 0, 57, 114, 171, 228 };
                int[] Deathpixels = { 0, 65, 130, 195, 260 };

                foreach (var pixel in Deathpixels)
                {
                    deathanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 65, 57)));
                }

                foreach (var pixel in runpixels)
                {
                    Runleftanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 57, 46)));
                    Runrightanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0,57, 46)));
                }

                Currentanimation = Runrightanimation;
            }
            public override int Width => 57; 
            public override int Height => 46;
        public int ScoreValue => 10;
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0 && !isDying)
            {
                isDying = true;
                Currentanimation = deathanimation;
                ScoreManager.Instance.AddScore(ScoreValue);
                LevelManager.Instance.NotifyEnemyDied();
            }
        }
        public override void Die()
        {
            if (!isDying)
            {
                isDying = true;
                LevelManager.Instance.NotifyEnemyDied();
                EnemyManager.Instance.RemoveEnemy(this);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
            {
            if (Currentanimation == deathanimation)
            {
                CurrentTexture = deadtexture;  
            }
            else if (Currentanimation == Runrightanimation)
            {
                CurrentTexture = Runrighttexture;
            }
            else
            {
                CurrentTexture = Runlefttexture;
            }

            spriteBatch.Draw(CurrentTexture, Position,
                             Currentanimation.CurrentFrame.SourceRectangle,
                             Color.White);

        }
        public override void Update(GameTime gameTime, Vector2 heropositie)
        {
            if (Health <= 0)
            {
                Die();
            }
            if (!isDying)
            {
                behavior.Execute(this, heropositie, gameTime);
            }
            else
            {
                deathanimation.Update(gameTime);

                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathTimer >= 100f)
                {

                    EnemyManager.Instance.RemoveEnemy(this);
                    CollisionManager.Instance.UnregisterObject(this);
                }


            }

        }
        }
}
