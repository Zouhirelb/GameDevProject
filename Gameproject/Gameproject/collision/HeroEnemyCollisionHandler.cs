using System;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.collision
{
    internal class HeroEnemyCollisionHandler : ICollsionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB)
        {
            if (objA is Hero && objB is Enemy || objA is Enemy && objB is Hero)
            {
                Hero hero;
                Enemy enemy;

                
                if (objA is Hero)
                {
                    hero = (Hero)objA;
                    enemy = (Enemy)objB;
                }
                else
                {
                    hero = (Hero)objB;
                    enemy = (Enemy)objA;
                }

               
                Rectangle heroBoundingBox = hero.BoundingBox;
                Rectangle enemyBoundingBox = enemy.BoundingBox;

                Vector2 richting = enemy.Positie - hero.Positie;
                richting.Normalize(); 

                if (heroBoundingBox.Intersects(enemyBoundingBox))
                {
                    Vector2 verschuiving = richting * 5f; // 5 pixel verschuiving

                    enemy.Positie += verschuiving;
                }
            }
        }
    }
}