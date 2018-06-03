using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class InputManager
    {
        // Public states for the mouse and keyboard. As well as their previous states.
        public static MouseState MouseState;
        public static MouseState PreviousMouseState;
        public static KeyboardState KBState;
        public static KeyboardState PreviousKBState;
        // Sprite for the in-game mouse.
        private Sprite mouseSprite;

        // Constructor
        public InputManager(Texture2D texture, Rectangle initialFrame)
        {
            // Create a sprite for the mouse.
            mouseSprite = new Sprite(Vector2.Zero, texture, initialFrame, Vector2.Zero);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Update mouse & keyboard states.
            PreviousKBState = KBState;
            KBState = Keyboard.GetState();
            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();

            // Rotate the sprite slightly.
            mouseSprite.Rotation += MathHelper.TwoPi / 360;
            // Rotate the sprite even more if left mouse button is pressed.
            if (MouseState.LeftButton == ButtonState.Pressed)
            {
                mouseSprite.Rotation += MathHelper.TwoPi / 180;
            }
            // Set the sprite's position to the position of the mouse cursor.
            mouseSprite.Position = new Vector2(MouseState.X - mouseSprite.FrameWidth / 2, MouseState.Y - mouseSprite.FrameHeight / 2);
            // Update the sprite.
            mouseSprite.Update(gameTime);
        }


        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the sprite.
            mouseSprite.Draw(spriteBatch);
        }
    }
}
