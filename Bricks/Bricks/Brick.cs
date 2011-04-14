using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    public class Brick
    {
        int pointValue;
        public int PointValue
        {
            get { return pointValue; }
        }

        Texture2D brickTexture;
        public Texture2D BrickTexture
        {
            get { return brickTexture; }
        }

        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { value = position; }
        }

        Color color;
        public Color Color
        {
            get { return color; }
        }

        bool hit;
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, brickTexture.Width, brickTexture.Height); }
        }

        public Brick(Texture2D BrickTexture, Vector2 Position, Color Color, int PointValue)
        {
            brickTexture = BrickTexture;
            position = Position;
            color = Color;
            pointValue = PointValue;
        }
    }
}
