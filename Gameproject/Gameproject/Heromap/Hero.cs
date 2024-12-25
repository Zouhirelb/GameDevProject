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

namespace Gameproject.Heromap
{
    public class Hero : IGameObject, IHealth
    {
        private Texture2D runrighttexture;
        private Texture2D idletexture;
        private Texture2D Currenttexture;
        private Texture2D runlefttexture;
        private Texture2D heroAttacklefttexture;
        private Texture2D heroAttackrighttexture;

        private Animations heroAttackleftAnimation;
        private Animations heroAttackrightAnimation;
        private Animations runrightanimation;
        private Animations idleanimation;
        private Animations currentanimation;
        private Animations runleftanimation;

        private int[] pixels = { 0, 49, 97, 145, 193, 241, 289, 337 };
        private int[] attackdifpixels = { 50, 50, 50, 75, 75, 75 };
        private int counterAP;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 speed;
        private Vector2 accelaration;
        private Vector2 mousevector;

        IinputReader inputReader;


        private bool faceLeft;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle BoundingBox => new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Width,
            Height
        );

        public int Health { get; set; }

        public bool IsDead => throw new NotImplementedException();

        public Hero(Texture2D textureleft, Texture2D textureright, Texture2D idletexture, Texture2D heroAttacklefttexture, Texture2D heroAttackrighttexture, IinputReader reader)
        {
            this.idletexture = idletexture;
            runrighttexture = textureright;
            runlefttexture = textureleft;
            this.heroAttackrighttexture = heroAttackrighttexture;
            this.heroAttacklefttexture = heroAttacklefttexture;
            Width = this.idletexture.Width;
            Height = this.idletexture.Height;


            inputReader = reader;
            position = new Vector2(10, 10);

            idleanimation = new Animations();
            runrightanimation = new Animations();
            runleftanimation = new Animations();
            heroAttackleftAnimation = new Animations();
            heroAttackrightAnimation = new Animations();

            idleanimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 39, 39)));

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
                runrightanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 48, 50)));
                runleftanimation.AddFrame(new AnimationFrame(new Rectangle(pixel, 0, 48, 50)));
            }

            speed = new Vector2(1, 1);
            accelaration = new Vector2(0.1f, 0.1f);

            currentanimation = idleanimation;

            Health = 10000;

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
                currentanimation = idleanimation;
            }
            else
            {
                if (directie.X > 0)
                {
                    currentanimation = runrightanimation;
                    faceLeft = false;
                }
                else if (directie.X < 0)
                {
                    currentanimation = runleftanimation;
                    faceLeft = true;
                }
                else if (directie.Y != 0)
                {
                    currentanimation = faceLeft ? runleftanimation : runrightanimation;
                }
                directie *= 4;
                position += directie;
            }


            if (inputReader is KeyBoardReader kbReader && kbReader.Attackpressed)
            {
                Attack();
            }

            currentanimation.Update(gameTime);
        }


        private void Move(Vector2 mouse)
        {

            var direction = Vector2.Add(mouse, -position);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 1f);
            position += direction;
            speed += accelaration;
            speed = Limit(speed, 2);

            float tmp = speed.Length();

            if (position.X > 600 || position.X < 0)
            {
                speed.X *= -1;
                accelaration.X *= -1;

            }
            if (position.Y > 400 || position.Y < 0)
            {
                speed.Y *= -1;
                accelaration *= 1;
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
                currentanimation = heroAttackleftAnimation;
            }
            else
            {
                currentanimation = heroAttackrightAnimation;
            }

            DoDamageToEnemiesInRange(50f, 10);
        }
        private void DoDamageToEnemiesInRange(float range, int damage)
        {
            var enemies = EnemyManager.Instance.GetEnemies();

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(Position, enemy.Position);

                if (dist <= range)
                {
                    if (faceLeft && enemy.Position.X < Position.X)
                    {
                        if (enemy is IHealth healthEnemy)
                        {
                            healthEnemy.TakeDamage(damage);
                        }
                    }
                    else if (!faceLeft && enemy.Position.X > Position.X)
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
            Health = 10000;
            Position = new Vector2(10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentanimation == idleanimation)
            {
                Currenttexture = idletexture;
            }
            else if (currentanimation == runrightanimation)
            {
                Currenttexture = runrighttexture;
            }
            else if (currentanimation == runleftanimation)
            {
                Currenttexture = runlefttexture;
            }
            else if (currentanimation == heroAttackleftAnimation)
            {
                Currenttexture = heroAttacklefttexture;
            }
            else if (currentanimation == heroAttackrightAnimation)
            {
                Currenttexture = heroAttackrighttexture;
            }

            spriteBatch.Draw(
                Currenttexture,
                position,
                currentanimation.CurrentFrame.SourceRectangle,
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
