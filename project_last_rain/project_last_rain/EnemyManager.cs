using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class EnemyManager
    {
        // Variables
        private Texture2D texture;
        private Rectangle initialFrame;
        private int frameCount;

        public List<Enemy> Enemies = new List<Enemy>();

        public ShotManager EnemyShotManager;
        private PlayerManager playerManager;

        public int MinShipsPerWave = 1;
        public int MaxShipsPerWave = 2;
        private float nextWaveTimer = 0.0f;
        public float nextWaveMinTimer = 2.0f;
        public float nextWaveMinTimerMax = 2.0f;
        private float shipSpawnTimer = 0.0f;
        private float shipSpawnWaitTime = 1f;
        private float shipShotChance = 0.05f;
        private List<List<Vector2>> pathWaypoints = new List<List<Vector2>>();
        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();
        public bool Active = true;
        private Random rand = new Random();
        private SoundEffect firingSound;

        // Create waypoints
        private void SetUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(1940, 400));
            path0.Add(new Vector2(832, 522));
            pathWaypoints.Add(path0);
            waveSpawns[0] = 0;

            List<Vector2> path1 = new List<Vector2>();
            path1.Add(new Vector2(1950, 600));
            path1.Add(new Vector2(832, 522));
            pathWaypoints.Add(path1);
            waveSpawns[1] = 0;

            List<Vector2> path2 = new List<Vector2>();
            path2.Add(new Vector2(1950, 800));
            path2.Add(new Vector2(832, 522));
            pathWaypoints.Add(path2);
            waveSpawns[2] = 0;

            List<Vector2> path3 = new List<Vector2>();
            path3.Add(new Vector2(1950, 1000));
            path3.Add(new Vector2(832, 522));
            pathWaypoints.Add(path3);
            waveSpawns[3] = 0;

            List<Vector2> path4 = new List<Vector2>();
            path4.Add(new Vector2(-80, 400));
            path4.Add(new Vector2(960, 540));
            pathWaypoints.Add(path4);
            waveSpawns[4] = 0;

            List<Vector2> path5 = new List<Vector2>();
            path5.Add(new Vector2(-80, 600));
            path5.Add(new Vector2(960, 540));
            pathWaypoints.Add(path5);
            waveSpawns[5] = 0;

            List<Vector2> path6 = new List<Vector2>();
            path6.Add(new Vector2(-80, 800));
            path6.Add(new Vector2(960, 540));
            pathWaypoints.Add(path6);
            waveSpawns[6] = 0;

            List<Vector2> path7 = new List<Vector2>();
            path7.Add(new Vector2(-80, 1000));
            path7.Add(new Vector2(960, 540));
            pathWaypoints.Add(path7);
            waveSpawns[7] = 0;
        }

        // Constructor for EnemyManager.
        public EnemyManager(Texture2D texture, Rectangle initialFrame, int frameCount, PlayerManager playerSprite, Rectangle screenBounds, SoundEffect firingSound)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.frameCount = frameCount;
            this.playerManager = playerSprite;
            this.firingSound = firingSound;

            EnemyShotManager = new ShotManager(texture, new Rectangle(0, 494, 21, 21), 3, 9, 250f, screenBounds);
            SetUpWaypoints();
        }

        // Function for spawning enemies.
        public void SpawnEnemy(int path)
        {
            Enemy thisEnemy = new Enemy(texture, pathWaypoints[path][0], initialFrame, frameCount);
            for (int i = 0; i < pathWaypoints[path].Count(); i++)
            {
                thisEnemy.AddWaypoint(pathWaypoints[path][i]);
            }
            Enemies.Add(thisEnemy);
        }

        // Function for removing all enemies and shots on screen;
        public void Reset()
        {
            Enemies.Clear();
            nextWaveTimer = 0.0f;
            nextWaveMinTimer = nextWaveMinTimerMax;
            EnemyShotManager.Shots.Clear();
        }

        // Function for spawning waves
        public void SpawnWave(int waveType)
        {
            waveSpawns[waveType] += rand.Next(MinShipsPerWave, MaxShipsPerWave + 1);
        }

        // Update wavespawns
        private void updateWaveSpawns(GameTime gameTime)
        {
            shipSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            nextWaveMinTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.01f;
            if (shipSpawnTimer > shipSpawnWaitTime)
            {
                for (int i = waveSpawns.Count - 1; i >= 0; i--)
                {
                    if (waveSpawns[i] > 0)
                    {
                        waveSpawns[i]--;
                        SpawnEnemy(i);
                    }
                }
                shipSpawnTimer = 0.0f;
            }
            nextWaveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (nextWaveTimer > nextWaveMinTimer)
            {
                SpawnWave(rand.Next(0, pathWaypoints.Count));
                nextWaveTimer = 0f;
            }
        }

        // Update
        public void Update(GameTime gameTime)
        {
            EnemyShotManager.Update(gameTime);

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);
                if (Enemies[i].IsActive() == false)
                {
                    Enemies.RemoveAt(i);
                }

                else
                {
                    // Make a random chance that a shot is fired.
                    if ((float)rand.Next(0, 1000) / 10 <= shipShotChance)
                    {
                        // Location from which shot should be fired.
                        Vector2 fireLoc = Enemies[i].EnemySprite.Position;
                        fireLoc += Enemies[i].GunOffset;

                        //  Direction in which shot whould be fired.
                        Vector2 shotDirection = playerManager.Center - fireLoc;
                        shotDirection.Normalize();

                        // Fire shot.
                        EnemyShotManager.FireShot(fireLoc, shotDirection, false);
                        // Play firing sound.
                        firingSound.Play();
                    }
                }
            }

            if (Active)
            {
                updateWaveSpawns(gameTime);
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyShotManager.Draw(spriteBatch);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
