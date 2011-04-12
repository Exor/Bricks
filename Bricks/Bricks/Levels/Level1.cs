using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bricks
{
    class Level1 : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        List<Brick> bricks;
        public List<Brick> Bricks
        {
            get { return bricks; }
        }

        Texture2D texture;

        public Level1(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            bricks = new List<Brick>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = content.Load<Texture2D>("brick");

            bricks.Add(new Brick(texture, new Vector2(200, 200), Color.Blue));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Brick brick in bricks)
            {
                spriteBatch.Draw(brick.BrickTexture, brick.Position, brick.Color);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void RemoveBrick(Brick brick)
        {
            bricks.Remove(brick);
        }
    }
}
