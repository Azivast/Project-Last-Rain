using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain.Menu_classes
{
    class Paused
    {
        private Button resume;
        private Button mainMenu;
        private Button quit;
        private Sprite pausedSprite;

        // Constructor
        public Paused(Texture2D Texture)
        {
            // Create the different buttons at their respective position.
            resume = new Button(Texture, new Vector2(660, 600), new Rectangle(0, 303, 600, 45), 2);
            mainMenu = new Button(Texture, new Vector2(660, 650), new Rectangle(0, 349, 600, 45), 2);
            quit = new Button(Texture, new Vector2(660, 700), new Rectangle(0, 138, 600, 45), 2);

            // Create the paused sprite.
            pausedSprite = new Sprite(new Vector2(783, 400), Texture, new Rectangle(631, 184, 353, 53), Vector2.Zero);
        }

        // Uppdate
        public void Update(GameTime gameTime)
        {
            // Update the start button.
            resume.Update(gameTime);
            // Change to gamestate playing if start button is pressed.
            if (resume.IsPressed())
            {
                Game1.gameState = Game1.GameState.Playing;
            }
            // Change to gamestate playing if escape is pressed.
            KeyboardState keyboard = Keyboard.GetState();
            if (InputManager.KBState.IsKeyDown(Keys.Escape)  && InputManager.PreviousKBState.IsKeyUp(Keys.Escape))
            {
                Game1.gameState = Game1.GameState.Playing;
            }

            // Update the main menu button.
            mainMenu.Update(gameTime);
            // Change to gamestate TitleScreen if main menu button is pressed.
            if (mainMenu.IsPressed())
            {
                Game1.gameState = Game1.GameState.TitleScreen;
            }

            // Update the quit button.
            quit.Update(gameTime);
            // Quit the game if quit button is pressed.
            if (quit.IsPressed())
            {
                Game1.ExitGame = true;
            }

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the buttons.
            resume.Draw(spriteBatch);
            mainMenu.Draw(spriteBatch);
            quit.Draw(spriteBatch);

            // Draw the title sprite.
            pausedSprite.Draw(spriteBatch);
        }
    }
}
