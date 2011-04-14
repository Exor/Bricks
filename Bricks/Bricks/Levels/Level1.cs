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
        public Level1(Game game, ContentManager content)
        {
            base.Initialize();
            levelName = "1";
            LoadContent(content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("brick");

            for (int i = 0; i < 590; i += 60)
            {
                for (int j = 40; j < 500; j += 50)
                {
                    bricks.Add(new Brick(texture, new Vector2(i, j), Color.Blue, 100));
                }
            }
        }
    }
}
