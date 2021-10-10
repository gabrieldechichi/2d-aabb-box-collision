using System;
using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public static class RectangleCollision
    {
        public static Vector2 CalculatePointRectangelPenetration(in Vector2 point, in RectangleF rect)
        {
            Vector2 penetration = Vector2.Zero;

            if (RectangleOverlap.PointRectangleOverlap(point, rect))
            {
                penetration.X = point.X > rect.Center.X ? -(rect.Right - point.X) : point.X - rect.Left;
                penetration.Y = point.Y > rect.Center.Y ? -(rect.Bottom - point.Y) : point.Y - rect.Top;

                if (Math.Abs(penetration.X) < Math.Abs(penetration.Y))
                {
                    penetration.Y = 0;
                }
                else
                {
                    penetration.X = 0;
                }
            }

            return penetration;
        }
    }
}