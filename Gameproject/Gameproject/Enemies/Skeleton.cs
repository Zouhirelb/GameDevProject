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

       
        private int[] pixels = {0,128,256,384,512,640,768,896};
        public Skeleton(Texture2D textureRight, Texture2D textureLeft, Texture2D textureIdle, Texture2D textureDeath, Texture2D textureAttack,Vector2 startPositie, IEnemybehavior behavior) : base(startPositie)
        {
            
            this.behavior = behavior;

            leftrunAnimation = new Animatie();
            RightrunAnimation = new Animatie();

            int[] pixels = { 0, 57, 114, 171, 228 };

            foreach (var item in pixels)
            {
                leftrunAnimation.AddFrame(new AnimationFrame(new Rectangle(item, 0, 57, 46)));
                RightrunAnimation.AddFrame(new AnimationFrame(new Rectangle(item, 0, 57, 46)));
            }

            huidigeanimatie = rechtsloopanimatie;
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
