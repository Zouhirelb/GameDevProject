using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Gameproject.Interfaces;
using Gameproject.Managers;

namespace Gameproject.Enemies
{
    public abstract class Enemy: IGameObject
    {
        public abstract int DamageToHero { get; }
        public Vector2 Position { get; set; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public IEnemybehavior behavior { get; private set; }
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        
        public Enemy(Vector2 startPositie, IEnemybehavior behavior)
        {
            Position = startPositie;
            this.behavior = behavior;
        }

        public abstract void Update(GameTime gameTime, Vector2 heroPosition);
        public abstract void Draw(SpriteBatch spriteBatch);
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Die()
        {
            LevelManager.Instance.NotifyEnemyDied();
            EnemyManager.Instance.RemoveEnemy(this);
        }

    }
}

