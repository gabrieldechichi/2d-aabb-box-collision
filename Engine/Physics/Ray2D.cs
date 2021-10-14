using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public struct Ray2D
    {
        public Vector2 Start;
        public Vector2 End;

        public Vector2 StartToEnd => End - Start;

        public Vector2 GetPoint(float t)
        {
            return Start + StartToEnd * t;
        }
    }
}