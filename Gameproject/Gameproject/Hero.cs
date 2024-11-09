using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Animation;
using Gameproject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject
{
    public class Hero:IGameObject
    {
        Texture2D heroTexture;
        Animatie animatie;
        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(49, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(97, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(145, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(193, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(241, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(289, 0, 48, 73)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(337, 0, 48, 73)));

        }
        public void Update() 
        {
            animatie.Update();
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(heroTexture, new Vector2(10, 10),animatie.CurrentFrame.SourceRectangle , Color.White);
        }

    }
}
