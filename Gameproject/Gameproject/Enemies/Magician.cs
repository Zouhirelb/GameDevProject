using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies
{
    public class Magician:Enemy
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
        private int counter;
        public Texture2D FireballRightTexture { get; internal set; }
        public Texture2D FireballLeftTexture { get; internal set; }


        IEnemybehavior behavior;

        private int health = 30;

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    isDead = true;
                }
            }
        }

        private bool isDead;
        public bool IsDead => isDead;
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

            leftrunAnimation = new Animatie();
            RightrunAnimation = new Animatie();
            IdleAnimation = new Animatie();
            DeathAnimation = new Animatie();
            AttackRightAnimation = new Animatie();
            AttackLeftAnimation = new Animatie();

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
                counter = 8;
            }
            else if (CurrentAnimation == IdleAnimation)
            {
                counter = 7;
            }
            else
            {
                counter = 6;
            }


        }
        public override int Breedte => textureCurrent.Width / counter;

        public override int Hoogte => textureCurrent.Height;

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureCurrent, Positie, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
            behavior.Execute(this, heroPosition, gameTime);
        }

        
    }
}
