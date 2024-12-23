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
    public class FireBall:IGameObject
    {
        public Vector2 Positie { get;  set; }
        private Vector2 richting;
        private float snelheid = 5f;
        private Texture2D texture;
        public Rectangle BoundingBox => new Rectangle((int)Positie.X, (int)Positie.Y, Breedte, Hoogte); //niet vergeten grote aan te passen
        public int Breedte => 16;

        public int Hoogte => 16;

        public FireBall(Vector2 startPositie, Vector2 richting, Texture2D texture)
        {
            this.Positie = startPositie;
            this.richting = richting;
            this.richting.Normalize();
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            Positie += richting * snelheid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Positie, Color.White);
        }
    }
}

