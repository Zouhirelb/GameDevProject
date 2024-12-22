using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Enemies
{
    public class Skeleton : Enemy
    {
        public Texture2D textureRight;
        public Texture2D textureLeft;
        public Texture2D textureIdle;
        public Texture2D textureDeath;
        public Texture2D textureAttack;
        public Texture2D textureCurrent;

        public Animatie IdleAnimation;
        public Animatie DeathAnimation;
        public Animatie AttackAnimation;
        public Animatie RightrunAnimation;
        public Animatie leftrunAnimation;
        public Animatie CurrentAnimation;

        private IEnemybehavior<Skeleton> behavior;

       
        
        public Skeleton(Texture2D textureRight, Texture2D textureLeft, Texture2D textureIdle, Texture2D textureDeath, Texture2D textureAttack,Vector2 startPositie, IEnemybehavior<Skeleton> behavior) : base(startPositie)
        {
            this.textureRight = textureRight;
            this.textureLeft = textureLeft;
            this.textureIdle = textureIdle;
            this.textureDeath = textureDeath;
            this.textureAttack = textureAttack;
            this.textureCurrent = textureIdle;

            this.behavior = behavior;
            
            leftrunAnimation = new Animatie();
            RightrunAnimation = new Animatie();
            IdleAnimation = new Animatie();
            DeathAnimation = new Animatie();
            AttackAnimation = new Animatie();

            int[] pixels = {0,128,256,384,512,640,768,896};

           
            CurrentAnimation = IdleAnimation;

            if (CurrentAnimation == IdleAnimation)
            {
                for (int i = 0; i < 7; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 57, 46)));
                }
            }
            else if (CurrentAnimation == leftrunAnimation || CurrentAnimation == RightrunAnimation)
            {
                for (int i = 0; i < 8; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 57, 46)));
                }
            }
            else if (CurrentAnimation == AttackAnimation)
            {
                for (int i = 0; i < 5; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 57, 46)));
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 57, 46)));
                }
            }
            

            
        }
        public override int Breedte => textureCurrent.Width;

        public override int Hoogte => textureCurrent.Height;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureCurrent, Positie, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
            
        }
    }
}
