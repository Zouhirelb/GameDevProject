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
        private const float Snelheid = 0.5f;
        public void Execute(Enemy enemy, Vector2 heroPositie, GameTime gameTime)
        {
            if (enemy is Monster monster)
            {
                      
                
                Vector2 richting = heroPositie - monster.Positie;

                float afstand = richting.Length();

                if (afstand > 1f)
                {
                    richting.Normalize();
                    monster.Positie += richting * Snelheid;
                        
                        if (richting.X > 0)
                        {
                            monster.huidigeanimatie = monster.rechtsloopanimatie;
                        }
                        else if (richting.X < 0)
                        {
                            monster.huidigeanimatie = monster.linksloopanimatie;
                        }
                    monster.huidigeanimatie.Update(gameTime);
                }
            }
        }
        
    }
}
