using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Bricks
{
    public abstract class Level
    {
        protected List<Brick> bricks;
        List<Brick> bricksCopy = new List<Brick>();
        protected Game game;
        protected Texture2D brickTexture;
        protected Texture2D background;
        protected string levelName;

        public string Name
        {
            get { return levelName; }
        }

        public List<Brick> Bricks
        {
            get { return bricks; }
        }

        public Level() { }

        public Level(Game Game, ContentManager content)
            :this(Game)
        {
            
        }

        public Level(Game Game)
        {
            game = Game;
            Initialize();
        }

        public virtual void Initialize()
        {
            bricks = new List<Brick>();
        }

        public virtual void LoadContent(ContentManager content)
        {
            brickTexture = content.Load<Texture2D>("brick");
        }

        public void Update(GameTime gameTime)
        {
            RemoveBricks();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            foreach (Brick brick in bricks)
            {
                spriteBatch.Draw(brick.BrickTexture, brick.Position, brick.Color);
            }
        }

        public void RemoveBricks()
        {
            foreach (Brick brick in bricks)
            {
                bricksCopy.Add(brick);
            }

            while (bricksCopy.Count > 0)
            {
                Brick currentBrick = bricksCopy[bricksCopy.Count - 1];
                if (currentBrick.Hit)
                    RemoveBrick(currentBrick);
                bricksCopy.Remove(currentBrick);
            }

        }

        public void RemoveBrick(Brick brick)
        {
            bricks.Remove(brick);
        }

        public bool IsLevelFinished()
        {
            if (bricks.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
