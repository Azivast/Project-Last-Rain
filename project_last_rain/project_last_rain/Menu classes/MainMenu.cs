using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain.Menu_classes
{
    class MainMenu
    {
        private Button start;
        private Button tutorial;
        private Button credits;
        private Button quit;
        private Sprite title;

        // Constructor
        public MainMenu(Texture2D Texture)
        {
            // Create the different buttons at their respective position.
            start = new Button(Texture, new Vector2(660, 600), new Rectangle(0, 0, 600, 45), 2);
            tutorial = new Button(Texture, new Vector2(660, 650), new Rectangle(0, 46, 600, 45), 2);
            credits = new Button(Texture, new Vector2(660, 700), new Rectangle(0, 92, 600, 45), 2);
            quit = new Button(Texture, new Vector2(660, 750), new Rectangle(0, 138, 600, 45), 2);

            // Create the title sprite.
            title = new Sprite(new Vector2(654, 400), Texture, new Rectangle(0, 184, 630, 118), Vector2.Zero);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Update the start button.
            start.Update(gameTime);
            // Change to gamestate PrePlaying if start button is pressed.
            if (start.IsPressed())
            {
                Game1.gameState = Game1.GameState.PrePlaying;
            }

            // Update the tutorial button.
            tutorial.Update(gameTime);
            // Change to gamestate tutorial if tutorial button is pressed.
            if (tutorial.IsPressed())
            {
                Game1.gameState = Game1.GameState.Tutorial;
            }

            // Update the credits button.
            credits.Update(gameTime);
            // Change to gamestate credits if credits button is pressed.
            if (credits.IsPressed())
            {
                Game1.gameState = Game1.GameState.Credits;
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
            start.Draw(spriteBatch);
            tutorial.Draw(spriteBatch);
            credits.Draw(spriteBatch);
            quit.Draw(spriteBatch);

            // Draw the title sprite.
            title.Draw(spriteBatch);
        }
    }
}
