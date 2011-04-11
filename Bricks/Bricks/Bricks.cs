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
using XELibrary;

namespace Bricks
{
    public class Bricks : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball;
        Texture2D brick;
        Texture2D paddle;

        FPS fps;
        InputHandler inputHandler;

        Vector2 ballPosition = new Vector2(100, 200);
        Vector2 paddlePosition = new Vector2(290, 920);
        Vector2 ballDirection = new Vector2(1, 1);

        public Bricks()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            fps = new FPS(this);
            Components.Add(fps);

            inputHandler = new InputHandler(this);
            Components.Add(inputHandler);

            // sets the game resolution
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 960;

            // makes the refresh rate the same as the monitors refresh rate
            //this.graphics.SynchronizeWithVerticalRetrace = true;
            //this.IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("ball");
            brick = Content.Load<Texture2D>("brick");
            paddle = Content.Load<Texture2D>("paddle");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (inputHandler.Keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            // movement from keyboard input (paddle)
            if (inputHandler.Keyboard.IsKeyDown(Keys.Left))
            {
                paddlePosition.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 700;
            }
            if (inputHandler.Keyboard.IsKeyDown(Keys.Right))
            {
                paddlePosition.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 700;
            }

            // arbitrary movement (ball)
            ballPosition.X += ballDirection.X * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            ballPosition.Y += ballDirection.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            // boundary collision direction change (ball)
            if (ballPosition.X >= Window.ClientBounds.Width - ball.Width)
            {
                ballPosition.X = Window.ClientBounds.Width - ball.Width;
                ballDirection.X *= -1;
            }
            if (ballPosition.X <= 0)
            {
                ballPosition.X = 0;
                ballDirection.X *= -1;
            } 
            if (ballPosition.Y >= Window.ClientBounds.Height - ball.Height)
            {
                ballPosition.Y = Window.ClientBounds.Height - ball.Height;
                ballDirection.Y *= -1;
            }
            if (ballPosition.Y <= 0)
            {
                ballPosition.Y = 0;
                ballDirection.Y *= -1;
            }
            
            // prevention of movement outside the window (paddle)
            paddlePosition.X = MathHelper.Clamp(paddlePosition.X, 0, 
                Window.ClientBounds.Width - paddle.Width);

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(ball, ballPosition, Color.White);
            spriteBatch.Draw(brick, new Vector2(100, 100), Color.Yellow);
            spriteBatch.Draw(paddle, paddlePosition, Color.Tomato);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
