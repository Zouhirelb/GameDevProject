using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class SkeletonBehavior : IEnemybehavior
    {
        private const float DetectionRange = 350f;
        private const float AttackRange = 130f;
        private const float Speed = 1.5f;
        public void Execute(Enemy enemy, Vector2 heroPositie,GameTime gameTime)
        {
            if (enemy is Skeleton skeleton)
            {

           
            var direction = heroPositie - skeleton.Positie;
            float distance = direction.Length();

            if (distance<AttackRange)
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
                    direction.Normalize();
                    skeleton.Positie += direction * Speed;
            }
            else if (distance< DetectionRange)
            {
                    direction.Normalize();
                    skeleton.Positie += direction * Speed;
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
                    skeleton.CurrentAnimation.Update(gameTime);

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
