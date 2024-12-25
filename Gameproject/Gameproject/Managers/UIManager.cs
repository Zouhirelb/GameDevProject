using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Heromap;
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

        public void Draw(SpriteBatch spriteBatch, Viewport viewport)
        {
            spriteBatch.Begin();

            HP.Instance.Draw(spriteBatch, _hero);
           
            string scoreText = $"Score {ScoreManager.Instance.Score}";
            Vector2 ScoreSize = _font.MeasureString(scoreText);
            spriteBatch.DrawString(_font, scoreText, new Vector2((viewport.Width - ScoreSize.X)-10,10), Color.LightGreen);

            string levelText = $"Level {LevelManager.Instance.CurrentLevel}";
            Vector2 levelSize = _font.MeasureString(levelText);
            spriteBatch.DrawString(_font, levelText, new Vector2((viewport.Width - levelSize.X)/2,10), Color.DarkGoldenrod);
            spriteBatch.End();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

          
          

            spriteBatch.End();
        }

    }
}
