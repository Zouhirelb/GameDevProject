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
        public Vector2 Positie { get;  set; }
        private Vector2 direction;
        private float speed = 5f;
        public Texture2D textureright;
        public Texture2D textureleft;
        public Animatie animationright;
        public Animatie animationleft;
        public Animatie currentanimation;
        public Rectangle BoundingBox => new Rectangle((int)Positie.X, (int)Positie.Y, Breedte, Hoogte); //niet vergeten grote aan te passen
        public int Breedte => 64;
        public int Hoogte => 64;
        private int[] pixels = { 0, 64, 192, 256, 320,384,448,512,576,640,704 };
        public FireBall(Enemy enemy,Vector2 startPositie, Vector2 direction, Texture2D textureright,Texture2D textureleft)
        {
            if (enemy is Magician magician)
            {


            this.Positie = startPositie;
            this.direction = direction;
            this.textureleft = textureleft;
            this.textureright = textureright;

            animationright = new Animatie();
            animationleft = new Animatie();

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
            return positie.X >= 0 && positie.X < 1280 && positie.Y >= 0 && positie.Y < 736;
        }
        public void Update(GameTime gameTime)
        {
            Positie += direction * speed;
            currentanimation.Update(gameTime);

            if (!IsOnScreen(Positie))
            {
                Destroy();
            }
        }

        public void Draw(SpriteBatch spriteBatch )
        {
            var sourceRectangle = currentanimation.CurrentFrame.SourceRectangle;

            if (direction.X > 0)
            {
                

                spriteBatch.Draw(textureright, Positie, sourceRectangle, Color.White);
                
            }
            else
            {
                
                    spriteBatch.Draw(textureleft, Positie, sourceRectangle, Color.White);
                
            }

        }

       
    }
}



   
  
  

    


