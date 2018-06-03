using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class Enemy
    {
        public Sprite EnemySprite;
        // Offset from where shots are fired.
        public Vector2 GunOffset = new Vector2(25, 25);
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private Vector2 currentWaypoint;
        // Speed at which enemies walk.
        private float speed = 120f;
        public bool Destroyed = false;
        // Collision radius for enemy.
        private int enemyRadius = 25;
        private Vector2 previousPosition = Vector2.Zero;

        // Constructor
        public Enemy(Texture2D texture, Vector2 position, Rectangle initialFrame, int frameCount)
        {
            // Create the sprite.
            EnemySprite = new Sprite(position, texture, initialFrame, Vector2.Zero);

            // Add the frames to the sprite.
            for (int i = 1; i < frameCount; i++)
            {
                EnemySprite.AddFrame(new Rectangle(initialFrame.X = (initialFrame.Width * i), initialFrame.Y, initialFrame.Width, initialFrame.Height));
            }

            // Update the internal position variables.
            previousPosition = position;
            currentWaypoint = position;
            // Set the collision radius for the sprite.
            EnemySprite.CollisionRadius = enemyRadius;
        }

        // Function for adding a waypoint.
        public void AddWaypoint(Vector2 waypoint)
        {
            waypoints.Enqueue(waypoint);
        }

        // Function for if waypoint has been reached.
        public bool WaypointReached()
        {
            // Check if enemy is at waypoint and if so return true.
            if (Vector2.Distance(EnemySprite.Position, currentWaypoint) < (float)EnemySprite.Source.Width / 2)
            {
                return true;
            }
            // Otherwise return false.
            else
            {
                return false;
            }
        }

        // Function to check if the enemy is active.
        public bool IsActive()
        {
            if (Destroyed)
            {
                return false;
            }

            if (waypoints.Count > 0)
            {
                return true;
            }

            if (WaypointReached())
            {
                return false;
            }
            return true;
        }

        // Update
        public void Update(GameTime gameTime)
        {
            if (IsActive())
            {
                // Create a vector2 for the heading in which the enemy will walk.
                Vector2 heading = currentWaypoint - EnemySprite.Position;
                // Normalize it.
                if (heading != Vector2.Zero)
                {
                    heading.Normalize();
                }
                // Apply the enemy's speed.
                heading *= speed;
                // Move the enemy in the heading.
                EnemySprite.Velocity = heading;
                // Update the old previous position.
                previousPosition = EnemySprite.Position;
                // Update the enemy sprite.
                EnemySprite.Update(gameTime);

                //Make enemy face its heading.
                if (heading.X < 0 && EnemySprite.Frame >= 4)
                {
                    EnemySprite.Frame = 0;
                }
                else if (heading.X > 0 && EnemySprite.Frame < 5)
                 {
                   EnemySprite.Frame = 4;
                }

                // Remove a waypoint once it has been reached as long as there are more waypoints left.
                if (WaypointReached() && waypoints.Count > 0)
                {

                        currentWaypoint = waypoints.Dequeue();
                }
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the enemy as long as it is active.
            if (IsActive())
            {
                EnemySprite.Draw(spriteBatch); 
            }
        }
    }
}
