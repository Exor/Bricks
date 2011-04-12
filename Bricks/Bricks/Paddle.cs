using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    public class Paddle
    {
        Texture2D paddle;
        Vector2 paddlePosition = new Vector2(290, 920);
        int paddleSpeed = 700;
        bool moveRight;
        bool moveLeft;

        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y, paddle.Width, paddle.Height); }
        }


        public Paddle()
        {

        }

        public void LoadContent(ContentManager content)
        {
            paddle = content.Load<Texture2D>("paddle");
        }

        public void Update(GameTime gameTime)
        {
            if (moveLeft)
                paddlePosition.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * paddleSpeed;
            if (moveRight)
                paddlePosition.X += (float)gameTime.ElapsedGameTime.TotalSeconds * paddleSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddle, paddlePosition, Color.Tomato);
        }

        public void MoveRight()
        {
            moveRight = true;
            moveLeft = false;
        }

        public void MoveLeft()
        {
            moveLeft = true;
            moveRight = false;
        }

        public void DoNotMove()
        {
            moveLeft = false;
            moveRight = false;
        }

        public void CheckForCollisionAtScreenBoundries(int screenBoundry)
        {
            // prevention of movement outside the window (paddle)
            paddlePosition.X = MathHelper.Clamp(paddlePosition.X, 0,
                screenBoundry - paddle.Width);
        }
    }
}
