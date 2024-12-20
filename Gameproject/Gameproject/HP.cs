using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject
{
    public class HP
    {
        private SpriteFont font;

        public HP(SpriteFont spritefont)
        {
            this.font = spritefont;
        }

        public void Draw(SpriteBatch spriteBatch, IHealth herohealth)
        {
            string healthText = $"HP: {herohealth.Health}";
            spriteBatch.DrawString(font, healthText, new Vector2(10, 10), Color.White);
        }
    }
}
