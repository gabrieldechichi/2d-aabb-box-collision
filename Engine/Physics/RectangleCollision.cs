using System;
using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public struct Hit2d
    {
        public Vector2 Point;
        public Vector2 Normal;
        public Vector2 Penetration;

        public bool DidHit => Penetration != Vector2.Zero;
    }
    public static class RectangleCollision
    {
        public static Hit2d CalculatePointRectangelPenetration(in Vector2 point, in RectangleF rect)
        {
            var hit = new Hit2d();

            var dx = point.X - rect.Center.X;
            var px = rect.Extents.X - Math.Abs(dx);

            if (px <= 0)
            {
                return hit;
            }

            var dy = point.Y - rect.Center.Y;
            var py = rect.Extents.Y - Math.Abs(dy);

            if (py <= 0)
            {
                return hit;
            }

            if (px < py)
            {
                var sign = Math.Sign(dx);
                hit.Normal.X = sign;
                hit.Point.X = rect.Center.X + rect.Extents.X * sign;
                hit.Point.Y = point.Y;
                hit.Penetration.X = sign * px;
            }
            else
            {
                var sign = Math.Sign(dy);
                hit.Normal.Y = sign;
                hit.Point.Y = rect.Center.Y + rect.Extents.Y * sign;
                hit.Point.X = point.X;
                hit.Penetration.Y = sign * py;
            }

            return hit;
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