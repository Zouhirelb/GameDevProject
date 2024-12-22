using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Gameproject.Interfaces;

namespace Gameproject.Enemies
{
    public abstract class Enemy: IGameObject
    {
        public Vector2 Positie { get; set; }
        public abstract int Breedte { get; }
        public abstract int Hoogte { get; }

        public IEnemybehavior behavior { get; private set; }
        public Rectangle BoundingBox => new Rectangle((int)Positie.X, (int)Positie.Y, Breedte, Hoogte);

        public Enemy(Vector2 startPositie, IEnemybehavior behavior)
        {
            Positie = startPositie;
            this.behavior = behavior;
        }

        public abstract void Update(GameTime gameTime, Vector2 heroPosition);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

