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
        Random random;
        public void Execute(Enemy enemy, Vector2 heroPositie, GameTime gameTime)
        {
            if (enemy is Magician magician)
            {

                var direction = heroPositie - magician.Positie;
                float distance = direction.Length();
                timeSinceLastFireball += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (distance > DetectionRange)
                {
                   random= new Random();
                    magician.Positie += new Vector2((float)(random.NextDouble() - 0.5), (float)(random.NextDouble() - 0.5)) * Speed;
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
                       
                        Fireball(magician, heroPositie, magician.FireballRightTexture, magician.FireballLeftTexture );
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
        public void Fireball(Enemy enemy, Vector2 heroPositie, Texture2D fireballrightTexture, Texture2D fireballkeftTexture)
        {
            if (enemy is Magician magician)
            {
                var direction = heroPositie - magician.Positie;
                FireBall fireball = new FireBall(magician,magician.Positie, direction, fireballrightTexture, fireballkeftTexture);

                FireballManager.GetInstance().RegisterFireball(fireball);
            }

        }
    }
}
