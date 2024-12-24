using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Managers
{
    public class UIManager
    {
        private SpriteFont _font;
        private Hero _hero;
        private HP HP;

        public UIManager(SpriteFont font, Hero hero)
        {
            _font = font;
            _hero = hero;
        }

        public void Draw(SpriteBatch spriteBatch, Hero herohealth)
        {
            spriteBatch.Begin();

            string healthText = $"HP: {herohealth.Health}";
            spriteBatch.DrawString(_font, healthText, new Vector2(10, 10), Color.White);

            string scoreText = $"Score: {ScoreManager.Instance.Score}";
            spriteBatch.DrawString(_font, scoreText, new Vector2(10, 40), Color.Yellow);

            string levelText = $"Level: {ScoreManager.Instance.Level}";
            spriteBatch.DrawString(_font, levelText, new Vector2(10, 70), Color.LightGreen);
            spriteBatch.End();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

          
          

            spriteBatch.End();
        }

    }
}
