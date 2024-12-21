using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gameproject.Enemies
{
    public abstract class Enemy
    {
        public Vector2 Positie { get; set; }
        public abstract int Breedte { get; }
        public abstract int Hoogte { get; }
        public Rectangle BoundingBox => new Rectangle((int)Positie.X, (int)Positie.Y, Breedte, Hoogte);

        public Enemy(Vector2 startPositie)
        {
            Positie = startPositie;
        }

        public abstract void Update(GameTime gameTime, Vector2 heroPosition);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

