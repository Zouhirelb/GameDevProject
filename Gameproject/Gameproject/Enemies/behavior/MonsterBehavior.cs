using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies.behavior
{
    public class MonsterBehavior : IEnemybehavior
    {
        private const float Speed = 0.5f;
        public void Execute(Enemy enemy, Vector2 heroPosition, GameTime gameTime)
        {
            if (enemy is Monster monster)
            {
                      
                
                Vector2 richting = heroPosition - monster.Position;

                float afstand = richting.Length();

                if (afstand > 1f)
                {
                    richting.Normalize();
                    monster.Position += richting * Speed;
                        
                        if (richting.X > 0)
                        {
                            monster.Currentanimation = monster.Runrightanimation;
                        }
                        else if (richting.X < 0)
                        {
                            monster.Currentanimation = monster.Runleftanimation;
                        }
                    monster.Currentanimation.Update(gameTime);
                }
            }
        }
        
    }
}
