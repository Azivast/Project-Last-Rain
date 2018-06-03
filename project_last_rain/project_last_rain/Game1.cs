using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using project_last_rain.Menu_classes;

namespace project_last_rain
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // The different gamestates.
        public enum GameState
        {
            TitleScreen, Tutorial, Credits, PrePlaying, Playing, Paused, GameOver
        };

        // Makes game start at TitleScreen
        public static GameState gameState = GameState.TitleScreen;

        // Player, Ally & rain.
        PlayerManager playerSprite;
        Ally ally;
        Rain rain;

        // Textures.
        Texture2D mixedSprites;
        Texture2D menuSprites;
        Texture2D background;
        Texture2D tutorialTexture;

        // Manager classes.
        EnemyManager enemyManager;
        CollisionsManager collisionsManager;
        ExplosionManager enemyExplosionManager;
        ExplosionManager playerExplosionManager;
        ScoreManager scoreManager;
        InputManager inputManager;

        // Sound effects.
        SoundEffect firingSound;
        SoundEffect infectedSound;
        SoundEffect music;
        SoundEffectInstance musicInstance;
        SoundEffect rainSound;
        SoundEffectInstance rainSoundInstance;
        SoundEffect ambientFiringSound;
        private SoundEffectInstance ambientFiringSoundInstance;

        // Menu classes.
        MainMenu mainMenu;
        Credits credits;
        Tutorial tutorial;
        Paused paused;
        GameOver gameOver;

        // Font.
        public static SpriteFont Font;

        // Bool used to exit game from other classes.
        public static bool ExitGame;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // Change window size and make window fullscreen.
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // Load texture for the SpriteSheet.
            mixedSprites = Content.Load<Texture2D>(@"Images/SpriteSheet");
            // Load texture for the Menu SpriteSheet.
            menuSprites = Content.Load<Texture2D>(@"Images/MenuSprites");
            // Load background texture.
            background = Content.Load<Texture2D>(@"Images/Background");
            // Load tutorial texture.
            tutorialTexture = Content.Load<Texture2D>(@"Images/Tutorial");

            // Create the different menus with their respective classes.
            mainMenu = new MainMenu(menuSprites);
            credits = new Credits(menuSprites, Font);
            tutorial = new Tutorial(menuSprites, tutorialTexture);
            paused = new Paused(menuSprites);
            gameOver = new GameOver(menuSprites);

            // Set screeBounds to the game windows size.
            Rectangle screenBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

            // Load firing sound.
            firingSound = Content.Load<SoundEffect>(@"Sounds/firing");
            // Load infected sound.
            infectedSound = Content.Load<SoundEffect>(@"Sounds/roar");
            // Load background music
            music = Content.Load<SoundEffect>(@"Sounds/sb_soulsearcher");
            // Create an instance of the music and loop it.
            musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            // Load rain sound
            rainSound = Content.Load<SoundEffect>(@"Sounds/rain");
            // Create an instance of the rain and loop it.
            rainSoundInstance = rainSound.CreateInstance();
            rainSoundInstance.IsLooped = true;
            // Load ambient firing sounds.
            ambientFiringSound = Content.Load<SoundEffect>(@"Sounds/ambient_firing");
            // Create an instance of ambient firing, loop it and lower its volume a bit.
            ambientFiringSoundInstance = ambientFiringSound.CreateInstance();
            ambientFiringSoundInstance.IsLooped = true;
            ambientFiringSoundInstance.Volume = 0.5f;

            // Create the player.
            playerSprite = new PlayerManager(mixedSprites, 1, 87, 141, screenBounds, firingSound);

            // Create the ally.
            ally = new Ally(mixedSprites, new Rectangle(0, 194, 380, 168), new Rectangle(381, 194, 81, 108));

            // Load font the font.
            Font = Content.Load<SpriteFont>(@"Fonts\pressstart");

            // Create the rain.
            rain = new Rain(
                Window.ClientBounds.Width,
                Window.ClientBounds.Height,
                200,
                new Vector2(0, 500f),
                mixedSprites,
                new Rectangle(64, 490, 6, 24));

            // Create the enemy manager.
            enemyManager = new EnemyManager(mixedSprites, new Rectangle(0, 363, 93, 126), 8, playerSprite, screenBounds, infectedSound);
            // Create the enemies' explosion manager.
            enemyExplosionManager = new ExplosionManager(
                mixedSprites,
                new Rectangle(0, 363, 93, 126),
                new Rectangle(372, 363, 93, 126),
                1,
                1,
                new Rectangle(28, 516, 6, 6),
                20,
                30,
                24,
                48);
            // Create the player's explosion manager.
            playerExplosionManager = new ExplosionManager(
                mixedSprites,
                new Rectangle(0, 0, 87, 141),
                new Rectangle(349, 0, 87, 141),
                1,
                1,
                new Rectangle(28, 523, 6, 6),
                20,
                30,
                24,
                48);

            // Create the collision manager.
            collisionsManager = new CollisionsManager(playerSprite, playerExplosionManager, ally, enemyExplosionManager, enemyManager);
            // Create the score manager.
            scoreManager = new ScoreManager();
            // Create the input manager.
            inputManager = new InputManager(mixedSprites, new Rectangle(381, 303, 48, 48));

            // Set the window title.
            this.Window.Title = "PROJECT LAST RAIN";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update input manager.
            inputManager.Update(gameTime);

            // Exit game when exit bool is true
            if (ExitGame)
            {
                Exit();
            }
            // Makes F toggle full-screen mode.
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }


            // Create a switch for where code specific to a gamestate can be placed.
            switch (gameState)
            {
                case GameState.TitleScreen:

                    // Update the main menu class.
                    mainMenu.Update(gameTime);
                    // Update the rain.
                    rain.Update(gameTime);

                    // Pause the ambient firing sounds.
                    ambientFiringSoundInstance.Pause();
                    // Play the other background sounds.
                    rainSoundInstance.Play();
                    musicInstance.Play();
                    break;

                case GameState.Tutorial:
                    // Update the tutorial menu class.
                    tutorial.Update(gameTime);
                    // Update the rain.
                    rain.Update(gameTime);
                    break;

                case GameState.Credits:
                    // Update the credits menu class.
                    credits.Update(gameTime);
                    // Update the rain.
                    rain.Update(gameTime);
                    break;

                // PrePlaying is used to reset everything before moving to gamestate playing.
                case GameState.PrePlaying:

                    gameState = GameState.Playing;
                    // Reset player.
                    playerSprite.Destroyed = false;
                    playerSprite.Position = playerSprite.StartingPosition;
                    playerSprite.PlayerShotManager.Shots.Clear();
                    // Reset score.
                    scoreManager.ResetScore();
                    // Reset/remove enemies and their shots.
                    enemyManager.Reset();
                    break;

                case GameState.Playing:
                    // Player movment. Links playerSprite to HandleSpriteMovement of PlayerManager so player can move.
                    playerSprite.HandleSpriteMovement(gameTime);

                    // Update the rain.
                    rain.Update(gameTime);

                    // Update playerSprite.
                    playerSprite.Update(gameTime);

                    // Update enemyManager.
                    enemyManager.Update(gameTime);

                    // Update player and enemy explosionManager.
                    enemyExplosionManager.Update(gameTime);
                    playerExplosionManager.Update(gameTime);

                    // Update collisionManager.
                    collisionsManager.CheckCollisions();

                    // Update scoreManager
                    scoreManager.Update(gameTime);

                    // Pause the game if escape is pressed
                    if (InputManager.KBState.IsKeyDown(Keys.Escape) && InputManager.PreviousKBState.IsKeyUp(Keys.Escape))
                        gameState = GameState.Paused;

                    // Move to Game Over if player dies.
                    if (playerSprite.Destroyed)
                    {
                        gameState = GameState.GameOver;
                    }

                    // Play the background sounds.
                    ambientFiringSoundInstance.Play();
                    rainSoundInstance.Play();
                    musicInstance.Play();
                    break;

                case GameState.Paused:
                    // Update paused menu.
                    paused.Update(gameTime);
                    // Pause the ambient firing sounds.
                    ambientFiringSoundInstance.Pause();
                    rainSoundInstance.Pause();
                    musicInstance.Pause();
                    break;

                case GameState.GameOver:
                    // Update the rain.
                    rain.Update(gameTime);
                    // Update explosionManager.
                    enemyExplosionManager.Update(gameTime);
                    playerExplosionManager.Update(gameTime);

                    gameOver.Update(gameTime);
                    break;

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Set the window's background color to black
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Switch to draw stuff specific to a gamestate. Most of it is self-documenting.
            switch (gameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.Draw(background, Vector2.Zero, Color.RoyalBlue);
                    rain.Draw(spriteBatch);
                    mainMenu.Draw(spriteBatch);
                    break;

                case GameState.Tutorial:
                    spriteBatch.Draw(background, Vector2.Zero, Color.RoyalBlue);
                    rain.Draw(spriteBatch);
                    tutorial.Draw(spriteBatch);
                    break;
                case GameState.Credits:
                    spriteBatch.Draw(background, Vector2.Zero, Color.RoyalBlue);
                    rain.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;
                case GameState.PrePlaying:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    rain.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    // Draw 'hog if player is "behind" it.
                    if (playerSprite.Position.Y >= ally.HogSprite.Position.Y)
                    {
                        ally.Draw(spriteBatch);
                    }

                    spriteBatch.Draw(playerSprite.Texture, playerSprite.Position, playerSprite.SourceRect, Color.White);
                    playerSprite.Draw(spriteBatch);
                    // Draw 'hog if player is "in front" of it.
                    if (playerSprite.Position.Y + playerSprite.Texture.Height <
                        ally.HogSprite.Position.Y + ally.HogSprite.Texture.Height)
                    {
                        ally.Draw(spriteBatch);
                    }
                    enemyManager.Draw(spriteBatch);
                    enemyExplosionManager.Draw(spriteBatch);
                    playerExplosionManager.Draw(spriteBatch);
                    rain.Draw(spriteBatch);
                    scoreManager.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    // Draw 'hog if player is "behind" it.
                    if (playerSprite.Position.Y >= ally.HogSprite.Position.Y)
                    {
                        ally.Draw(spriteBatch);
                    }

                    spriteBatch.Draw(playerSprite.Texture, playerSprite.Position, playerSprite.SourceRect, Color.White);
                    playerSprite.Draw(spriteBatch);
                    // Draw 'hog if player is "in front" of it.
                    if (playerSprite.Position.Y + playerSprite.Texture.Height <
                        ally.HogSprite.Position.Y + ally.HogSprite.Texture.Height)
                    {
                        ally.Draw(spriteBatch);
                    }
                    enemyManager.Draw(spriteBatch);
                    enemyExplosionManager.Draw(spriteBatch);
                    playerExplosionManager.Draw(spriteBatch);
                    rain.Draw(spriteBatch);
                    scoreManager.Draw(spriteBatch);
                    paused.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    // Draw 'hog if player is "behind" it.
                    if (playerSprite.Position.Y >= ally.HogSprite.Position.Y)
                    {
                        ally.Draw(spriteBatch);
                    }

                    spriteBatch.Draw(playerSprite.Texture, playerSprite.Position, playerSprite.SourceRect, Color.White);
                    playerSprite.Draw(spriteBatch);
                    // Draw 'hog if player is "in front" of it.
                    if (playerSprite.Position.Y + playerSprite.Texture.Height <
                        ally.HogSprite.Position.Y + ally.HogSprite.Texture.Height)
                    {
                        ally.Draw(spriteBatch);
                    }
                    enemyManager.Draw(spriteBatch);
                    enemyExplosionManager.Draw(spriteBatch);
                    playerExplosionManager.Draw(spriteBatch);
                    rain.Draw(spriteBatch);
                    gameOver.Draw(spriteBatch);
                    break;
            }
            // Draw mouse cursor sprite.
            inputManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
