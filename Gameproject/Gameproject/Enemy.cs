using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject { 


        public class Enemy : IGameObject
        {
            private Texture2D looptexture;
            private Texture2D doodtexture;
            private Animatie rechtsloopanimatie;  // Animatie voor de rechtsloop
            private Vector2 positie;  // Positie van de vijand

        // Constructor om de textures en animatie in te stellen
        public Enemy(Texture2D texturerechts, Vector2 startPositie)
            {
                this.looptexture = texturerechts;
                //this.doodtexture = doodtexure;
                this.positie = startPositie;

                // Voeg frames toe aan de animatie
                rechtsloopanimatie = new Animatie();
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle( 0,0,57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle( 57,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle( 114,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(171,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(228,0, 57, 46)));

        }

            // Update de animatie per frame
            public void Update(GameTime gameTime, Vector2 heropositie)
            {
            rechtsloopanimatie.Update(gameTime);

            Vector2 richting = heropositie - positie;
            richting.Normalize();
            positie += richting * 2f;

        }

            // Teken de vijand op het scherm
            public void Draw(SpriteBatch spriteBatch)
            {
                // Zorg ervoor dat je de juiste positie gebruikt om te tekenen
                spriteBatch.Draw(looptexture, positie, rechtsloopanimatie.CurrentFrame.SourceRectangle, Color.White);
            }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
    }
