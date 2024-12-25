using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Input;
using Gameproject.Interfaces;
using Gameproject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject
{
    public class Hero : IGameObject, IHealth
    {
        private Texture2D heldlooprechtstexture;
        private Texture2D heldstiltexture;
        private Texture2D huidigetexture;
        private Texture2D heldlinksllopentexture;
        private Texture2D heroAttacklefttexture;
        private Texture2D heroAttackrighttexture;

        private Animatie heroAttackleftAnimation;
        private Animatie heroAttackrightAnimation;
        private Animatie rechtsloopanimatie;
        private Animatie stilanimatie;
        private Animatie huidigeanimatie;
        private Animatie linksloopanimatie;

        private int[] pixels = { 0, 49, 97, 145, 193, 241, 289, 337 };
        private int[] attackdifpixels = { 50, 50, 50, 75, 75, 75 };
        private int counterAP;
        private Vector2 positie;

        public Vector2 Positie
        {
            get { return positie; }
            set { positie = value; }
        }

        private Vector2 snelheid;
        private Vector2 versnelling;
        private Vector2 mousevector;

        IinputReader inputReader;


        private bool faceLeft;
        public int Breedte { get; private set; }
        public int Hoogte { get; private set; }

        public Rectangle BoundingBox => new Rectangle(
            (int)Positie.X,
            (int)Positie.Y,
            Breedte,
            Hoogte
        );

        public int Health { get; set; }

        public bool IsDead => throw new NotImplementedException();

        public Hero(Texture2D texturelinks, Texture2D texturerechts, Texture2D idletexture, Texture2D heroAttacklefttexture, Texture2D heroAttackrighttexture, IinputReader reader)
        {
            heldstiltexture = idletexture;
            heldlooprechtstexture = texturerechts;
            heldlinksllopentexture = texturelinks;
            this.heroAttackrighttexture = heroAttackrighttexture;
            this.heroAttacklefttexture = heroAttacklefttexture;
            Breedte = heldstiltexture.Width;
            Hoogte = heldstiltexture.Height;


            inputReader = reader;
            positie = new Vector2(10, 10);

            stilanimatie = new Animatie();
            rechtsloopanimatie = new Animatie();
            linksloopanimatie = new Animatie();
            heroAttackleftAnimation = new Animatie();
            heroAttackrightAnimation = new Animatie();

            stilanimatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 39, 39)));

            for (int i = attackdifpixels.Length - 1; i >= 0; i--)
            {
                heroAttackleftAnimation.AddFrame(new AnimationFrame(new Rectangle(counterAP, 0, attackdifpixels[i], 50)));
                counterAP += attackdifpixels[i];

            }
            counterAP = 0;

            for (int i = 0; i < attackdifpixels.Length; i++)
            {
                heroAttackrightAnimation.AddFrame(new AnimationFrame(new Rectangle(counterAP, 0, attackdifpixels[i], 50)));
                counterAP += attackdifpixels[i];
            }

            foreach (var pixel in pixels)
            {
                rechtsloopanimatie.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 48, 50)));
                linksloopanimatie.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 48, 50)));
            }

            snelheid = new Vector2(1, 1);
            versnelling = new Vector2(0.1f, 0.1f);

            huidigeanimatie = stilanimatie;

            Health = 1000;


        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        private int lastHorizontalDirection = 0;

        public void Update(GameTime gameTime)
        {
            var directie = inputReader.ReaderInput();

            if (directie == Vector2.Zero)
            {
                huidigeanimatie = stilanimatie;
            }
            else
            {
                if (directie.X > 0)
                {
                    huidigeanimatie = rechtsloopanimatie;
                    faceLeft = false;
                }
                else if (directie.X < 0)
                {
                    huidigeanimatie = linksloopanimatie;
                    faceLeft = true;
                }
                else if (directie.Y != 0)
                {
                    huidigeanimatie = faceLeft ? linksloopanimatie : rechtsloopanimatie;
                }
                directie *= 4;
                positie += directie;
            }


            if (inputReader is KeyBoardReader kbReader && kbReader.Attackpressed)
            {
                Attack();
            }

            huidigeanimatie.Update(gameTime);
        }


        private void Move(Vector2 mouse)
        {

            var directie = Vector2.Add(mouse, -positie);
            directie.Normalize();
            directie = Vector2.Multiply(directie, 1f);
            // deze code nog refactoren
            positie += directie;
            snelheid += versnelling;
            snelheid = Limit(snelheid, 2);
            // tot hier

            float tmp = snelheid.Length();

            if (positie.X > 600 || positie.X < 0)
            {
                snelheid.X *= -1;
                versnelling.X *= -1;

            }
            if (positie.Y > 400 || positie.Y < 0)
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
        private void Attack()
        {
            if (faceLeft)
            {
                huidigeanimatie = heroAttackleftAnimation;
            }
            else
            {
                huidigeanimatie = heroAttackrightAnimation;
            }

            DoDamageToEnemiesInRange(50f, 10);
        }
        private void DoDamageToEnemiesInRange(float range, int damage)
        {
            var enemies = EnemyManager.Instance.GetEnemies();

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(Positie, enemy.Positie);

                if (dist <= range)
                {
                    if (faceLeft && enemy.Positie.X < Positie.X)
                    {
                        if (enemy is IHealth healthEnemy)
                        {
                            healthEnemy.TakeDamage(damage);
                        }
                    }
                    else if (!faceLeft && enemy.Positie.X > Positie.X)
                    {
                        if (enemy is IHealth healthEnemy)
                        {
                            healthEnemy.TakeDamage(damage);
                        }
                    }
                }
            }
        }
        public void Reset()
        {
            Health = 1000;
            Positie = new Vector2(10, 10); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (huidigeanimatie == stilanimatie)
            {
                huidigetexture = heldstiltexture;
            }
            else if (huidigeanimatie == rechtsloopanimatie)
            {
                huidigetexture = heldlooprechtstexture;
            }
            else if (huidigeanimatie == linksloopanimatie)
            {
                huidigetexture = heldlinksllopentexture;
            }
            else if (huidigeanimatie == heroAttackleftAnimation)
            {
                huidigetexture = heroAttacklefttexture;
            }
            else if (huidigeanimatie == heroAttackrightAnimation)
            {
                huidigetexture = heroAttackrighttexture;
            }

            spriteBatch.Draw(
                huidigetexture,
                positie,
                huidigeanimatie.CurrentFrame.SourceRectangle,
                Color.White,
                0,
                new Vector2(0, 0),
                1.2f,
                SpriteEffects.None,
                0
            );
        }


    }
}
