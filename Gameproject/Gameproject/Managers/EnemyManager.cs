﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Gameproject.Interfaces;
using Gameproject.Enemies;

namespace Gameproject.Managers
{
    public class EnemyManager
    {
        private static EnemyManager instance;
        public static EnemyManager Instance
        {
            get
            {
                return instance;
            }
        }

        private List<Enemy> enemies;

        public EnemyManager()
        {
            instance = this;
            enemies = new List<Enemy>();
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
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

        public IEnumerable<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}