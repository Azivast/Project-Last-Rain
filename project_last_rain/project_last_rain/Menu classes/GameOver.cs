using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain.Menu_classes
{
    class GameOver
    {
        private Button playAgain;
        private Button mainMenu;
        private Button quit;
        private Sprite gameoverSprite;

        // Constructor
        public GameOver(Texture2D Texture)
        {
            // Create the different buttons at their respective position.
            playAgain = new Button(Texture, new Vector2(660, 600), new Rectangle(0, 441, 600, 45), 2);
            mainMenu = new Button(Texture, new Vector2(660, 650), new Rectangle(0, 349, 600, 45), 2);
            quit = new Button(Texture, new Vector2(660, 700), new Rectangle(0, 138, 600, 45), 2);

            // Create the game over sprite.
            gameoverSprite = new Sprite(new Vector2(640, 400), Texture, new Rectangle(0, 487, 640, 63), Vector2.Zero);
        }

        // Uppdate
        public void Update(GameTime gameTime)
        {
            // Update playAgain button.
            playAgain.Update(gameTime);
            // Change to gamestate PrePlaying if playAgain button is pressed.
            if (playAgain.IsPressed())
            {
                Game1.gameState = Game1.GameState.PrePlaying;
            }

            // Update mainMenu button.
            mainMenu.Update(gameTime);
            // Change to gamestate titlescreen if mainMenu button is pressed.
            if (mainMenu.IsPressed())
            {
                Game1.gameState = Game1.GameState.TitleScreen;
            }

            // Update quit button.
            quit.Update(gameTime);
            // Quit the game if exit button is pressed.
            if (quit.IsPressed())
            {
                Game1.ExitGame = true;
            }

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {;
            // Draw each button.
            playAgain.Draw(spriteBatch);
            mainMenu.Draw(spriteBatch);
            quit.Draw(spriteBatch);
            // Draw the game over sprite.
            gameoverSprite.Draw(spriteBatch);
            // Draw the scores.
            spriteBatch.DrawString(Game1.Font, "SCORE THIS ROUND: " + ((int)ScoreManager.CurrentScore).ToString(), new Vector2(700, 510), Color.White);
            spriteBatch.DrawString(Game1.Font, "BEST SCORE: " + ((int)ScoreManager.BestScore).ToString(), new Vector2(780, 550), Color.White);
        }
    }
}
