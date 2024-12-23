using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Enemies.behavior
{
    public class MagicianBehavior : IEnemybehavior
    {
        private const float DetectionRange = 500f;
        private const float FireballRange = 300f;
        private const float Speed = 1.5f;
        private float fireballCooldown = 2f; 
        private float timeSinceLastFireball = 0f;
        public void Execute(Enemy enemy, Vector2 heroPositie, GameTime gameTime)
        {
            if (enemy is Magician magician)
            {

                var direction = heroPositie - magician.Positie;
                float distance = direction.Length();
                timeSinceLastFireball += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
                    if (timeSinceLastFireball>= fireballCooldown)
                    {
                        Fireball(magician, heroPositie);
                        timeSinceLastFireball = 0f;
                    }

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
                magician.CurrentAnimation.Update(gameTime);
            }

        }
        public void Fireball(Enemy enemy, Vector2 heroPositie)
        {
            if (enemy is Magician magician)
            {
                var direction = heroPositie - magician.Positie;
                Texture2D fireballTexture = magician.textureCurrent;
                FireBall fireball = new FireBall(magician.Positie, direction, fireballTexture);

                FireballManager.GetInstance().RegisterFireball(fireball);
            }

        }
    }
}
