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
        private static CollisionManager _instance;
        public static CollisionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CollisionManager();
                return _instance;
            }
        }
        private CollisionManager() { gameObjects = new List<IGameObject>(); }
        public CollisionManager(ICollsionHandler collisionHandler)
        {
            this.collisionHandler = collisionHandler;
            gameObjects = new List<IGameObject>();
        }
       
        public void HandleCollision(IGameObject objA, IGameObject objB)
        {

            if (objA is Hero hero && objB is Enemy enemy)
            {
                int damage = 0;
                if (enemy is Monster mon) damage = mon.DamageToHero;
                else if (enemy is Skeleton skel) damage = skel.DamageToHero;
                else if (enemy is Magician mag) damage = mag.DamageToHero;

                var healthManager = new HealthManager();
                healthManager.ApplyDamage(hero, damage);

                Console.WriteLine($"Hero got hit by {enemy.GetType().Name}, damage: {damage}");

            }
            else if (objA is Enemy enemy2 && objB is Hero hero2)
            {
                int damage = 0;
                if (enemy2 is Monster mon) damage = mon.DamageToHero;
                else if (enemy2 is Skeleton skel) damage = skel.DamageToHero;
                else if (enemy2 is Magician mag) damage = mag.DamageToHero;

                var healthManager = new HealthManager();
                healthManager.ApplyDamage(hero2, damage);

                Console.WriteLine($"Hero got hit by {enemy2.GetType().Name}, damage: {damage}");

            }

            if (objA is Enemy && objB is Enemy)
            {
                HandleEnemyEnemyCollision((Enemy)objA, (Enemy)objB);
            }
            
            if (objA is Hero && objB is Enemy || objA is Enemy && objB is Hero)
            {
                Hero Hero;
                Enemy _enemy;


                if (objA is Hero)
                {
                    Hero = (Hero)objA;
                    _enemy = (Enemy)objB;
                }
                else
                {
                    Hero = (Hero)objB;
                    _enemy = (Enemy)objA;
                }

              


                Rectangle heroBoundingBox = Hero.BoundingBox;
                Rectangle enemyBoundingBox = _enemy.BoundingBox;

                Vector2 richting = _enemy.Positie - Hero.Positie;
                richting.Normalize();

                if (heroBoundingBox.Intersects(enemyBoundingBox))
                {
                    Vector2 verschuiving = richting * 5f; // 5 pixel verschuiving

                    _enemy.Positie += verschuiving;
                }

            }
            if (objA is Hero _hero && objB is FireBall fireball)
            {
                HandlefireballCollision(_hero, fireball);
            }
            else if (objA is FireBall fireball2 && objB is Hero hero2)
            {
                HandlefireballCollision(hero2, fireball2);
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
                hero.TakeDamage(10);
                fireBall.Destroy();
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
                        HandleCollision(objA, objB);
                    }
                }
            }
        }

    }






}
