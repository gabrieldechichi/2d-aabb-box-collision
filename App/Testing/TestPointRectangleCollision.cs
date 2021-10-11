using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestPointRectangleCollision : App
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
            var hit = RectangleCollision.CalculatePointRectangelPenetration(mousePos.ToVector2(), testRect);

            var drawColor = hit.DidHit ? Color.Green : Color.Red;
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)testRect, drawColor, 2);

            var adjustedPos = mousePos.ToVector2() + hit.Penetration;
            DrawPrimitivesUtility.DrawPoint(spriteBatch, adjustedPos.ToPoint(), Color.Red, 5);

            DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point + hit.Normal * 10, Color.Blue, 2);
            DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point - hit.Penetration, Color.Red, 2);
        }

        protected override void UpdateGame(GameTime gameTime)
        {

        }
    }
}