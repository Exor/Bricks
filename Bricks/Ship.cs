using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    class Ship : Sprite
    {
        public Ship(SpriteBatch batch, Texture2D text, Vector2 pos, Color col)
        {
            Initialize(batch, text, pos, col);
        }
    }
}
