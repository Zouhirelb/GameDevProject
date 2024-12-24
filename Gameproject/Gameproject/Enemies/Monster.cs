using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Enemies;
using Gameproject.Interfaces;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject { 


        public class Monster :Enemy
        {
            public Texture2D looprechtstexture;
            public Texture2D looplinkstexture;
            public Texture2D huidigeTexture;
            public Texture2D deadtexture;
            
            public Animatie deathanimation;
            public Animatie rechtsloopanimatie;
            public Animatie linksloopanimatie;
            public Animatie huidigeanimatie;
            
            IEnemybehavior behavior;
            private int health = 50; 

            public int Health
            {
                get { return health; }
                set
                {
                    health = value;
                    if (health <= 0)
                    {
                        isDead = true;
                    huidigeanimatie = deathanimation;
                    }
                }
            }

            private bool isDead;
        private float deathTimer;

        public bool IsDead => isDead;
        public Monster(Texture2D texturerechts, Texture2D texturelinks, Texture2D deadtexture, Vector2 startPositie, IEnemybehavior behavior) : base(startPositie,behavior)
            {
                this.looprechtstexture = texturerechts;
                this.deadtexture = deadtexture;
                this.looplinkstexture = texturelinks;
                this.behavior = behavior;

                deathanimation = new Animatie();
                linksloopanimatie = new Animatie();
                rechtsloopanimatie = new Animatie();

                int[] runpixels = { 0, 57, 114, 171, 228 };
                int[] Deathpixels = { 0, 65, 130, 195, 260 };

                foreach (var pixel in Deathpixels)
                {
                    deathanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 65, 57)));
                }

                foreach (var pixel in runpixels)
                {
                    linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 57, 46)));
                    rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(pixel, 0,57, 46)));
                }

                huidigeanimatie = rechtsloopanimatie;
            }
            public override int Breedte => 57; 
            public override int Hoogte => 46;

        public void TakeDamage(int damage)
        {
            Health -= damage; 
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
                spriteBatch.Draw(huidigeTexture, Positie, huidigeanimatie.CurrentFrame.SourceRectangle, Color.White);
                
            }
        public override void Update(GameTime gameTime, Vector2 heropositie)
        {
            if (!isDead)
            {
                behavior.Execute(this, heropositie, gameTime);
            }
            else
            {
                deathanimation.Update(gameTime);

                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathTimer >= 1.5f)
                {

                    EnemyManager.Instance.RemoveEnemy(this);
                    CollisionManager.Instance.UnregisterObject(this);
                }


            }

        }
        }
}
