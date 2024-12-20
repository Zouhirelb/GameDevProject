using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameproject.Interfaces;

namespace Gameproject.Managers
{
    public class HealthManager
    {
        public void ApplyDamage(IHealth target, int damage)
        {
            target.TakeDamage(damage);
        }
    }
}
