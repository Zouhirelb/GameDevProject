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
        private static FireballManager instance;
        private readonly List<FireBall> fireBalls;

        public FireballManager()
        {
            fireBalls = new List<FireBall>();
        }

        public static FireballManager GetInstance()
        {
            if (instance == null)
            {
                instance = new FireballManager();
            }
            return instance;
        }

        public void RegisterFireball(FireBall fireBall)
        {
            fireBalls.Add(fireBall);
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

        public List<FireBall> GetFireBalls()
        {
            return fireBalls;
        }
    }
}
