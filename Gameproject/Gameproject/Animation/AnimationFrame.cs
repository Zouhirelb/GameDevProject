using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameproject.Animation
{
    public class AnimationFrame
    {
        public Microsoft.Xna.Framework.Rectangle SourceRectangle { get; set; }
        public AnimationFrame(Microsoft.Xna.Framework.Rectangle rectangle)
        {
            SourceRectangle = rectangle;
        }
    }
}
