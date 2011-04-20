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
            brickTexture = content.Load<Texture2D>("brick");
            background = content.Load<Texture2D>("Sushi");

            for (int i = 0; i < 640; i += brickTexture.Width*3)
            {
                for (int j = 40; j < 400; j += brickTexture.Height)
                {
                    bricks.Add(new Brick(brickTexture, new Vector2(i, j), Color.Green, 100));
                }
            }
            //bricks.Add(new Brick(brickTexture, new Vector2(10, 50), Color.Green, 100));
        }

    }
}
