using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.App
{
    public static class ConversionUtilities
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}