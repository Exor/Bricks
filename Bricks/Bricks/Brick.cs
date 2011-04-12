using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    public class Brick : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        Texture2D brick;
        Vector2 position;
        Color color;

        public Brick(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            brick = content.Load<Texture2D>("brick");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(brick, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddBrick()
        {

        }

        public void RemoveBrick()
        {

        }
    }
}
