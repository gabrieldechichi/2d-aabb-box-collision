using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public static class RectangleOverlap
    {
        public static bool PointRectangleOverlap(in Vector2 point, in RectangleF rect)
        {
            return point.X <= rect.BottomRight.X && point.X >= rect.TopLeft.X
                && point.Y <= rect.BottomRight.Y && point.Y >= rect.TopLeft.Y;
        }
    }
}