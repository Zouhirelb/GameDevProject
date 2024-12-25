using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Gameproject.Interfaces;
using Gameproject.Animation;
using System.Security.AccessControl;
using System.Globalization;
using Gameproject.Managers;

namespace Gameproject.Enemies
{
    public class FireBall:IGameObject
    {
        public Vector2 Position { get;  set; }
        private Vector2 direction;
        private float speed = 5f;

        public Texture2D textureright;
        public Texture2D textureleft;
        public Animation.Animations animationright;
        public Animation.Animations animationleft;
        public Animation.Animations currentanimation;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height); 
        public int Width => 64;
        public int Height => 64;
        private int[] pixels = { 0, 64, 192, 256, 320,384,448,512,576,640,704 };
        public FireBall(Enemy enemy,Vector2 startPosition, Vector2 direction, Texture2D textureright,Texture2D textureleft)
        {
            if (enemy is Magician magician)
            {

            this.Position = startPosition;
            this.direction = direction;
            this.textureleft = textureleft;
            this.textureright = textureright;

                animationright = new Animation.Animations();
                animationleft = new Animation.Animations();

            this.direction.Normalize();
            
            for (int i = 0; i < pixels.Length; i++)
            {
                animationright.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 64, 64)));
                animationleft.AddFrame(new AnimationFrame(new Rectangle(pixels[i], 0, 64, 64)));
                

            }

                currentanimation = animationright;

                if (magician.CurrentAnimation == magician.AttackLeftAnimation)
                {
                    currentanimation = animationleft;
                }
                else if (magician.CurrentAnimation == magician.AttackRightAnimation)
                {
                    currentanimation = animationright;
                }

            }
        }
        public void Destroy()
        {
            FireballManager.GetInstance().RemoveFireball(this);

        }
        private bool IsOnScreen(Vector2 positie)
        {
            return positie.X >= 0 && positie.X < 11000 && positie.Y >= 0 && positie.Y < 10000;
        }
        public void Update(GameTime gameTime)
        {
            Position += direction * speed;
            currentanimation.Update(gameTime);

            if (!IsOnScreen(Position))
            {
                Destroy();
            }
        }

        public void Draw(SpriteBatch spriteBatch )
        {
            var sourceRectangle = currentanimation.CurrentFrame.SourceRectangle;

            if (direction.X > 0)
            {
                

                spriteBatch.Draw(textureright, Position, sourceRectangle, Color.White);
                
            }
            else
            {
                
                    spriteBatch.Draw(textureleft, Position, sourceRectangle, Color.White);
                
            }

        }

       
    }
}



   
  
  

    


