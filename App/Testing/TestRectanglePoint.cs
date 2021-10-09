using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestRectanglePoint : App
    {
        private RectangleF testRect;

        protected override void Initialize()
        {
            testRect = new RectangleF(
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) * 0.5f,
                new Vector2(100, 50));

            base.Initialize();
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            var mousePos = Mouse.GetState(Window).Position;
            var isOverlap = RectangleOverlap.PointRectangleOverlap(new Vector2(mousePos.X, mousePos.Y), testRect);
            var drawColor = isOverlap ? Color.Green : Color.Red;
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)testRect, drawColor, 2);
        }

        protected override void UpdateGame(GameTime gameTime)
        {

        }
    }
}