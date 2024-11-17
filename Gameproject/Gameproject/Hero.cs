using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject
{
    public class Hero:IGameObject
    {
        private Texture2D heroTexture;
        private Animatie animatie;
        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;
        private Vector2 mousevector;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(49, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(97, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(145, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(193, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(241, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(289, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(337, 0, 48, 73)));
            positie = new Vector2(10, 10);
            snelheid = new Vector2(1, 1);
            versnelling = new Vector2(0.1f, 0.1f);

        }
        public void Update(GameTime gameTime) 
        {

            Move(GetMouseState());
            animatie.Update(gameTime);

        }

        private Vector2 GetMouseState()
        {
            MouseState state = Mouse.GetState();
            mousevector = new Vector2(state.X, state.Y);
            return mousevector;
        }

        private void Move(Vector2 mouse)
        {

            var direction = Vector2.Add(mouse, -positie);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 1f);

            positie += direction;
            snelheid += versnelling;
            snelheid = Limit(snelheid, 4);

            float tmp = snelheid.Length();
            
            if (positie.X > 600 || positie.X< 0)
            {
                snelheid.X *= -1;
                versnelling.X *= -1;

            }
            if (positie.Y > 400 || positie.Y< 0)
            {
                snelheid.Y *= -1;
                versnelling *= 1;
            }
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(heroTexture, positie,animatie.CurrentFrame.SourceRectangle , Color.White,0, new Vector2(0,0),1f,SpriteEffects.None,0);
        }

    }
}
