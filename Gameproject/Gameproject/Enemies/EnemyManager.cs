using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies
{
    public class EnemyManager
    {
        private List<Monster> enemies;

        public EnemyManager()
        {
            enemies = new List<Monster>();
        }

        public void AddEnemy(Monster enemy)
        {
            enemies.Add(enemy);
        }

        public void Update(GameTime gameTime, Vector2 heroPositie)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, heroPositie); 
            }
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
