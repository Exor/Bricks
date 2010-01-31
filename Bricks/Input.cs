using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Bricks
{
    class Input
    {
        public void GetKeyboardState()
        {
            KeyboardState state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Left))
                shipPosition.X -= speed;
            if (state.IsKeyDown(Keys.Right))
                shipPosition.X += speed;
            if (state.IsKeyDown(Keys.Down))
                shipPosition.Y += speed;
            if (state.IsKeyDown(Keys.Up))
                shipPosition.Y -= speed;
        }

    }
}
