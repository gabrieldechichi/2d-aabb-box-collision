using System;
using System.Diagnostics;
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
                var sign = SignFixed(dx);
                hit.Normal.X = sign;
                hit.Point.X = rect.Center.X + rect.Extents.X * sign;
                hit.Point.Y = point.Y;
                hit.Penetration.X = sign * px;
            }
            else
            {
                var sign = SignFixed(dy);
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
        public static Hit2d CalculateRectangleRectanglePenetration(in RectangleF r1, in RectangleF r2)
        {
            var hit = new Hit2d();

            var dx = r1.Center.X - r2.Center.X;
            var px = (r1.Extents.X + r2.Extents.X) - Math.Abs(dx);

            if (px <= 0)
            {
                Debug.WriteLine($"Px {px}, Dx {dx}");
                return hit;
            }

            var dy = r1.Center.Y - r2.Center.Y;
            var py = (r1.Extents.Y + r2.Extents.Y) - Math.Abs(dy);

            if (py <= 0)
            {
                Debug.WriteLine($"Py {py}, Dy {dy}");
                return hit;
            }

            if (px < py)
            {
                var sign = SignFixed(dx);
                hit.Normal.X = sign;
                hit.Point.X = r2.Center.X + r2.Extents.X * sign;
                hit.Point.Y = r1.Center.Y;
                hit.Penetration.X = px * sign;
            }
            else
            {
                var sign = SignFixed(dy);
                hit.Normal.Y = sign;
                hit.Point.Y = r2.Center.Y + r2.Extents.Y * sign;
                hit.Point.X = r1.Center.X;
                hit.Penetration.Y = py * sign;
            }

            return hit;
        }

        private static float SignFixed(float n)
        {
            return n >= 0 ? 1.0f : -1.0f;
        }
    }
}