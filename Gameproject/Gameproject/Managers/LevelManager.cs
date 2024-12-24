using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject.Managers
{
    internal class LevelManager
    {
        private static LevelManager instance;
        public static LevelManager Instance
        {
            get
            {
                if (instance == null) instance = new LevelManager();
                return instance;
            }
        }

        private int currentLevel = 1;
        private int scoreForNextLevel = 50;
        private int scoreIncrementPerLevel = 50;

        private bool levelChangedThisFrame; 
        private LevelManager()
        {
        }

        public int CurrentLevel => currentLevel;
    }
}
