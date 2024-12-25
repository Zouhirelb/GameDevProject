using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Gameproject.Managers;

namespace Gameproject.Enemies
{
    public class Magician:Enemy, IHealth
    {
        public Texture2D textureRight;
        public Texture2D textureLeft;
        public Texture2D textureIdle;
        public Texture2D textureDeath;
        public Texture2D textureAttackRight;
        public Texture2D textureAttackLeft;
        public Texture2D textureCurrent;

        public Animation.Animations IdleAnimation;
        public Animation.Animations DeathAnimation;
        public Animation.Animations AttackRightAnimation;
        public Animation.Animations AttackLeftAnimation;
        public Animation.Animations RightrunAnimation;
        public Animation.Animations leftrunAnimation;
        public Animation.Animations CurrentAnimation;
        private int counter;
        private bool isDying;
        public bool IsDead => isDying;
        public override int DamageToHero => 1;
        public Texture2D FireballRightTexture { get; internal set; }
        public Texture2D FireballLeftTexture { get; internal set; }


        IEnemybehavior behavior;

        private int health = 30;
        public int ScoreValue => 30;
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                
            }
        }

        private bool isDead;
        private float deathTimer;

        public Magician(Texture2D fireballRightTexture, Texture2D fireballLeftTexture,Texture2D textureRight, Texture2D textureLeft, Texture2D textureIdle, Texture2D textureDeath, Texture2D textureAttackRight, Texture2D textureAttackLeft, Vector2 startPositie, IEnemybehavior behavior) : base(startPositie, behavior)
        {
            this.FireballRightTexture = fireballRightTexture;
            this.FireballLeftTexture = fireballLeftTexture;
            this.textureRight = textureRight;
            this.textureLeft = textureLeft;
            this.textureIdle = textureIdle;
            this.textureDeath = textureDeath;
            this.textureAttackRight = textureAttackRight;
            this.textureAttackLeft = textureAttackLeft;
            this.textureCurrent = textureIdle;

            this.behavior = behavior;

            leftrunAnimation = new Animation.Animations();
            RightrunAnimation = new Animation.Animations();
            IdleAnimation = new Animation.Animations();
            DeathAnimation = new Animation.Animations();
            AttackRightAnimation = new Animation.Animations();
            AttackLeftAnimation = new Animation.Animations();

            int[] run_attack_pixels = { 0, 128, 256, 384, 512, 640, 768, 896 };
            int[] idlepixels = { 0, 128, 256, 384, 512, 640, 768 };
            int[] deathpixels = { 0, 128, 256, 384, 512,640 };


            foreach (var pixel in run_attack_pixels)
            {
                leftrunAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
                RightrunAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
                AttackRightAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
                AttackLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
            foreach (var pixel in deathpixels)
            {
                DeathAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
           
            foreach (var pixel in idlepixels)
            {
                IdleAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }

            CurrentAnimation = IdleAnimation;
            if (CurrentAnimation == AttackLeftAnimation|| CurrentAnimation == AttackRightAnimation|| CurrentAnimation == leftrunAnimation|| CurrentAnimation == RightrunAnimation )
            {
                counter = 13;
            }
            else if (CurrentAnimation == IdleAnimation)
            {
                counter = 13;
            }
            else
            {
                counter = 10;
            }


        }
        public override int Width => textureCurrent.Width / counter;

        public override int Height => textureCurrent.Height;

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0 && !isDying)
            {
                isDying = true;
                CurrentAnimation = DeathAnimation;
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
            spriteBatch.Draw(textureCurrent, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
            if (Health <= 0) 
            {
                Die();
            }

            if (!isDead)
            {
                behavior.Execute(this, heroPosition, gameTime);
            }
            else
            {
                DeathAnimation.Update(gameTime);

                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathTimer >= 1.5f)
                {

                    EnemyManager.Instance.RemoveEnemy(this);
                    CollisionManager.Instance.UnregisterObject(this);
                }

            }
        }

        
    }
}
