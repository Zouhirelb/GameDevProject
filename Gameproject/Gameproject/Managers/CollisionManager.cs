using System;
using System.Collections.Generic;
using Gameproject.Enemies;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Managers
{
    internal class CollisionManager : ICollsionHandler
    {
        private List<IGameObject> gameObjects;
        private ICollsionHandler collisionHandler;
        public CollisionManager() { }
        public CollisionManager(ICollsionHandler collisionHandler)
        {
            this.collisionHandler = collisionHandler;
            gameObjects = new List<IGameObject>();
        }
        public void HandleCollision(IGameObject objA, IGameObject objB)
        {

            if (objA is Monster && objB is Monster)
            {
                HandleEnemyEnemyCollision((Monster)objA, (Monster)objB);
            }

            if (objA is Hero && objB is Monster || objA is Monster && objB is Hero)
            {
                Hero hero;
                Monster enemy;


                if (objA is Hero)
                {
                    hero = (Hero)objA;
                    enemy = (Monster)objB;
                }
                else
                {
                    hero = (Hero)objB;
                    enemy = (Monster)objA;
                }


                Console.WriteLine("Hero en Enemy botsen!");


             

                var healthManager = new HealthManager();
                if (hero is Hero healthHero)
                {
                    healthManager.ApplyDamage(healthHero, 5);
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
        private void HandleEnemyEnemyCollision(Monster enemy1, Monster enemy2)
        {
            
            Rectangle enemy1BoundingBox = enemy1.BoundingBox;
            Rectangle enemy2BoundingBox = enemy2.BoundingBox;

            if (enemy1BoundingBox.Intersects(enemy2BoundingBox))
            {
                Vector2 directie = enemy2.Positie - enemy1.Positie;
                directie.Normalize();

                Vector2 verschuiving = directie * 5f; 
                enemy2.Positie += verschuiving;
            }
        }
        public void RegisterObject(IGameObject obj)
        {
            if (!gameObjects.Contains(obj))
            {
                gameObjects.Add(obj);
            }
        }


        public void UnregisterObject(IGameObject obj)
        {
            gameObjects.Remove(obj);
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    var objA = gameObjects[i];
                    var objB = gameObjects[j];

                    if (objA.BoundingBox.Intersects(objB.BoundingBox))
                    {
                        collisionHandler.HandleCollision(objA, objB);
                    }
                }
            }
        }

    }






}
