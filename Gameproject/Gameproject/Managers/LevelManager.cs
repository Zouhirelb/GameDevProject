using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Comora;
using Gameproject.Enemies;
using Gameproject.Enemies.behavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Gameproject.Managers.GameStateManager;

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

        int worldWidth = 2560;
        int worldHeight = 1472;


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
        private List<Wave> waves;

        private int currentWaveIndex = 0;
        public List<Wave> Waves => waves;
        public void InitializeWaves()
        {
            waves = new List<Wave>()
            {
                new Wave(0, 0, 5),
                new Wave(6, 2, 0),
                new Wave(8, 4, 0),
                new Wave(9, 6, 3),
                new Wave(1, 25, 5),
                new Wave(10, 24, 14),
                new Wave(13, 13, 15),
                new Wave(15, 13, 18),
                new Wave(22, 12, 12)
             };
        }

        private int enemiesAliveThisWave = 0;
        private bool waveSpawning = false;

        private int currentLevel = 1;
        

        private bool levelChangedThisFrame;
        private LevelManager()
        {
        }

        public int CurrentLevel => currentLevel;
        private void SpawnWave(int waveIndex)
        {
            if (waveIndex >= Waves.Count) return;

            Random rnd = new Random();
            var wave = Waves[waveIndex];
            currentLevel = currentWaveIndex;
            Console.WriteLine($"Spawning wave #{waveIndex + 1}: " +
                              $"{wave.Monsters} Monsters, {wave.Skeletons} Skeletons, {wave.Magicians} Magicians");

            enemiesAliveThisWave = wave.Monsters + wave.Skeletons + wave.Magicians;

            // Monsters
            for (int i = 0; i < wave.Monsters; i++)
            {
                var monster = new Monster(monsterrechtstexture, monsterlinkstexture, monsterDeathTexture,
                            new Vector2(rnd.Next(0,worldWidth), rnd.Next(0,worldHeight)),
                            new MonsterBehavior());

                EnemyManager.Instance.AddEnemy(monster);
                CollisionManager.Instance.RegisterObject(monster);
            }

            // Skeletons
            for (int i = 0; i < wave.Skeletons; i++)
            {

               var skeleton = new Skeleton(skeletonRightTexture, skeletonLeftTexture, skeletonIdleTexture, skeletonDeathTexture,
                             skeletonAttackRightTexture, skeletonAttackLeftTexture,
                             new Vector2(rnd.Next(0,worldWidth), rnd.Next(0, worldHeight)),
                             new SkeletonBehavior());
               EnemyManager.Instance.AddEnemy(skeleton );
                CollisionManager.Instance.RegisterObject(skeleton);
            }

            // Magicians
            for (int i = 0; i < wave.Magicians; i++)
            {

                var magician = new Magician(FireballRightTexture, FireballLeftTexture,
                              magicianRightTexture, magicianLeftTexture,
                              magicianIdleTexture, magicianDeathTexture,
                              magicianAttackRightTexture, magicianAttackLeftTexture,
                              new Vector2(rnd.Next(0, worldWidth), rnd.Next(0, worldHeight)),
                              new MagicianBehavior());
                EnemyManager.Instance.AddEnemy(magician);
                CollisionManager.Instance.RegisterObject(magician);
            }

            waveSpawning = false;
        }
        public void NotifyEnemyDied()
        {
            if (enemiesAliveThisWave > 0)
            {
                enemiesAliveThisWave--;
            }
            if (enemiesAliveThisWave <= 0)
            {
                currentWaveIndex++;
                Console.WriteLine($"Wave {currentWaveIndex} completed!");
            }
        }
        public void Reset()
        {
            currentWaveIndex = 0;
            enemiesAliveThisWave = 0;
            waveSpawning = false;
        }

        public void Update(GameTime gameTime)
        {
            if (currentWaveIndex >= 9)
            {
                GameStateManager.CurrentState = GameState.GameOver;
                return;
            }

            if (enemiesAliveThisWave <= 0 && !waveSpawning)
            {
                waveSpawning = true;
                SpawnWave(currentWaveIndex);
            }
          
        }
    }

}

