﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bricks
{
    class Brick : Sprite
    {
        public Brick(SpriteBatch batch, Texture2D text, Vector2 pos, Color col)
        {
            Initialize(batch, text, pos, col);
        }
    }
}
