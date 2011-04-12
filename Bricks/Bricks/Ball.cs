using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    public class Ball
    {
        Texture2D ball;
        Vector2 ballPosition = new Vector2(300, 100);
        Vector2 ballDirection = new Vector2(1, 1);
        int ballSpeed = 500;

        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)ballPosition.X, (int)ballPosition.Y, ball.Width, ball.Height); }
        }

        public int BallSpeed
        {
            get { return ballSpeed; }
        }

        public Ball()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            ball = content.Load<Texture2D>("ball");
        }

        public void Update(GameTime gameTime)
        {
            // arbitrary movement (ball)
            ballPosition.X += ballDirection.X * (float)gameTime.ElapsedGameTime.TotalSeconds * ballSpeed;
            ballPosition.Y += ballDirection.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * ballSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, ballPosition, Color.White);
        }

        public void CheckForCollisionAtScreenBoundries(int screenBoundryX, int screenBoundryY)
        {
            // boundary collision direction change (ball)
            if (ballPosition.X >= screenBoundryX - ball.Width)
            {
                ballPosition.X = screenBoundryX - ball.Width;
                ballDirection.X *= -1;
            }
            if (ballPosition.X <= 0)
            {
                ballPosition.X = 0;
                ballDirection.X *= -1;
            }
            if (ballPosition.Y >= screenBoundryY + ball.Height)
            {
                ballPosition.Y = screenBoundryY + ball.Height;
                ballDirection.Y *= -1;
            }
            if (ballPosition.Y <= 0)
            {
                ballPosition.Y = 0;
                ballDirection.Y *= -1;
            }
        }

        public void CheckForCollisionBetweenBallAndPaddle(Rectangle paddleRectangle)
        {
            if (ballPosition.Y >= paddleRectangle.Top - ball.Height)
            {
                if (BoundingRectangle.Center.X > paddleRectangle.Left && BoundingRectangle.Center.X < paddleRectangle.Right)
                {
                    ballPosition.Y = paddleRectangle.Top - ball.Height;
                    ballDirection.Y *= -1;
                }
            }
        }

        public void CheckForCollisionBetweenBallAndRectangle(Rectangle rectangle)
        {
            //Check if they intersect before doing any calculations
            if (BoundingRectangle.Intersects(rectangle))
            {
                Vector2 ballOverlap = RectangleHelper.GetSignedOverlap(BoundingRectangle, rectangle);

                //Check if the ball hits the left or right side
                if (Math.Abs(ballOverlap.Y) > Math.Abs(ballOverlap.X))
                {
                    //push the ball left or right
                    ballPosition.X += ballOverlap.X;
                    //reflect the ball
                    ballDirection.X *= -1;
                }
                else //The ball hits the top or bottom
                {
                    //push the ball up or down
                    ballPosition.Y += ballOverlap.Y;
                    //reflect the ball
                    ballDirection.Y *= -1;
                }

            }
        }
    }
}
