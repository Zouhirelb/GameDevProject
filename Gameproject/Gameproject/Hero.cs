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
    public class Hero : IGameObject
    {
        private Texture2D heldlooprechtstexture;
        private Texture2D heldstiltexture;
        private Texture2D huidigetexture;
        private Texture2D heldlinksllopentexture;

        private Animatie rechtsloopanimatie;
        private Animatie stilanimatie;
        private Animatie huidigeanimatie;
        private Animatie linksloopanimatie;

        private int[] pixels = { 0, 49, 97, 145, 193, 241, 289, 337 };

        private Vector2 positie;

        public Vector2 Positie
        {
             get  {return positie; }
             set { positie = value; }
        }

        private Vector2 snelheid;
        private Vector2 versnelling;
        private Vector2 mousevector;

        IinputReader inputReader;

        public int Breedte { get; private set; }    
        public int Hoogte { get; private set; }     

        public Rectangle BoundingBox => new Rectangle(
            (int)Positie.X,
            (int)Positie.Y,
            Breedte,
            Hoogte
        );
        public Hero(Texture2D texturelinks,Texture2D texturerechts, Texture2D idletexture,  IinputReader reader)
        {
            heldstiltexture = idletexture;
            heldlooprechtstexture = texturerechts;
            heldlinksllopentexture = texturelinks;

            Breedte = heldstiltexture.Width;
            Hoogte = heldstiltexture.Height;

            
            this.inputReader = reader;
            positie = new Vector2(10, 10);

            stilanimatie = new Animatie();
            rechtsloopanimatie = new Animatie();
            linksloopanimatie = new Animatie();

            stilanimatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 39,39)));

            foreach (var item in pixels)
            {
            rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(item, 0, 48, 50)));
            linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(item, 0, 48, 50)));
            }

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

            if (directie == Vector2.Zero)
            {
                huidigeanimatie = stilanimatie;

            }
            else
            {

                huidigeanimatie = rechtsloopanimatie;
                directie *= 4;
                positie += directie;
            }

            // Move(GetMouseState());

            
            
            huidigeanimatie.Update(gameTime);

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
            snelheid = Limit(snelheid, 3);
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
            var directie = inputReader.ReaderInput();

            Breedte = 48;
            Hoogte = 50;

            if (huidigeanimatie == stilanimatie)
            {
                huidigetexture = heldstiltexture;
            }
            else
            {
                huidigetexture = heldlooprechtstexture;
                
            }

            if (directie.X < 0 )  
            {
                huidigetexture = heldlinksllopentexture;
            }
            else if (directie.X > 0 )  
            {
                huidigetexture = heldlooprechtstexture;
                Hoogte += 10;
            }


            spriteBatch.Draw(huidigetexture, positie,huidigeanimatie.CurrentFrame.SourceRectangle , Color.White,0, new Vector2(0,0),1.2f,SpriteEffects.None,0);
        }

    }
}
