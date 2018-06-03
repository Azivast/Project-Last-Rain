using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class ShotManager
    {
        // List of all shots.
        public List<Sprite> Shots = new List<Sprite>();
        // Bound of the screen.
        private Rectangle screenBounds;

        private Texture2D Texture;
        private Rectangle InitialFrame;
        private int FrameCount;
        private float shotSpeed;
        // Collision radius of the shot.
        private int CollisionRadius;

        // Constructor
        public ShotManager(Texture2D texture, Rectangle initialFrame, int frameCount, int collisionRadius, float shotSpeed, Rectangle screenBounds)
        {
            // Update internal variables to the ones supplied by the constructior.
            Texture = texture;
            InitialFrame = initialFrame;
            FrameCount = frameCount;
            CollisionRadius = collisionRadius;
            this.shotSpeed = shotSpeed;
            this.screenBounds = screenBounds;
        }

        // FireShot function.
        public void FireShot(Vector2 position, Vector2 velocity, bool playerFired)
        {
            // Create a sprite of the shot.
            Sprite thisShot = new Sprite(position, Texture, InitialFrame, velocity);
            // Set the sprite's velocity to shotSpeed.
            thisShot.Velocity *= shotSpeed;

            // Add frames to the sprite.
            for (int i = 1; i < FrameCount; i++)
            {
                thisShot.AddFrame(new Rectangle(
                    InitialFrame.X + (InitialFrame.Width * i),
                    InitialFrame.Y,
                    InitialFrame.Width,
                    InitialFrame.Height));
            }

            // Set the sprites's collisionRadius.
            thisShot.CollisionRadius = CollisionRadius;
            // Add the shot/sprite to the list.
            Shots.Add(thisShot);
        }

        // Update
        public void Update(GameTime gametime)
        {
            // Run for each shot.
            for (int i = Shots.Count - 1; i >= 0; i--)
            {
                // Update the shot.
                Shots[i].Update(gametime);
                // Remove the shot if it moves outside the screen.
                if (!screenBounds.Intersects(Shots[i].Destination))
                {
                    Shots.RemoveAt(i);
                }
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each shot.
            foreach (Sprite shot in Shots)
            {
                shot.Draw(spriteBatch); 
            }
        }

    }
}
