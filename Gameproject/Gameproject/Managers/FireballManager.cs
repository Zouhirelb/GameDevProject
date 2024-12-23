using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Managers
{
    internal class FireballManager
    {
        private readonly List<FireBall> fireBalls;
        private static FireballManager instance;
                
        public static FireballManager GetInstance( )
        {
            if (instance == null)
            {
                instance = new FireballManager();
            }
            return instance;
        }
        private FireballManager( )
        {
            fireBalls = new List<FireBall>();


        }

        public void RegisterFireball(FireBall fireBall)
        {
          

            fireBalls.Add(fireBall);
            CollisionManager.Instance.RegisterObject(fireBall);
        }
        public void RemoveFireball(FireBall fireBall)
        {
            fireBalls.Remove(fireBall);
            CollisionManager.Instance.UnregisterObject(fireBall);
        }


        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < fireBalls.Count; i++)
            {
                fireBalls[i].Update(gameTime);
            }
        }
       
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var fireball in fireBalls)
            {
                fireball.Draw(spriteBatch);
            }
        }
    
    }
}
