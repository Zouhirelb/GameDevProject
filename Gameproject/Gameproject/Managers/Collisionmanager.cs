using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;

namespace Gameproject.Managers
{
    internal class Collisionmanager
    {
        private List<IGameObject> gameObjects;
        public Collisionmanager()
        {
            gameObjects = new List<IGameObject>();
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

        public void CheckCollisions(Action<IGameObject, IGameObject> onCollision)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = i; j < gameObjects.Count; j++)
                {
                    var objA = gameObjects[i];
                    var objB = gameObjects[j];
                    if (objA.BoundingBox.Intersects(objB.BoundingBox))
                    {
                        if (onCollision != null)
                        {
                            onCollision.Invoke(objA, objB);
                        }

                    }
                }
            }
        }

    }
}
