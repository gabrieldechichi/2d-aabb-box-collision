using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public static class RectangleOverlap
    {
        public static bool PointRectangleOverlap(in Vector2 point, in RectangleF rect)
        {
            return point.X <= rect.Right && point.X >= rect.Left
                && point.Y <= rect.Bottom && point.Y >= rect.Top;
        }

        public static bool RectangleRectangleOverlap(in RectangleF r1, in RectangleF r2)
        {
            return !(r1.Right < r2.Left || r2.Right < r1.Left || r1.Top > r2.Bottom || r2.Top > r1.Bottom);
        }
    }
}