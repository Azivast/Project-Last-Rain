using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class Button
    {
        public Sprite buttonSprite;


        // Constructor
        public Button(Texture2D texture, Vector2 position, Rectangle initialFrame, int frameCount)
        {
            // Create the button's sprite.
            buttonSprite = new Sprite(position, texture, initialFrame, Vector2.Zero);

            // Create the frames for the button.
            for (int i = 1; i < frameCount; i++)
            {
                buttonSprite.AddFrame(new Rectangle(initialFrame.X = (initialFrame.Width * i), initialFrame.Y, initialFrame.Width, initialFrame.Height));
            }

        }

        // Fucntion for checking if button is pressed.
        public bool IsPressed()
        {
            // Check if mouse and button are colliding, if the left mouse button is pressed, and if the left mouse button wasn't pressed before.
            if (buttonSprite.IsBoxColliding(new Rectangle(InputManager.MouseState.X, InputManager.MouseState.Y, 1, 1)) &&
                InputManager.MouseState.LeftButton == ButtonState.Pressed && InputManager.PreviousMouseState.LeftButton == ButtonState.Released)
            {
                // Then return true: Button is being pressed.
                return true;
            }
            // Otherwise return false: Button is not being pressed.
            return false;
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Check if mouse is over button and if so update texture accordingly.
            if (buttonSprite.IsBoxColliding(new Rectangle(InputManager.MouseState.X, InputManager.MouseState.Y, 1, 1)))
            {
                // Move to second frame to change the texture.
                buttonSprite.Frame = 2;
            }
            // Otherwise keep first frame.
            else
            {
                buttonSprite.Frame = 0;
            }
        }


        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the button.
            buttonSprite.Draw(spriteBatch);
        }
    }
}
