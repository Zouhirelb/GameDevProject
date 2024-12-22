using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Enemies;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject { 


        public class Monster :Enemy, IGameObject
        {
            private Texture2D looprechtstexture;
            private Texture2D looplinkstexture;
            private Texture2D huidigeTexture;
            private Texture2D doodtexture;

            private Animatie rechtsloopanimatie; 
            private Animatie linksloopanimatie;
            private Animatie huidigeanimatie;

            private IEnemybehavior behavior;

            private Vector2 positie;  


            public Vector2 Positie { get { return positie; } set { positie = value; } } 
            public override int Breedte => 57; 
            public override int Hoogte => 46;  

            public Rectangle BoundingBox => new Rectangle(
                (int)Positie.X,
                (int)Positie.Y,
                Breedte,
                Hoogte
            );


            public Monster(Texture2D texturerechts, Texture2D texturelinks, Vector2 startPositie, IEnemybehavior behavior) : base(startPositie)
        {
                this.looprechtstexture = texturerechts;
                //this.doodtexture = doodtexure;
                this.positie = startPositie;
                this.looplinkstexture = texturelinks;
                this.behavior = behavior;

                linksloopanimatie = new Animatie();
                rechtsloopanimatie = new Animatie();

                int[] pixels = { 0, 57, 114, 171, 228 };

                foreach (var item in pixels)
                {
                    linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(item, 0, 57, 46)));
                    rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(item,0,57, 46)));
                }

            huidigeanimatie = rechtsloopanimatie;
            }

            public override void Update(GameTime gameTime, Vector2 heropositie)
            {
                behavior.Execute(this, heropositie);
                
              
                Vector2 richting = heropositie - positie;
                float afstand = richting.Length();

                    if (afstand > 1f) 
                    {
                        richting.Normalize();
                        positie += richting * 2f; 

                        if (richting.X > 0) 
                        {
                           huidigeanimatie = rechtsloopanimatie;
                        }
                        else if (richting.X < 0) 
                        {
                            huidigeanimatie = linksloopanimatie;
                        }
                    }

                huidigeanimatie.Update(gameTime);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {
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

       
    }
    }
