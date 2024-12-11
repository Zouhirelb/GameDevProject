using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;

namespace Gameproject.Managers
{
    internal class Collisionmanager:ICollsionHandler
    {
        private List<IGameObject> gameObjects;
        private ICollsionHandler collisionHandler;

        public Collisionmanager(ICollsionHandler collisionHandler)
        {
            this.collisionHandler = collisionHandler;
            gameObjects = new List<IGameObject>();
        }
        public void HandleCollision(IGameObject objA, IGameObject objB)
        {
            if (objA is Hero && objB is Enemy || objA is Enemy && objB is Hero)
            {
                Console.WriteLine("Hero en Enemy botsen!");

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


                Vector2 richting = hero.Positie - enemy.Positie;
                richting.Normalize();
                hero.Positie += richting * 10f;
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
