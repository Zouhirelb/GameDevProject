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

        private int currentWaveIndex = 0;
        private List<Wave> waves;
        public void InitializeWaves()
        {
            waves = new List<Wave>()
            {
                new Wave(5, 0, 0),
                new Wave(6, 2, 0),
                new Wave(8, 4, 1),
                new Wave(0, 6, 3),
                new Wave(0, 5, 5),
                new Wave(0, 4, 4),
                new Wave(0, 3, 5),
                new Wave(0, 3, 8),
                new Wave(0, 0, 7)
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
            if (waveIndex >= waves.Count) return;

            Random rnd = new Random();
            var wave = waves[waveIndex];
            currentLevel = currentWaveIndex+1;
            Console.WriteLine($"Spawning wave #{waveIndex + 1}: " +
                              $"{wave.Monsters} Monsters, {wave.Skeletons} Skeletons, {wave.Magicians} Magicians");

            enemiesAliveThisWave = wave.Monsters + wave.Skeletons + wave.Magicians;

            // Monsters
            for (int i = 0; i < wave.Monsters; i++)
            {
                var monster = new Monster(monsterrechtstexture, monsterlinkstexture, monsterDeathTexture,
                            new Vector2(rnd.Next(-1000, 1000), rnd.Next(-1000, 1000)),
                            new MonsterBehavior());

                EnemyManager.Instance.AddEnemy(monster);
                EnemyManager.Instance.AddEnemy(monster);
                CollisionManager.Instance.RegisterObject(monster);
            }

            // Skeletons
            for (int i = 0; i < wave.Skeletons; i++)
            {

               var skeleton = new Skeleton(skeletonRightTexture, skeletonLeftTexture, skeletonIdleTexture, skeletonDeathTexture,
                             skeletonAttackRightTexture, skeletonAttackLeftTexture,
                             new Vector2(rnd.Next(-1000, 1000), rnd.Next(-1000, 1000)),
                             new SkeletonBehavior());
               EnemyManager.Instance.AddEnemy(skeleton );
                EnemyManager.Instance.AddEnemy(skeleton);
                CollisionManager.Instance.RegisterObject(skeleton);
            }

            // Magicians
            for (int i = 0; i < wave.Magicians; i++)
            {

                var magician = new Magician(FireballRightTexture, FireballLeftTexture,
                              magicianRightTexture, magicianLeftTexture,
                              magicianIdleTexture, magicianDeathTexture,
                              magicianAttackRightTexture, magicianAttackLeftTexture,
                              new Vector2(rnd.Next(-1000, 1000), rnd.Next(-1000, 1000)),
                              new MagicianBehavior());
                EnemyManager.Instance.AddEnemy(magician);
                EnemyManager.Instance.AddEnemy(magician);
                CollisionManager.Instance.RegisterObject(magician);
            }

            waveSpawning = false;
        }
        public void NotifyEnemyDied()
        {
            enemiesAliveThisWave--;
            Console.WriteLine($"Enemy died. enemiesAliveThisWave: {enemiesAliveThisWave}");
           // Als je wilt checken of wave is cleard, doe je hier:
            if (enemiesAliveThisWave <= 0)
            {
                // wave is klaar, wave++ doe je NIET meteen hier
                // maar kan net zo goed hier
                currentWaveIndex++;
                Console.WriteLine($"Wave {currentWaveIndex} completed!");
            }
        }
        public void Update(GameTime gameTime)
        {
            // Als we alle waves hebben gehad => stop
            if (currentWaveIndex >= waves.Count)
            {
                // Hier kun je zeggen "finale" of "geen vijanden meer".
                return;
            }

            // Check of we nog bezig zijn met een wave
            if (enemiesAliveThisWave <= 0 && !waveSpawning)
            {
                // We beginnen de wave
                waveSpawning = true;
                SpawnWave(currentWaveIndex);
            }

          
        }
    }

}

