using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestRectangleRectangleCollision : App
    {
        private RectangleF mouseRect;
        private RectangleF testRect;

        protected override void Initialize()
        {
            mouseRect = new RectangleF(Vector2.Zero, Vector2.One * 20);
            testRect = new RectangleF(
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) * 0.5f,
                new Vector2(100, 50));

            base.Initialize();
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            mouseRect.Center = Mouse.GetState(Window).Position.ToVector2();
            var penetration = RectangleCollision.CalculateRectangleRectanglePenetration(mouseRect, testRect);
            mouseRect.Center -= penetration;
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            var isOverlap = RectangleOverlap.RectangleRectangleOverlap(mouseRect, testRect);
            var drawColor = isOverlap ? Color.Green : Color.Red;
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)testRect, drawColor, 2);

            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)mouseRect, Color.Black, 2);
            DrawPrimitivesUtility.DrawPoint(spriteBatch, mouseRect.TopLeft.ToPoint(), Color.Red, 5);
        }
    }
}