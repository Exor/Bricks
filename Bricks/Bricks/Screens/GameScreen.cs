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

        Level1 level;
        Ball ball;
        Paddle paddle;

        public GameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            level = new Level1(ScreenManager.Game);
            level.LoadContent(content);
            ball = new Ball();
            ball.LoadContent(content);
            paddle = new Paddle();
            paddle.LoadContent(content);

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
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            ball.Update(gameTime);
            paddle.Update(gameTime);
            level.Update(gameTime);
            UpdateCollisions();

            //UpdateHud();

            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();

            ball.Draw(ScreenManager.SpriteBatch);
            paddle.Draw(ScreenManager.SpriteBatch);
            level.Draw(ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateCollisions()
        {
            paddle.CheckForCollisionAtScreenBoundries(ScreenManager.GraphicsDevice.Viewport.Width);
            ball.CheckForCollisionAtScreenBoundries(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            ball.CheckForCollisionBetweenBallAndPaddle(paddle.BoundingRectangle);
            
            foreach (Brick brick in level.Bricks)
            {
                if (ball.CheckForCollisionBetweenBallAndRectangle(brick.BoundingRectangle))
                    brick.Hit = true;
            }
            level.RemoveBricks();
        }
    }
}
