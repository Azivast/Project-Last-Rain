using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class Particle : Sprite
    {
        // Particle's acceleration.
        private Vector2 acceleration;
        // Particle's maximum speed.
        private float maxSpeed;
        // Duration at start.
        private int initialDuration;
        // Duration left/remaining.
        private int remainingDuration;
        // Color the particles starts with.
        private Color initialColor;
        // Color the particle fades to.
        private Color finalColor;

        // Time that has elapsed since particle's spawn.
        public int ElapsedDuration
        {
            get { return initialDuration - remainingDuration; }
        }

        // How much of the total time has progressed.
        public float DurationProgress
        {
            get { return (float)ElapsedDuration / (float)initialDuration; }
        }

        // If the particle is still active (if there is time left.)
        public bool IsActive
        {
            get { return (remainingDuration > 0); }
        }

        // Constructor
        public Particle(
            Vector2 location, 
            Texture2D texture, 
            Rectangle initialFrame, 
            Vector2 velocity, 
            Vector2 acceleration, 
            float maxSpeed, 
            int duration, 
            Color initialColor, 
            Color finalColor) 
            : base(location, texture, initialFrame, velocity)
        {
            // Update internal variables to the ones supplied by the constructor.
            initialDuration = duration;
            remainingDuration = duration;
            this.acceleration = acceleration;
            this.initialColor = initialColor;
            this.maxSpeed = maxSpeed;
            this.finalColor = finalColor;
        }

        // Update.
        public override void Update(GameTime gameTime)
        {
            // Check if the particle is active.
            if (IsActive)
            {
                // Increase the particle's speed according to the acceleration.
                velocity += acceleration;
                // Check if the particle's velocity is bigger the maximum allowed speed and if so set it to max speed.
                if (velocity.Length() > maxSpeed)
                {
                    velocity.Normalize();
                    velocity *= maxSpeed;
                }
                // Fade color from initialColor to finalColor.
                TintColor = Color.Lerp(initialColor, finalColor, DurationProgress);
                // Update the particle's remaining time.
                remainingDuration--;
                base.Update(gameTime);
            }
        }

        // Draw.
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the particle if it is active.
            if (IsActive)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
