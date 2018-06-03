using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain.Menu_classes
{
    class Tutorial
    {
        private Button back;
        private Sprite title;
        private Texture2D tutorialTexture;

        // Constructor
        public Tutorial(Texture2D texture, Texture2D tutorialTexture)
        {
            // Create the different back button.
            back = new Button(texture, new Vector2(660, 900), new Rectangle(0, 395, 600, 45), 2);

            // Update internal tutorialTexture;
            this.tutorialTexture = tutorialTexture;

            // Create the title sprite.
            title = new Sprite(new Vector2(654, 30), texture, new Rectangle(0, 184, 630, 118), Vector2.Zero);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Update the start button.
            back.Update(gameTime);
            // Change to gamestate TitleScreen if back button is pressed.
            if (back.IsPressed())
            {
                Game1.gameState = Game1.GameState.TitleScreen;
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the back button.
            back.Draw(spriteBatch);

            // Draw the tutorial texture.
            spriteBatch.Draw(tutorialTexture, Vector2.Zero, Color.White);

            // Draw the title sprite.
            title.Draw(spriteBatch);
        }
    }
}
