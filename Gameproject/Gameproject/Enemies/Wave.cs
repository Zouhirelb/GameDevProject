using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject.Enemies
{
    public class Wave
    {
        public int Monsters { get; set; }
        public int Skeletons { get; set; }
        public int Magicians { get; set; }

        public Wave(int monsters, int skeletons, int magicians)
        {
            Monsters = monsters;
            Skeletons = skeletons;
            Magicians = magicians;
        }
    }
}
