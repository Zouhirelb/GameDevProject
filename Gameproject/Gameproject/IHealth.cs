using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject
{
    internal interface IHealth
    {
        int Health { get; set; }
        void TakeDamage(int damage);
    }
}
