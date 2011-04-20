using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XELibrary;

namespace Bricks
{
    class GameScreen : Screen
    {
        ContentManager content;

        List<Level> levels;
        Level currentLevel;
        int levelCounter = 0;
        Player player;
        Ball ball;
        Paddle paddle;
        Hud hud;

        public GameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.1);
            TransitionOffTime = TimeSpan.FromSeconds(0.1);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            player = new Player(3);
            LoadLevels();
            ball = new Ball(content);
            paddle = new Paddle(content);
            LoadNextLevel();
            hud = new Hud(content, currentLevel.Name, player.Lives, player.Score);

            base.LoadContent();
        }

        public override void HandleInput(InputHandler input)
        {
            if (input.Keyboard.WasKeyPressed(Keys.Enter))
                ScreenManager.AddScreen(new PauseScreen());

            // movement from keyboard input (paddle)
            if (input.Keyboard.IsKeyDown(Keys.Left))
            {
                paddle.MoveLeft();
            }
            else if (input.Keyboard.IsKeyDown(Keys.Right))
            {
                paddle.MoveRight();
            }
            else
            {
                paddle.DoNotMove();
            }

            // ball launch
            if (input.Keyboard.IsKeyDown(Keys.Space))
            {
                ball.Launch(paddle.BoundingRectangle);
            }
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            ball.Update(gameTime);
            paddle.Update(gameTime);
            hud.Update(gameTime, player.Score, player.Lives, currentLevel.Name);
            UpdateCollisions();
            currentLevel.Update(gameTime);

            if (player.isGameOver())
            {
                ScreenManager.AddScreen(new GameOverScreen(player.Score));
                ScreenManager.RemoveScreen(this);
            }

            if (currentLevel.IsLevelFinished())
            {
                LoadNextLevel();
            }

            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();

            currentLevel.Draw(ScreenManager.SpriteBatch);
            ball.Draw(ScreenManager.SpriteBatch);
            paddle.Draw(ScreenManager.SpriteBatch);
            hud.Draw(ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateCollisions()
        {
            paddle.CheckForCollisionAtScreenBoundries(ScreenManager.GraphicsDevice.Viewport.Width);
            ball.CheckForCollisionBetweenBallAndPaddle(paddle.BoundingRectangle);

            if (ball.CheckForCollisionAtScreenBoundries(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height))
            {
                player.Lives -= 1;
                ball.Reset();
                paddle.Reset();
            }

            foreach (Brick brick in currentLevel.Bricks)
            {
                if (ball.CheckForCollisionBetweenBallAndRectangle(brick.BoundingRectangle))
                {
                    brick.Hit = true;
                    player.Score += brick.PointValue;
                }
            }
        }

        private void LoadLevels()
        {
            levels = new List<Level>();
            levels.Add(new Level1(ScreenManager.Game, content));
            levels.Add(new Level2(ScreenManager.Game, content));
            levels.Add(new Level3(ScreenManager.Game, content));
        }

        private void LoadNextLevel()
        {
            if (currentLevel != null)
            {
                levelCounter++;
            }
            if (levels.Count > levelCounter)
            {
                currentLevel = levels[levelCounter];
                ball.Reset();
                paddle.Reset();
            }
            else
            {
                ScreenManager.AddScreen(new WinningScreen(player.Score));
                ScreenManager.RemoveScreen(this);
            }
        }
    }
}
