using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Enemies
{
    public class Skeleton : Enemy , IHealth
    {
        public Texture2D textureRight;
        public Texture2D textureLeft;
        public Texture2D textureIdle;
        public Texture2D textureDeath;
        public Texture2D textureAttackRight;
        public Texture2D textureAttackLeft;
        public Texture2D textureCurrent;

        public Animatie IdleAnimation;
        public Animatie DeathAnimation;
        public Animatie AttackRightAnimation;
        public Animatie AttackLeftAnimation;
        public Animatie RightrunAnimation;
        public Animatie leftrunAnimation;
        public Animatie CurrentAnimation;

        IEnemybehavior behavior;

        private int health = 15;

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

        public bool IsDead => isDead;
        public Skeleton(Texture2D textureRight, Texture2D textureLeft, Texture2D textureIdle, Texture2D textureDeath, Texture2D textureAttackRight, Texture2D textureAttackLeft, Vector2 startPositie, IEnemybehavior behavior) : base(startPositie, behavior)
        {
            this.textureRight = textureRight;
            this.textureLeft = textureLeft;
            this.textureIdle = textureIdle;
            this.textureDeath = textureDeath;
            this.textureAttackRight = textureAttackRight;
            this.textureAttackLeft = textureAttackLeft;
            this.textureCurrent = textureIdle;

            this.behavior = behavior;
            
            leftrunAnimation = new Animatie();
            RightrunAnimation = new Animatie();
            IdleAnimation = new Animatie();
            DeathAnimation = new Animatie();
            AttackRightAnimation = new Animatie();
            AttackLeftAnimation = new Animatie();

            int[] runpixels = { 0, 128, 256, 384, 512, 640, 768, 896 };
            int[] idlepixels = { 0, 128, 256, 384, 512, 640, 768 };
            int[] attackpixels = { 0, 128, 256, 384, 512};
            int[] deathpixels = { 0, 128, 256, 384};


           

            foreach (var pixel in runpixels)
            {
                    leftrunAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
                    RightrunAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
            foreach (var pixel in deathpixels)
            {
                DeathAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
            foreach (var pixel in attackpixels)
            {
                AttackRightAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
                AttackLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
            foreach (var pixel in idlepixels)
            {
                IdleAnimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 128, 80)));
            }
            CurrentAnimation = IdleAnimation;



        }
        public override int Breedte => 126;

        public override int Hoogte => 80;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"Skeleton took {damage} damage, HP={Health}");
                if (health <= 0)
                {
                    isDead = true;
                    CurrentAnimation = DeathAnimation;
                }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureCurrent, Positie, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

       

        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
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
