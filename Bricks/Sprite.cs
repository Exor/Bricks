using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    abstract class Sprite
    {
         public Vector2 position;
         SpriteBatch spriteBatch;
         Texture2D texture;
         Color tint;

         public void Initialize(SpriteBatch batch, Texture2D text, Vector2 pos, Color col)
         {
             spriteBatch = batch;
             texture = text;
             position = pos;
             tint = col;
         }

         public void Update()
         {
 
         }

         public void Draw()
         {
             spriteBatch.Draw(texture, position, tint);
         }
    }
}
