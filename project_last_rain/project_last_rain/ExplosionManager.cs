using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class ExplosionManager
    {
        // Variables
        private Texture2D texture;
        private Rectangle characterRectangle;
        private Rectangle characterRectangle2;
        private Rectangle pointRectangle;

        // Number of pieces (the main part/texture of the explosion).
        private int pieceCount = 1;
        // Number of points (the smaller particles of the explosion.)
        private int minPointCount = 20;
        private int maxPointCount = 30;

        // For how long the explosiion lasts.
        private int durationCount = 60;
        // The explosions speed.
        private float explosionMaxSpeed = 100f;

        // The speed of the pieces.
        private float pieceSpeedScale = 1f;
        private int pointSpeedMin = 24;
        private int pointSpeedMax = 48;

        // Color the explosion starts with.
        private Color initialColor = new Color(1.0f, 1.0f, 1.0f) * 0.5f;
        // Color the explosion fades to.
        private Color finalColor = new Color(0f, 0f, 0f, 0f);

        Random rand = new Random();
        // List of all particles
        private List<Particle> ExplosionParticles = new List<Particle>();

        // Constructor
        public ExplosionManager(
            Texture2D texture, 
            Rectangle characterRectangle,
            Rectangle characterRectangle2,
            int pieceCount,
            float pieceSpeedScale,
            Rectangle pointRectangle,
            int minPointCount,
            int maxPointCount,
            int pointSpeedMin,
            int pointSpeedMax)
        {
            // Update all internal variables to the ones supplied by the constructor.
            this.texture = texture;
            this.characterRectangle = characterRectangle;
            this.characterRectangle2 = characterRectangle2;
            this.pieceSpeedScale = pieceSpeedScale;
            this.pieceCount = pieceCount;
            this.pointRectangle = pointRectangle;
            this.minPointCount = minPointCount;
            this.maxPointCount = maxPointCount;
            this.pointSpeedMin = pointSpeedMin;
            this.pointSpeedMax = pointSpeedMax;
        }

        // Function for throwing  particles in random direction.
        public Vector2 RandomDirection(float scale)
        {
            // Create vector2 with a random direction.
            Vector2 direction;
            do
            {
                direction = new Vector2(
                    rand.Next(0, 101) - 50,
                    rand.Next(0, 101) - 50);
            } while (direction.Length() == 0);
            // Normalize it.
            direction.Normalize();
            // Set the speed.
            direction *= scale;
            // Return the vector2.
            return direction;
        }

        // Function for starting an explosion-
        public void AddExplosion(Vector2 location, Vector2 momentum, bool mirrored)
        {
            // Set the piece from which pieces spawn.
            Vector2 pieceLocation = location -
                new Vector2(characterRectangle.Width / 2,
                    characterRectangle.Height / 2);

            // Add the piece corresponding to if it should be mirrored or not.
            if (!mirrored)
            {
                ExplosionParticles.Add(new Particle(
                    pieceLocation,
                    texture,

                    characterRectangle,

                    RandomDirection(pieceSpeedScale) + momentum,
                    Vector2.Zero,
                    explosionMaxSpeed,
                    durationCount,
                    initialColor,
                    finalColor));
            }
            else
            {
                ExplosionParticles.Add(new Particle(
                    pieceLocation,
                    texture,
                    characterRectangle2,
                    RandomDirection(pieceSpeedScale) + momentum,
                    Vector2.Zero,
                    explosionMaxSpeed,
                    durationCount,
                    initialColor,
                    finalColor));
            }

            // Randomize the amount of points to spawn between the specified min-max numbers.
            int points = rand.Next(minPointCount, maxPointCount + 1);
            // Spawn each point untill the randomized number is reached.
            for (int i = 0; i < points; i++)
            {
                ExplosionParticles.Add(new Particle(
                    location,
                    texture,
                    pointRectangle,
                    RandomDirection((float)rand.Next(pointSpeedMin, pointSpeedMax)) + momentum,
                    Vector2.Zero,
                    explosionMaxSpeed,
                    durationCount,
                    initialColor,
                    finalColor)); 
            }
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Run for all the particles in ExplosionParticles.
            for (int i = ExplosionParticles.Count - 1; i >= 0; i--)
            {
                // Check if a particle is active and if so update it.
                if (ExplosionParticles[i].IsActive)
                {
                    ExplosionParticles[i].Update(gameTime);
                }
                // Otherwise remove it.
                else
                {
                    ExplosionParticles.RemoveAt(i);
                }
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each particle.
            foreach (Particle particle in ExplosionParticles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
