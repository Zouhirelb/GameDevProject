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

            if (objA is Enemy && objB is Enemy)
            {
                HandleEnemyEnemyCollision((Enemy)objA, (Enemy)objB);
            }
            
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
            if (objA is Hero && objB is FireBall || objA is FireBall && objB is Hero)
            {

                HandlefireballCollision((Hero)objA, (FireBall)objB);
             

            }
        }
        private void HandleEnemyEnemyCollision(Enemy enemy1, Enemy enemy2)
        {
            
            Rectangle enemy1BoundingBox = enemy1.BoundingBox;
            Rectangle enemy2BoundingBox = enemy2.BoundingBox;

            if (enemy1BoundingBox.Intersects(enemy2BoundingBox))
            {
                Vector2 directie = enemy2.Positie - enemy1.Positie;
                if (directie != Vector2.Zero)
                    directie.Normalize();

                Vector2 verschuiving = directie * 2.5f;
                enemy1.Positie -= verschuiving;
                enemy2.Positie += verschuiving;
            }
        }
        private void HandlefireballCollision(Hero hero, FireBall fireBall)
        {

            Rectangle heroBoundingBox = hero.BoundingBox;
            Rectangle fireballBoundingBox = fireBall.BoundingBox;

            if (heroBoundingBox.Intersects(fireballBoundingBox))
            {
                Vector2 directie = fireBall.Positie - hero.Positie;
                if (directie != Vector2.Zero)
                    directie.Normalize();

                Vector2 verschuiving = directie * 2.5f;
                hero.Positie -= verschuiving;
                fireBall.Positie += verschuiving;
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
