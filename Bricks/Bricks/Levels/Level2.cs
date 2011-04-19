using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    class Level2 : Level
    {
        public Level2(Game game, ContentManager content)
        {
            base.Initialize();
            levelName = "2";
            LoadContent(content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("brick");

            //for (int i = 20; i < 590; i += 130)
            //{
            //    for (int j = 40; j < 600; j += 50)
            //    {
            //        bricks.Add(new Brick(texture, new Vector2(i, j), Color.Green, 100));
            //    }
            //}
            bricks.Add(new Brick(texture, new Vector2(10, 50), Color.Green, 100));
        }

    }
}
