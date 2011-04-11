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
    class GameOverScreen : Screen
    {
        const float timeToExit = 3;
        float totalTimeOnScreen;
        float scale = 5;
        string text = "Game Over";

        public GameOverScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void HandleInput(InputHandler input)
        {
            if (input.Keyboard.WasKeyPressed(Keys.Enter))
                ExitScreen();
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            totalTimeOnScreen += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (totalTimeOnScreen > timeToExit)
                ExitScreen();
            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();

            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, text,
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, ScreenManager.GraphicsDevice.Viewport.Height /2),
                Color.Black, 0, new Vector2(ScreenManager.Font.MeasureString(text).X / 2, 0), scale, SpriteEffects.None, 0);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
