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
    class TitleScreen : Screen
    {
        float scale = 1;
        float direction = 2;
        string title = "Bricks";
        string text = "Press Enter to play";
        Vector2 origin;

        public TitleScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void HandleInput(InputHandler input)
        {
            if (input.Keyboard.WasKeyPressed(Keys.Down))
                this.ExitScreen();
            if (input.Keyboard.WasKeyPressed(Keys.Enter))
                ScreenManager.AddScreen(new GameScreen());
        }

        public override void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            origin = new Vector2(ScreenManager.Font.MeasureString(title).X/2, ScreenManager.Font.MeasureString(title).Y/2);
            scale += ((float)gameTime.ElapsedGameTime.TotalSeconds * direction);
            if (scale < 1 || scale > 2)
                direction *= -1;
            base.Update(gameTime, shouldTransitionOff);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);
            ScreenManager.SpriteBatch.Begin();

            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, title, 
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2, ScreenManager.GraphicsDevice.Viewport.Height/3), 
                Color.Black, 0, origin, scale, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, text,
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, 600), 
                Color.Black,0, new Vector2(ScreenManager.Font.MeasureString(text).X/2, 0), 1, SpriteEffects.None, 0);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
