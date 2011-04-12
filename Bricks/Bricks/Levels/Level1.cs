using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    class Level1 : Level
    {
        public Level1(Game game)
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("brick");

            for (int i = 0; i < 700; i += 100)
            {
                for (int j = 0; j < 500; j += 100)
                {
                    bricks.Add(new Brick(texture, new Vector2(i, j), Color.Blue));
                }
            }
        }
    }
}
