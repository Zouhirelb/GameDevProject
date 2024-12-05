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
            private Texture2D looprechtstexture;
            private Texture2D looplinkstexture;

            private Texture2D huidigeTexture;

            private Texture2D doodtexture;

            private Animatie rechtsloopanimatie;  // Animatie voor de rechtsloop
            private Animatie linksloopanimatie;
            private Animatie huidigeanimatie;

            private Vector2 positie;  // Positie van de vijand
        

        // Constructor om de textures en animatie in te stellen
        public Enemy(Texture2D texturerechts, Texture2D texturelinks, Vector2 startPositie)
            {
                this.looprechtstexture = texturerechts;
                //this.doodtexture = doodtexure;
                this.positie = startPositie;
                this.looplinkstexture = texturelinks;

                // Voeg frames toe aan de animatie
                rechtsloopanimatie = new Animatie();
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(0,0,57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(57,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(114,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(171,0, 57, 46)));
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(228,0, 57, 46)));

                linksloopanimatie = new Animatie();
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 57, 46)));
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(57, 0, 57, 46)));
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(114, 0, 57, 46)));
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(171, 0, 57, 46)));
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(228, 0, 57, 46)));

            huidigeanimatie = rechtsloopanimatie;
        }

            // Update de animatie per frame
            public void Update(GameTime gameTime, Vector2 heropositie)
            {

            var directie = heropositie - positie;
            

            rechtsloopanimatie.Update(gameTime);

            Vector2 richting = heropositie - positie;
            float afstand = richting.Length();

            if (afstand > 1f) // Alleen bewegen als er afstand is
            {
                richting.Normalize();
                positie += richting * 2f; // Snelheid van 2

                // Kies de animatie op basis van de richting
                if (richting.X > 0) // Naar rechts
                {
                    huidigeanimatie = rechtsloopanimatie;
                }
                else if (richting.X < 0) // Naar links
                {
                    huidigeanimatie = linksloopanimatie;
                }
            }
            huidigeanimatie.Update(gameTime);

        }

            // Teken de vijand op het scherm
            public void Draw(SpriteBatch spriteBatch)
            {
            // Zorg ervoor dat je de juiste positie gebruikt om te tekenen
            if (huidigeanimatie == rechtsloopanimatie)
            {
                huidigeTexture = looprechtstexture;
            }
            else
            {
                huidigeTexture = looplinkstexture;
            }
            spriteBatch.Draw(huidigeTexture, positie, huidigeanimatie.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
    }
