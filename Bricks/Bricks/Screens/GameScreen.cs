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

        Texture2D ball;
        Texture2D brick;
        Texture2D paddle;

        Vector2 ballPosition = new Vector2(100, 200);
        Vector2 paddlePosition = new Vector2(290, 920);
        Vector2 ballDirection = new Vector2(1, 1);

        bool moveLeft;
        bool moveRight;

        public GameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            ball = content.Load<Texture2D>("ball");
            brick = content.Load<Texture2D>("brick");
            paddle = content.Load<Texture2D>("paddle");
            base.LoadContent();
        }

        public override void HandleInput(InputHandler input)
        {
            if (input.Keyboard.WasKeyPressed(Keys.Enter))
                ScreenManager.AddScreen(new PauseScreen());

            // movement from keyboard input (paddle)
            moveLeft = false;
            moveRight = false;
            if (input.Keyboard.IsKeyDown(Keys.Left))
            {
                moveLeft = true;
            }
            if (input.Keyboard.IsKeyDown(Keys.Right))
            {
                moveRight = true;
            }
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            if (moveLeft)
                paddlePosition.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 700;
            if (moveRight)
                paddlePosition.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 700;

            // arbitrary movement (ball)
            ballPosition.X += ballDirection.X * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            ballPosition.Y += ballDirection.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            // boundary collision direction change (ball)
            if (ballPosition.X >= ScreenManager.GraphicsDevice.Viewport.Width - ball.Width)
            {
                ballPosition.X = ScreenManager.GraphicsDevice.Viewport.Width - ball.Width;
                ballDirection.X *= -1;
            }
            if (ballPosition.X <= 0)
            {
                ballPosition.X = 0;
                ballDirection.X *= -1;
            }
            if (ballPosition.Y >= ScreenManager.GraphicsDevice.Viewport.Height)
            {
                //Game over
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new GameOverScreen());

                //ballPosition.Y = ScreenManager.GraphicsDevice.Viewport.Height + ball.Height;
                //ballDirection.Y *= -1;
            }
            if (ballPosition.Y <= 0)
            {
                ballPosition.Y = 0;
                ballDirection.Y *= -1;
            }

            // prevention of movement outside the window (paddle)
            paddlePosition.X = MathHelper.Clamp(paddlePosition.X, 0,
                ScreenManager.GraphicsDevice.Viewport.Width - paddle.Width);

            // Collision (ball and paddle)
            Rectangle paddleRectange =
                new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y,
                paddle.Width, paddle.Height);

            Rectangle ballRectangle =
                new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
                ball.Width, ball.Height);

            if (ballPosition.Y >= paddleRectange.Top - ball.Height)
            {
                if (ballRectangle.Center.X > paddleRectange.Left && ballRectangle.Center.X < paddleRectange.Right)
                {
                    ballPosition.Y = paddleRectange.Top - ball.Height;
                    ballDirection.Y *= -1;
                }
            }

            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(ball, ballPosition, Color.White);
            ScreenManager.SpriteBatch.Draw(brick, new Vector2(100, 100), Color.Yellow);
            ScreenManager.SpriteBatch.Draw(paddle, paddlePosition, Color.Tomato);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
