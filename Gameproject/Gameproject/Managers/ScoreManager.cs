using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject.Managers
{
    public class ScoreManager
    {
        private static ScoreManager instance;
        public static ScoreManager Instance
        {
            get
            {
                if (instance == null) instance = new ScoreManager();
                return instance;
            }
        }
        private int score;
        private int level;
        public  int ScoreValue { get; }

        private ScoreManager() { }

        public int Score
        {
            get
            {
                return score;
            }
          
        }

        public int Level
        {
            get
            {
                return level;
            }
        }
        public void AwardScore()
        {
            ScoreManager.Instance.AddScore(ScoreValue);
        }
        public void reset()
        {
            score = 0;
            CheckLevelUp();
        }

        public void AddScore(int amount)
        {
            score += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if (score >= (level + 1) * 50)
            {
                level++;
            }
        }
    }
}
