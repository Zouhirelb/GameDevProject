using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class SkeletonBehavior : IEnemybehavior
    {
        private const float DetectionRange = 200f;
        private const float AttackRange = 128f;
        private const float Speed = 1f;
        public void Execute(Enemy enemy, Vector2 heroPositie,GameTime gameTime)
        {
            if (enemy is Skeleton skeleton)
            {

           
            var direction = heroPositie - skeleton.Positie;
            float distance = direction.Length();

            if (distance < AttackRange)
            {
                    if (direction.X > 0)
                    {
                        skeleton.CurrentAnimation = skeleton.AttackRightAnimation;
                        skeleton.textureCurrent = skeleton.textureAttackRight;
                    }
                    else
                    {
                        skeleton.CurrentAnimation = skeleton.AttackLeftAnimation;
                        skeleton.textureCurrent = skeleton.textureAttackLeft;
                    }
                    
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
}
