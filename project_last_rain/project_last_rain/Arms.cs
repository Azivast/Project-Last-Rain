using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class Arms
    {
        // Sprite for arms.
        public Sprite ArmsSprite;
        // Target at which arms will point.
        private Vector2 target;
        // Position of arms.
        private Vector2 position;
        // Origin around which the arms pivot.
        private Vector2 origin = new Vector2(73, 26);
        // Boold for if gun is firing.
        public bool FacingRight;

        // Get/sets
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Target
        {
            get { return target; }
            set { target = value; }
        }

        // Constructor
        public Arms(Texture2D texture, Vector2 position, Rectangle initialFrame, int frameCount)
        {
            this.position = position;
            // Create a sprite for the arms.
            ArmsSprite = new Sprite(position, texture, initialFrame, Vector2.Zero);
            // Set the arm sprite's pivot to the correct place.
            ArmsSprite.Origin = origin;

            // Create frames for the sprite.
            for (int i = 1; i < frameCount; i++)
            {
                ArmsSprite.AddFrame(new Rectangle(initialFrame.X = (initialFrame.Width * i), initialFrame.Y, initialFrame.Width, initialFrame.Height));
            }

            // Stop the firing animation so gun remains idle on load.
            ArmsSprite.Animate = false;
        }

        // Function for firing the gun.
        public void Fire()
        {
            // Check if player is facing right and if so animate the gun from the corresponding frame.
            if (FacingRight == true)
            {
                // Corresponding frame at which gun fires -1 since Animate = true goes one frame forward.
                ArmsSprite.Frame = 0;
                ArmsSprite.Animate = true;

            }
            // Check if player is facing left and if so animate the gun from the corresponding frame.
            else if (FacingRight == false)
            {
                // Corresponding frame at which gun fires -1 since Animate = true goes one frame forward.
                ArmsSprite.Frame = 3;
                ArmsSprite.Animate = true;
            }

        }

        // Update
        public void Update(GameTime gameTime)
        {
            // Update the sprite and it's position
            ArmsSprite.Position = position;
            ArmsSprite.Update(gameTime);

            // Stop animation and go back to frame at which gun idles when facing right unless playing any of the frames where gun fires.
            if (FacingRight && ArmsSprite.Frame != 1 && ArmsSprite.Frame != 2)
            {
                ArmsSprite.Frame = 0;
                ArmsSprite.Animate = false;
            }
            // Stop animation and go back to frame at which gun idles when facing left unless playing any of the frames where gun fires.
            else if (!FacingRight && ArmsSprite.Frame != 4 && ArmsSprite.Frame != 5)
            {
                ArmsSprite.Frame = 3;
                ArmsSprite.Animate = false;
            }

            // Create a direction in which the arms should be rotated from the target.
            Vector2 rotationDirection = (ArmsSprite.Position + ArmsSprite.Origin) - target;
            // Normalize it.
            rotationDirection.Normalize();
            // Rotate the sprite in said direction.
            ArmsSprite.Rotation = (float)Math.Atan2(rotationDirection.Y, rotationDirection.X);
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the sprite.
            ArmsSprite.Draw(spriteBatch);
        }

    }
}
