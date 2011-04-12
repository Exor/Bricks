using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bricks
{
    public static class RectangleHelper
    {
        public static Vector2 GetSignedOverlap(Rectangle r1, Rectangle r2)
        {
            //find the center of each rectangle
            Vector2 r1Center = new Vector2(r1.X + r1.Width / 2, r1.Y + r1.Height / 2);
            Vector2 r2Center = new Vector2(r2.X + r2.Width / 2, r2.Y + r2.Height / 2);

            //signed distance from center of one rectangle to the other
            float xDistance = r1Center.X - r2Center.X;
            float yDistance = r1Center.Y - r2Center.Y;

            //Minimum distance from one rectangle to the other without colliding
            float xMinDistance = r1.Width / 2 + r2.Width / 2;
            float yMinDistance = r1.Height / 2 + r2.Height / 2;

            //if they do not collide return vector2.zero
            if (Math.Abs(xDistance) >= xMinDistance || Math.Abs(yDistance) >= yMinDistance)
                return Vector2.Zero;

            //get the amount of overlap (and make sure it is signed correctly)
            float overlapX = xDistance > 0 ? xMinDistance - xDistance : -xMinDistance - xDistance;
            float overlapY = yDistance > 0 ? yMinDistance - yDistance : -yMinDistance - yDistance;

            //return the amount of overlap
            return new Vector2(overlapX, overlapY);
        }
    }
}
