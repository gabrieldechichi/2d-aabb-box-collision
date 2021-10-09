using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public struct RectangleF
    {
        public Vector2 TopLeft;
        public Vector2 Size;

        public Vector2 BottomRight => TopLeft + Size;

        public float Right => BottomRight.X;
        public float Left => TopLeft.X;
        public float Bottom => BottomRight.Y;
        public float Top => TopLeft.Y;

        public Vector2 Center
        {
            set => TopLeft = value - Size * 0.5f;
        }

        public RectangleF(Vector2 center, Vector2 size)
        {
            TopLeft = center - size * 0.5f;
            Size = size;
        }

        public static explicit operator Rectangle(RectangleF rect) => new Rectangle((int)rect.TopLeft.X, (int)rect.TopLeft.Y, (int)rect.Size.X, (int)rect.Size.Y);

    }
}