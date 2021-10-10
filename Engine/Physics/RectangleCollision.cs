using System;
using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public static class RectangleCollision
    {
        public static Vector2 CalculatePointRectangelPenetration(in Vector2 point, in RectangleF rect)
        {
            var penetration = Vector2.Zero;

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

        /// <summary>
        /// Calculates penetration of r1 into r2
        /// </summary>
        public static Vector2 CalculateRectangleRectanglePenetration(in RectangleF r1, in RectangleF r2)
        {
            var penetration = Vector2.Zero;

            if (RectangleOverlap.RectangleRectangleOverlap(r1, r2))
            {
                var rightMinuLeft = r1.Right - r2.Left;
                var leftMinusRight = r1.Left - r2.Right;
                penetration.X = MathF.Abs(rightMinuLeft) < MathF.Abs(leftMinusRight) ? rightMinuLeft : leftMinusRight;

                var bottomMinusTop = r1.Bottom - r2.Top;
                var topMinusBottom = r1.Top - r2.Bottom;

                penetration.Y = MathF.Abs(bottomMinusTop) < MathF.Abs(topMinusBottom) ? bottomMinusTop : topMinusBottom;

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