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
        private static HealthManager instance;
        public static HealthManager Instance
        {
            get
            {
                if (instance == null) instance = new HealthManager();
                return instance;
            }
        }

        private HealthManager()
        {
            
        }


        public void ApplyDamage(IHealth target, int damage)
        {
            target.TakeDamage(damage);
        }
    }
}
