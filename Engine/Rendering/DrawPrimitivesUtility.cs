using System;
using ArbitraryCollisionRectangle.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArbitraryCollisionRectangle.Engine.Rendering
{
    public static class DrawPrimitivesUtility
    {
        private static Texture2D pointTexture;

        private static Texture2D GetOrCreatePointTexture(SpriteBatch spriteBatch)
        {
            if (pointTexture == null)
            {
                pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pointTexture.SetData<Color>(new Color[] { Color.White });
            }
            return pointTexture;
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 from, Vector2 to, Color color, float lineWidth = 1f)
        {
            var distance = Vector2.Distance(from, to);
            var angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            DrawLine(spriteBatch, from, distance, angle, color, lineWidth);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 from, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            var pointTex = GetOrCreatePointTexture(spriteBatch);
            spriteBatch.Draw(pointTex, from, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            var pointTex = GetOrCreatePointTexture(spriteBatch);
            spriteBatch.Draw(pointTex, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(pointTex, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(pointTex, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(pointTex, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }

        public static void DrawPoint(SpriteBatch spriteBatch, Point point, Color color, int halfSize)
        {
            var pointTex = GetOrCreatePointTexture(spriteBatch);
            spriteBatch.Draw(pointTex, new Rectangle(point.X - halfSize, point.Y - halfSize, halfSize, halfSize), color);
        }
    }
}