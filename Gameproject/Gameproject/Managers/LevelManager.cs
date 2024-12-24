using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comora;
using Gameproject.Enemies;
using Gameproject.Enemies.behavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Managers
{
    internal class LevelManager
    {
        Random Random;
        private Texture2D monsterrechtstexture;
        private Texture2D monsterlinkstexture;
        private Texture2D monsterDeathTexture;

        private Texture2D skeletonDeathTexture;
        private Texture2D skeletonAttackRightTexture;
        private Texture2D skeletonAttackLeftTexture;
        private Texture2D skeletonIdleTexture;
        private Texture2D skeletonLeftTexture;
        private Texture2D skeletonRightTexture;

        private Texture2D magicianDeathTexture;
        private Texture2D magicianAttackRightTexture;
        private Texture2D magicianAttackLeftTexture;
        private Texture2D FireballRightTexture;
        private Texture2D FireballLeftTexture;
        private Texture2D magicianIdleTexture;
        private Texture2D magicianLeftTexture;
        private Texture2D magicianRightTexture;
        int counter=10;
        public void Initialize(
            Texture2D monsterrechtstexture,
            Texture2D monsterlinkstexture,
            Texture2D monsterDeathTexture,
            Texture2D skeletonRightTexture,
            Texture2D skeletonLeftTexture,
            Texture2D skeletonIdleTexture,
            Texture2D skeletonDeathTexture,
            Texture2D skeletonAttackRightTexture,
            Texture2D skeletonAttackLeftTexture,
            Texture2D magicianRightTexture,
            Texture2D magicianLeftTexture,
            Texture2D magicianIdleTexture,
            Texture2D magicianDeathTexture,
            Texture2D magicianAttackRightTexture,
            Texture2D magicianAttackLeftTexture,
            Texture2D fireballRightTexture,
            Texture2D fireballLeftTexture
        )
        {
            this.monsterrechtstexture = monsterrechtstexture;
            this.monsterlinkstexture = monsterlinkstexture;
            this.monsterDeathTexture = monsterDeathTexture;

            this.skeletonRightTexture = skeletonRightTexture;
            this.skeletonLeftTexture = skeletonLeftTexture;
            this.skeletonIdleTexture = skeletonIdleTexture;
            this.skeletonDeathTexture = skeletonDeathTexture;
            this.skeletonAttackRightTexture = skeletonAttackRightTexture;
            this.skeletonAttackLeftTexture = skeletonAttackLeftTexture;

            this.magicianRightTexture = magicianRightTexture;
            this.magicianLeftTexture = magicianLeftTexture;
            this.magicianIdleTexture = magicianIdleTexture;
            this.magicianDeathTexture = magicianDeathTexture;
            this.magicianAttackRightTexture = magicianAttackRightTexture;
            this.magicianAttackLeftTexture = magicianAttackLeftTexture;

            this.FireballRightTexture = fireballRightTexture;
            this.FireballLeftTexture = fireballLeftTexture;
        }
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
        private int scoreForNextLevel = 30;
        private int scoreIncrementPerLevel = 30;

        private bool levelChangedThisFrame;
        private LevelManager()
        {
        }

        public int CurrentLevel => currentLevel;
        public void Update(GameTime gameTime)
        {
            levelChangedThisFrame = false;

            if (ScoreManager.Instance.Score >= scoreForNextLevel)
            {
                currentLevel++;
                levelChangedThisFrame = true;

                scoreForNextLevel += scoreIncrementPerLevel;

                Console.WriteLine($"Level Up! Nu level: {currentLevel} " +
                                  $"(scoreForNextLevel = {scoreForNextLevel})");

                SpawnEnemiesForLevel(currentLevel);
            }
        }

        public bool DidLevelChangeThisFrame()
        {
            return levelChangedThisFrame;
        }

        public void SpawnEnemiesForLevel(int level)
        {
            Random = new Random();

            counter += 4;
            int amountOfMonsters = level + counter;
            int amountOfSkeletons = level;
            int amountOfMagicians = Math.Max(0, level - 2);

            Console.WriteLine($"Spawning {amountOfMonsters} Monsters," +
                              $" {amountOfSkeletons} Skeletons," +
                              $" {amountOfMagicians} Magicians for Level {level}...");

            for (int i = 0; i < amountOfMonsters; i++)
            {
                 EnemyManager.Instance.AddEnemy(
                    new Monster(monsterrechtstexture, monsterlinkstexture, monsterDeathTexture, new Vector2(Random.Next(-1000, 1000), Random.Next(-1000, 1000)), new MonsterBehavior())
                );
            }
            for (int i = 0; i < amountOfSkeletons; i++)
            {
                EnemyManager.Instance.AddEnemy(
                    new Skeleton(skeletonRightTexture, skeletonLeftTexture, skeletonIdleTexture, skeletonDeathTexture, skeletonAttackRightTexture, skeletonAttackLeftTexture, new Vector2(Random.Next(-1000, 1000), Random.Next(-1000, 1000)), new SkeletonBehavior())
                );
            }
            for (int i = 0; i < amountOfMagicians; i++)
            {
                EnemyManager.Instance.AddEnemy(
                    new Magician(FireballRightTexture, FireballLeftTexture, magicianRightTexture, magicianLeftTexture, magicianIdleTexture, magicianDeathTexture, magicianAttackRightTexture, magicianAttackLeftTexture, new Vector2(Random.Next(-1000, 1000), Random.Next(-1000, 1000)), new MagicianBehavior())
                );

            }
        }
    
    }
}

