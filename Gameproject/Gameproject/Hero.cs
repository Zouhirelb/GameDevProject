using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Input;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject
{
    public class Hero:IGameObject
    {
        private Texture2D heldlooptexture;
        private Texture2D heldstiltexture;
        private Texture2D huidigetexture;

        private Animatie loopanimatie;
        private Animatie stilanimatie;
        private Animatie huidigeanimatie;

        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;
        private Vector2 mousevector;
        IinputReader inputReader;

        public Hero(Texture2D texture, Texture2D idletexture, IinputReader reader)
        {
            heldstiltexture = idletexture;
            heldlooptexture = texture;
            

            this.inputReader = reader;
            positie = new Vector2(10, 10);

            stilanimatie = new Animatie();
            stilanimatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 39,39)));

            loopanimatie = new Animatie();
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(49, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(97, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(145, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(193, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(241, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(289, 0, 48, 50)));
            loopanimatie.AddFrame(new AnimationFrame(new Rectangle(337, 0, 48, 50)));
            
            snelheid = new Vector2(1, 1);
            versnelling = new Vector2(0.1f, 0.1f);

            huidigeanimatie = stilanimatie;



        }
        public void Update(GameTime gameTime) 
        {
            var directie = inputReader.ReaderInput();

            if (huidigetexture == null)
            {
             
                huidigetexture = heldstiltexture;
            }

            positie.X = MathHelper.Clamp(positie.X, 0, 1140 - huidigetexture.Width); 
            positie.Y = MathHelper.Clamp(positie.Y, 0, 480 - huidigetexture.Height); 

            
            if (directie == Vector2.Zero)
            {
                huidigeanimatie = stilanimatie;

            }
            else
            {

                huidigeanimatie = loopanimatie;
                directie *= 4;
                positie += directie;
            }


            // Move(GetMouseState());
            loopanimatie.Update(gameTime);

        }

        //private Vector2 GetMouseState()
        //{
        //    MouseState state = Mouse.GetState();
        //    mousevector = new Vector2(state.X, state.Y);
        //    return mousevector;
        //}

        private void Move(Vector2 mouse)
        {

            var directie = Vector2.Add(mouse, -positie);
            directie.Normalize();
            directie = Vector2.Multiply(directie, 1f);
            // deze code nog refactoren
            positie += directie;
            snelheid += versnelling;
            snelheid = Limit(snelheid, 4);
            // tot hier

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


            if (huidigeanimatie == stilanimatie)
            {
                huidigetexture = heldstiltexture;
            }
            else
            {
                huidigetexture = heldlooptexture;
            }



            spriteBatch.Draw(huidigetexture, positie,huidigeanimatie.CurrentFrame.SourceRectangle , Color.White,0, new Vector2(0,0),1.2f,SpriteEffects.None,0);
        }

    }
}
