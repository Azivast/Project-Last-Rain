using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class ScoreManager
    {
        public static float CurrentScore;
        public static float BestScore;

        // Constructor (empty since nothing is needed to create an instance of ScoreManager.)
        public ScoreManager(){}

        // Function to current reset score.
        public void ResetScore()
        {
            CurrentScore = 0;
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Increase the score every second.
            CurrentScore += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update best score if currentscore becomes bigger.
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw 2 strings showing the score.
            spriteBatch.DrawString(Game1.Font, "Current Score: " + ((int)CurrentScore).ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Game1.Font, "Best Score: " + ((int)BestScore).ToString(), new Vector2(10, 50), Color.White);
        }
    }
}
