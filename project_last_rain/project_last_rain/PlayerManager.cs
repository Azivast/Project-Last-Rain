using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class PlayerManager
    {
        Texture2D playerSprite;
        // Timer for when to change frame.
        float timer = 0f;
        // Interval for when to change frame.
        float interval = 200f;
        int currentFrame = 0;
        int spriteWidth = 29;
        int spriteHeight = 47;
        // Player's speed.
        int spriteSpeed = 2;
        // Position at which the player spawns.
        private Vector2 startingPosition = new Vector2(960, 540);
        Rectangle sourceRect;
        Vector2 position;
        // Sound for firing.
        private SoundEffect firingSound;

        // Variables for the current and prevous keyboardstate.
        KeyboardState currentKBState;
        KeyboardState previousKBState;

        /// Variables for ShotManager.
        // From where shots are fired.
        private Vector2 gunOffset = new Vector2(0, 0);
        private float shotTimer = 0.0f;
        // Delay between each shot.
        private float minShotTimer = 650f;
        public ShotManager PlayerShotManager;
        // Bounds outside which shots are removed.
        Rectangle screenBounds;

        /// Variables for CollisionsManager
        // Player's collision radius.
        public int CollisionRadius = 25;
        // If the player is destroyed or not.
        public bool Destroyed = false;

        /// Variables for Arms
        public Arms PlayerArms;
        // Offset at which arms are placed on player's sprite.
        private Vector2 armsOffset = new Vector2(-37,42);


        // Get/sets.
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 StartingPosition
        {
            get { return startingPosition; }

        }

        public Texture2D Texture
        {
            get { return playerSprite; }
            set { playerSprite = value; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        // Coordinates of player's center.
        public Vector2 Center
        {
            get { return position + new Vector2(spriteWidth / 2, spriteHeight / 2); }
        }

        // PlayerManager's Constructor; Specifies what is needed to create an object using this class.
        public PlayerManager(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, Rectangle screenBounds, SoundEffect firingSound)
        {
            // Update internal variables to the one's supplied by the constructor.
            this.playerSprite = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.screenBounds = screenBounds;
            this.firingSound = firingSound;

            // Create a ShotManager for the player.
            PlayerShotManager = new ShotManager(texture, new Rectangle(0, 490, 3, 3), 5, 3, 650f, screenBounds);

            // Create the arms for the player.
            PlayerArms = new Arms(texture, position + armsOffset, new Rectangle(0, 142, 162, 51), 6);
        }

        // FireShot Function.
        public void FireShot()
        {
            // Create new shot if enough time has passed since last shot (shotTimer).
            if (shotTimer >= minShotTimer)
            {
                // Create position form which shot will be fired.
                Vector2 fireLoc = Center;

                // Create direction in which shot will be fired and normalize it.
                Vector2 shotDirection = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - fireLoc;
                shotDirection.Normalize();

                // Fire shot.
                PlayerShotManager.FireShot(
                Center + gunOffset, shotDirection, true);
                // Reset the shot timer.
                shotTimer = 0.0f;
                // Play firing sound
                firingSound.Play();
                PlayerArms.Fire();
            }
        }

        // HandleSpriteMovement Function.
        public void HandleSpriteMovement(GameTime gameTime)
        {
            // Sets the previous keyboard state to the current and then updates the current one.
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            // Shoot if left mouse button is pressed.
            if (InputManager.MouseState.LeftButton == ButtonState.Pressed)
            {
                FireShot();
            }

            // Create rectangle from current playing frame.
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            // Make player face the mouse and update the arms bool to which direction player is now facing.
            if (InputManager.MouseState.X > Position.X + sourceRect.Width / 2 && currentFrame < 5)
            {
                currentFrame = 5;
                PlayerArms.FacingRight = false;
            }
            else if (InputManager.MouseState.X <= Position.X + sourceRect.Width / 2 && currentFrame > 3)
            {
                currentFrame = 1;
                PlayerArms.FacingRight = true;
            }
            // Update arms bool for which way it should face.
            if (InputManager.MouseState.X > Position.X + sourceRect.Width / 2)
            {
                PlayerArms.FacingRight = false;
            }
            else
            {
                PlayerArms.FacingRight = true;
            }

            if (currentKBState.GetPressedKeys().Length == 0)
            {
                // Start at the first frame when pressing A.
                if (currentFrame > 0 && currentFrame < 4)
                {
                    currentFrame = 0;
                }
                // Start at the fourth frame when pressing D.
                if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }
            }

            // Animate the player Right and create a boundry to confine the player within the Right side of the window.
            if (currentKBState.IsKeyDown(Keys.D) == true)
            {
                // Animate player facing mouse.
                if (Mouse.GetState().X > Position.X)
                AnimateRight(gameTime);
                else
                {
                    AnimateLeft(gameTime);
                }
                // Move player right if left of the 'hog.
                if (position.X < screenBounds.Width - spriteWidth && position.X <= 697)
                {
                    position.X += spriteSpeed;
                }
                // Move player right if under the 'hog.
                else if (position.Y > 499)
                {
                    position.X += spriteSpeed;
                }
                // Move player right if right of the 'hog.
                else if (position.Y > 250 && position.X >= 1136)
                {
                    position.X += spriteSpeed;
                }
            }
            // Animates the player Left and creates a boundry to confine the player within the Left side of the window.
            if (currentKBState.IsKeyDown(Keys.A) == true)
            {
                // Animate player facing mouse.
                if (Mouse.GetState().X > Position.X)
                    AnimateRight(gameTime);
                else
                {
                    AnimateLeft(gameTime);
                }

                // Move player left if left of the 'hog.
                if (position.X < screenBounds.Width - spriteWidth && position.X <= 698)
                {
                    position.X -= spriteSpeed;
                }
                // Move player left if under the 'hog.
                else if (position.Y > 499)
                {
                    position.X -= spriteSpeed;
                }
                // Move player left if right of the 'hog.
                else if (position.Y > 250 && position.X >= 1137)
                {
                    position.X -= spriteSpeed;
                }
            }
            // Animates the player Down and creates a boundry to confine the player within the bottom of the window.
            if (currentKBState.IsKeyDown(Keys.S) == true)
            {
                if (currentFrame > 4 && currentFrame < 8)
                {
                    AnimateRight(gameTime);
                }
                else
                {
                    AnimateLeft(gameTime);
                }
                if (position.Y < screenBounds.Height - spriteHeight)
                {
                    position.Y += spriteSpeed;
                }
            }
            // Animates the player Up and creates a boundry to confine the player within the top of the window.
            if (currentKBState.IsKeyDown(Keys.W) == true)
            {
                if (currentFrame > 4 && currentFrame < 8)
                {
                    AnimateRight(gameTime);
                }
                else
                {
                    AnimateLeft(gameTime);
                }
                // Move player upwards if left of the 'hog.
                if (position.Y > 250 && position.X <= 698)
                {
                    position.Y -= spriteSpeed;
                }
                // Move player upwards if right of the 'hog.
                else if (position.Y > 250 && position.X >= 1135)
                {
                    position.Y -= spriteSpeed;
                }
                // Move player upwards if under the 'hog.
                else if (position.Y > 500)
                {
                    position.Y -= spriteSpeed;
                }
            }
        }


        public void AnimateRight(GameTime gameTime)
        {
            // Resets to the first "right frame" if Right is released.
            if (currentKBState != previousKBState)
            {
                currentFrame = 5;
            }

            // Create a timer that counts upwards in milliseconds.
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Changes frame and resets the timer once it reaches the value of "interval".
            if (timer > interval)
            {
                currentFrame++;
                // Loops back to the "first" frame once the "last" one is reached.
                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timer = 0f;
            }
        }

        public void AnimateUp(GameTime gameTime)
        {
            // Resets to the first "right frame" if Right is released.
            if (currentKBState != previousKBState)
            {
                currentFrame = 5;
            }

            // Create a timer that counts upwards in milliseconds.
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Changes frame and resets the timer once it reaches the value of "interval".
            if (timer > interval)
            {
                currentFrame++;
                // Loops back to the "first" frame once the "last" one is reached.
                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timer = 0f;
            }
        }

        public void AnimateDown(GameTime gameTime)
        {
            // Resets to the first "right frame" if Right is released.
            if (currentKBState != previousKBState)
            {
                currentFrame = 5;
            }

            // Create a timer that counts upwards in milliseconds.
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Changes frame and resets the timer once it reaches the value of "interval".
            if (timer > interval)
            {
                currentFrame++;
                // Loops back to the "first" frame once the "last" one is reached.
                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timer = 0f;
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            // Resets to the first "right frame" if Right is released.
            if (currentKBState != previousKBState)
            {
                currentFrame = 1;
            }
            // Create a timer that counts upwards in milliseconds.
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Changes frame and resets the timer once it reaches the value of "interval".
            if (timer > interval)
            {
                currentFrame++;
                // Loops back to the "first" frame once the "last" one is reached.
                if (currentFrame > 3)
                {
                    currentFrame = 1;
                }
                timer = 0f;
            }
        }

        public void Arms(GameTime gameTime)
        {
            // Update player arms and its target and position.
            PlayerArms.Target = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            PlayerArms.Position = position + armsOffset;
            PlayerArms.Update(gameTime);
        }
        // Update
        public void Update(GameTime gameTime)
        {
            // Update PlayerShotManager.
            PlayerShotManager.Update(gameTime);
            // Make shotTimer count upwards in milliseconds (allows shooting) if player ins't dead.
            if (!Destroyed)
            {
                shotTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            // Update arms.
            Arms(gameTime);
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw PlayerShotManager.
            PlayerShotManager.Draw(spriteBatch);
            // Draw arms.
            PlayerArms.Draw(spriteBatch);
        }
    }
}
