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

        Vector2 ballPosition = new Vector2(100,200);
        Vector2 paddlePosition = new Vector2(290, 920);

        int yDirection = 1;
        int xDirection = 1;

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
            ballPosition.X += xDirection * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            ballPosition.Y += yDirection * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            // boundary collision direction change (ball)
            if (ballPosition.X >= Window.ClientBounds.Width - ball.Width)
                xDirection = -1;
            if (ballPosition.X <= 0)
                xDirection = 1;
            if (ballPosition.Y >= Window.ClientBounds.Height - ball.Height)
                yDirection = -1;
            if (ballPosition.Y <= 0)
                yDirection = 1;
            
            // prevention of movement outside the window (paddle)
            paddlePosition.X = MathHelper.Clamp(paddlePosition.X, 0, 
                Window.ClientBounds.Width - paddle.Width);
            //paddlePosition.Y = MathHelper.Clamp(paddlePosition.Y, 0,
            //    Window.ClientBounds.Height - paddle.Height);

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
