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