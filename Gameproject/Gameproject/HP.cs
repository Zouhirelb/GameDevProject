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
        private static HP instance;
        public static HP Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HP();
                }
                return instance;
            }
        }

        private SpriteFont font;

        private HP()
        {

        }
        public void Initialize(SpriteFont spriteFont)
        {
            font = spriteFont;
        }
        public void Draw(SpriteBatch spriteBatch, Hero hero)
        {
            string healthText = $"HP: {hero.Health}";
            spriteBatch.DrawString(font, healthText, new Vector2(10, 10), Color.White);
        }
    }
}
