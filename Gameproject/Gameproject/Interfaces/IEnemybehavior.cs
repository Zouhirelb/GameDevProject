using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Enemies;
using Microsoft.Xna.Framework;

namespace Gameproject.Interfaces
{
    public interface IEnemybehavior<T> where T : Enemy
    {
            void Execute(T enemy, Vector2 heroPositie, GameTime gameTime);
    }
}
