using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Gameproject.Input
{
    internal class KeyBoardReader : IinputReader
    {
        public Vector2 ReaderInput()
        {
            var direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
            {
                direction = new Vector2(-1, 0);
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction = new Vector2(1, 0);
            }
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }
            return direction;
        }
    }
}
