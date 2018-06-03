using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace project_last_rain
{
    class CollisionsManager
    {
        // Manager for player, enemy, and ally, as well as their explosion managers.
        private PlayerManager playerManager;
        private ExplosionManager playerExplosionManager;
        private EnemyManager enemyManager;
        private ExplosionManager enemyExplosionManager;
        private Ally ally;
        // OffScreen used to remove objects outside screen.
        private Vector2 offScreen = new Vector2(-500, -500);

        // Constructor
        public CollisionsManager(PlayerManager playerSprite, ExplosionManager playerExplosionManager, Ally ally, ExplosionManager enemyExplosionManager, EnemyManager enemyManager)
        {
            // Update the internal variables to the ones supplied by constructor.
            this.playerManager = playerSprite;
            this.playerExplosionManager = playerExplosionManager;
            this.enemyExplosionManager = enemyExplosionManager;
            this.enemyManager = enemyManager;
            this.ally = ally;
        }

        // Function for if Enemy is beeing shot.
        private void checkShotToEnemyCollisions()
        {
            // Run for all shots.
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                // And all enemies.
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    // Check if a shot and enemy is colliding.
                    if (shot.IsBoxColliding(enemy.EnemySprite.BoundingBoxRect))
                    {
                        // Move shot off screen so that it can be removed.
                        shot.Position = offScreen;
                        // Destroy the enemy.
                        enemy.Destroyed = true;
                        // Add an explosion att the enemy.
                        enemyExplosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10, !(enemy.EnemySprite.Velocity.X < 0));
                        ScoreManager.CurrentScore += 10;
                    }
                }
            }
        }

        // Function for if player is beeing shot.
        private void checkShotToPlayerCollisions()
        {
            // Run for all enemy shots.
            foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
            {
                // Check if shot is colliding with player.
                if (shot.IsCircleColliding(playerManager.Center, playerManager.CollisionRadius))
                {
                    // Move shot off screen so that it can be removed.
                    shot.Position = offScreen;
                    // Destroy the player.
                    playerManager.Destroyed = true;

                    // Add an explosion corresponding to if player is facing left or right.
                    if (playerManager.Position.X >= InputManager.MouseState.X)
                    {
                        playerExplosionManager.AddExplosion(playerManager.Center, Vector2.Zero, false);
                    }
                    else
                    {
                        playerExplosionManager.AddExplosion(playerManager.Center, Vector2.Zero, true);
                    }
                }
            }
        }

        // Function for if player and enemy are colliding.
        private void checkEnemyToPlayerCollisions()
        {
            // Run for all enemies.
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                // Check if player and enemy is colliding.
                if (enemy.EnemySprite.IsCircleColliding(playerManager.Center, playerManager.CollisionRadius))
                {
                    // Destroy the player.
                    playerManager.Destroyed = true;

                    // Add an explosion corresponding to if player is facing left or right.
                    if (playerManager.Position.X >= InputManager.MouseState.X)
                    {
                        playerExplosionManager.AddExplosion(playerManager.Center, Vector2.Zero, false);
                    }
                    else
                    {
                        playerExplosionManager.AddExplosion(playerManager.Center, Vector2.Zero, true);
                    }
                }
            }
        }

        // Function for if ally and enemy are colliding.
        private void checkEnemyToAllyCollisions()
        {
            // Run for each enemy.
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                // Check if ally and enemu is colliding.
                if (enemy.EnemySprite.IsCircleColliding(ally.AllySprite.Center, ally.AllySprite.CollisionRadius))
            {
                // Change gamestate to game over.
                Game1.gameState = Game1.GameState.GameOver;
                // Add an explosion at the ally.
                playerExplosionManager.AddExplosion(ally.AllySprite.Center, Vector2.Zero, false);
                }
            }
        }

        // Function for checking collisions (runs all the other functions).
        public void CheckCollisions()
        {
            checkShotToEnemyCollisions();
            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkEnemyToPlayerCollisions();
                checkEnemyToAllyCollisions();
            }
        }
    }
}
