using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_last_rain.Menu_classes
{
    class Credits
    {
        private Button back;
        private Sprite title;
        private Sprite ms;

        // Constructor
        public Credits(Texture2D texture, SpriteFont font)
        {
            // Create the different buttons at their respective position.
            back = new Button(texture, new Vector2(660, 900), new Rectangle(0, 395, 600, 45), 2);
            // Create the title sprite.
            title = new Sprite(new Vector2(654, 30), texture, new Rectangle(0, 184, 630, 118), Vector2.Zero);
            // Create the sprite for microsoft's usage rules.
            ms = new Sprite(new Vector2(505, 810), texture, new Rectangle(0, 551, 909, 47), Vector2.Zero);
        }

        // Uppdate
        public void Update(GameTime gameTime)
        {
            // Update back button.
            back.Update(gameTime);
            // Go to main menu if back button is pressed.
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
            // Draw Title & Microsoft sprite
            title.Draw(spriteBatch);
            ms.Draw(spriteBatch);

            // Draw all credit strings.
            spriteBatch.DrawString(Game1.Font, "PROGRAMMED BY", new Vector2(785, 180), Color.White);
            spriteBatch.DrawString(Game1.Font, "OLLE ASTRE", new Vector2(840, 220), Color.White);
            spriteBatch.DrawString(Game1.Font, "GRAPHICS BY", new Vector2(815, 300), Color.White);
            spriteBatch.DrawString(Game1.Font, "OLLE ASTRE", new Vector2(840, 340), Color.White);
            spriteBatch.DrawString(Game1.Font, "SFX BY", new Vector2(895, 410), Color.White);
            spriteBatch.DrawString(Game1.Font, "FREESOUND.ORG", new Vector2(785, 450), Color.White);
            spriteBatch.DrawString(Game1.Font, "LICENSE: CC-BY & CC0", new Vector2(720, 490), Color.White);
            spriteBatch.DrawString(Game1.Font, "MUSIC BY", new Vector2(865, 560), Color.White);
            spriteBatch.DrawString(Game1.Font, "SCOTT BUCKLEY", new Vector2(795, 600), Color.White);
            spriteBatch.DrawString(Game1.Font, "WWW.SCOTTBUCKLEY.COM.AU", new Vector2(660, 640), Color.White);
            spriteBatch.DrawString(Game1.Font, "FONT", new Vector2(905, 710), Color.White);
            spriteBatch.DrawString(Game1.Font, "PRESS START", new Vector2(820, 750), Color.White);
        }
    }
}
