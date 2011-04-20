using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    class Level3 : Level
    {
        public Level3(Game game, ContentManager content)
        {
            base.Initialize();
            levelName = "1";
            LoadContent(content);
        }

        public override void LoadContent(ContentManager content)
        {
            brickTexture = content.Load<Texture2D>("brick");
            background = content.Load<Texture2D>("SoundbyPibbs");

            for (int i = 0; i < 640; i += brickTexture.Width*2)
            {
                for (int j = 40; j < 400; j += brickTexture.Height*4)
                {
                    bricks.Add(new Brick(brickTexture, new Vector2(i, j), Color.Olive, 100));
                }
            }
            for (int i = brickTexture.Width; i < 640; i += brickTexture.Width * 2)
            {
                for (int j = 40 + brickTexture.Height; j < 400; j += brickTexture.Height * 4)
                {
                    bricks.Add(new Brick(brickTexture, new Vector2(i, j), Color.NavajoWhite, 100));
                }
            }

            //bricks.Add(new Brick(brickTexture, new Vector2(10, 50), Color.Blue, 100));
        }

    }
}
