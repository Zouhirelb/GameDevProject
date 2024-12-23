using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class MagicianBehavior : IEnemybehavior
    {
        private const float DetectionRange = 500f;
        private const float FireballRange = 300f;
        private const float Speed = 1.5f;
        public void Execute(Enemy enemy, Vector2 heroPositie, GameTime gameTime)
        {
            if (enemy is Magician magician)
            {


                var direction = heroPositie - magician.Positie;
                float distance = direction.Length();

                if (distance > DetectionRange)
                {
                    magician.CurrentAnimation = magician.IdleAnimation;
                    magician.textureCurrent = magician.textureIdle;
                }
                else if (distance > FireballRange)
                {
                    direction.Normalize();
                    magician.Positie += direction * Speed;

                    if (direction.X > 0)
                    {
                        magician.CurrentAnimation = magician.RightrunAnimation;
                        magician.textureCurrent = magician.textureRight;
                    }
                    else
                    {
                        magician.CurrentAnimation = magician.leftrunAnimation;
                        magician.textureCurrent = magician.textureLeft;
                    }


                }
                else
                {
                    Fireball(magician, heroPositie, gameTime);

                }
                magician.CurrentAnimation.Update(gameTime);
            }

        }
        public void Fireball(Enemy enemy, Vector2 heroPositie, GameTime gameTime)
        {
            if (enemy is Magician magician)
            {
                var direction = heroPositie - magician.Positie;
            

                if (direction.X > 0)
                {
                    magician.CurrentAnimation = magician.AttackRightAnimation;
                    magician.textureCurrent = magician.textureAttackRight;
                }
                else
                {
                    magician.CurrentAnimation = magician.AttackLeftAnimation;
                    magician.textureCurrent = magician.textureAttackLeft;
                }
            }

        }
    }
}
