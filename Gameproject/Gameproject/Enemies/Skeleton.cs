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
        private Texture2D textureRight;
        private Texture2D textureLeft;
        private Texture2D textureIdle;
        private Texture2D textureDeath;
        private Texture2D textureAttack;

        private Animatie IdleAnimation;
        private Animatie DeathAnimation;
        private Animatie AttackAnimation;
        private Animatie RightrunAnimation;
        private Animatie leftrunAnimation;
        private Animatie CurrentAnimation;

        private IEnemybehavior behavior;

       
        
        public Skeleton(Texture2D textureRight, Texture2D textureLeft, Texture2D textureIdle, Texture2D textureDeath, Texture2D textureAttack,Vector2 startPositie, IEnemybehavior behavior) : base(startPositie)
        {
            this.textureRight = textureRight;
            this.textureLeft = textureLeft;
            this.textureIdle = textureIdle;
            this.textureDeath = textureDeath;
            this.textureAttack = textureAttack;
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
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(i, 0, 57, 46)));
                }
            }
            else if (CurrentAnimation == leftrunAnimation || CurrentAnimation == RightrunAnimation)
            {
                for (int i = 0; i < 8; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(i, 0, 57, 46)));
                }
            }
            else if (CurrentAnimation == AttackAnimation)
            {
                for (int i = 0; i < 5; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(i, 0, 57, 46)));
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    CurrentAnimation.AddFrame(new AnimationFrame(new Rectangle(i, 0, 57, 46)));
                }
            }
            

            
        }
        public override int Breedte => throw new NotImplementedException();

        public override int Hoogte => throw new NotImplementedException();

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
            throw new NotImplementedException();
        }
    }
}
