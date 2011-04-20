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
    class WinningScreen : Screen
    {
        string text = "You Win!";
        int finalScore;

        public WinningScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.1);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);
        }

        public WinningScreen(int score)
            : base()
        {
            finalScore = score;
        }

        public override void HandleInput(InputHandler input)
        {
            if (input.Keyboard.WasKeyPressed(Keys.Enter))
                ExitScreen();
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();

            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, text,
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, 400),
                Color.Black, 0, new Vector2(ScreenManager.Font.MeasureString(text).X / 2, 0), 2, SpriteEffects.None, 0);

            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, "Your final score was " + finalScore.ToString() + " points",
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, 650),
                Color.Black, 0, new Vector2(ScreenManager.Font.MeasureString("Your final score was         points").X / 2, 0), 2, SpriteEffects.None, 0);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
