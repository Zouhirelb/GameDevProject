using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class SkeletonBehavior : IEnemybehavior<Skeleton>
    {
        private const float DetectionRange = 200f;
        private const float AttackRange = 50f;
        private const float Speed = 1f;
        public void Execute(Skeleton skeleton, Vector2 heroPositie,GameTime gameTime)
        {
            
            var direction = heroPositie - skeleton.Positie;
            float distance = direction.Length();

            if (distance < AttackRange)
            {
                skeleton.CurrentAnimation = skeleton.AttackAnimation;
                skeleton.textureCurrent = skeleton.textureAttack;
            }
            else if (distance< DetectionRange)
            {
                if (direction.X > 0)
                {
                    skeleton.CurrentAnimation = skeleton.RightrunAnimation;
                    skeleton.textureCurrent = skeleton.textureRight;
                }
                else
                {
                    skeleton.CurrentAnimation = skeleton.leftrunAnimation;
                    skeleton.textureCurrent = skeleton.textureLeft;
                }

            }
            else
            {
                skeleton.CurrentAnimation = skeleton.IdleAnimation;
                skeleton.textureCurrent = skeleton.textureIdle;
            }
            skeleton.CurrentAnimation.Update(gameTime);
        }
    }
    
}
