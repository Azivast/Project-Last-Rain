using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class Ally
    {
        public Sprite HogSprite;
        public Sprite AllySprite;
        // Positions of the sprites.
        private static Vector2 hogPosition = new Vector2(770, 458);
        private static Vector2 allyPosition = hogPosition +  new Vector2(137, 60);
        // Collision Radius for ally.
        private int collisionRadius = 50;

        // Constructor
        public Ally(Texture2D texture, Rectangle initialFrameHog, Rectangle initialFrameAlly)
        {
            // Create the hog sprite.
            HogSprite = new Sprite(hogPosition, texture, initialFrameHog, Vector2.Zero);
            // Create the ally sprite and update its collision radius.
            AllySprite = new Sprite(allyPosition, texture, initialFrameAlly, Vector2.Zero) {CollisionRadius = collisionRadius};
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the sprites.
            HogSprite.Draw(spriteBatch);
            AllySprite.Draw(spriteBatch);
        }
    }
}
