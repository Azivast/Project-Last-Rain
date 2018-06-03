using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain
{
    class Rain
    {
        // Create a list containing sprites.
        private List<Sprite> raindrops = new List<Sprite>();
        // Size of the screen.
        private int screenWidth;
        private int screenHeight;
        private Random rand = new Random();
        // Create an array of colors for the raindrops.
        private Color[] colors = { new Color(100, 100, 100, 50), new Color(130, 143, 163, 50) };


        // Constructor
        public Rain (int screenWidth, int screenHeight, int dropCount, Vector2 dropVelocity, Texture2D texture, Rectangle frameRectangle)
        {
            // Update internal variables for screen size.
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            // Add raindrop sprites with random colors to the list until dropcount is reached.
            for (int i = 0; i < dropCount; i++)
            {
                raindrops.Add(new Sprite(new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)), texture, frameRectangle, dropVelocity));
                Color dropColor = colors[rand.Next(0, colors.Count())];
                dropColor *= (float)(rand.Next(30, 80) / 100f); raindrops[raindrops.Count() - 1].TintColor = dropColor;
            }
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Run for all raindrops.
            foreach (Sprite drop in raindrops)
            {
                // Update the raindrop.
                drop.Update(gameTime);
                // Remove raindrops outside the screen.
                if (drop.Position.Y > screenHeight)
                {
                    drop.Position = new Vector2(rand.Next(0, screenWidth), 0);
                }
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each raindrop.
            foreach (Sprite drop in raindrops)
            {
                drop.Draw(spriteBatch);
            }
        }
    }
}
