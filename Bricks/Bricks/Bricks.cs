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

        public Bricks()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            fps = new FPS(this);
            Components.Add(fps);

            inputHandler = new InputHandler(this);
            Components.Add(inputHandler);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(ball, new Vector2(500, 200), Color.White);
            spriteBatch.Draw(brick, new Vector2(100, 100), Color.Yellow);
            spriteBatch.Draw(paddle, new Vector2(300, 300), Color.Tomato);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
