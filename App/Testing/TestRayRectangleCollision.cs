using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestRayRectangleCollision : App
    {
        private RectangleF testRect;
        private Vector2 rayStart;

        private SpriteFont font;

        protected override void Initialize()
        {
            testRect = new RectangleF(
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) * 0.5f,
                new Vector2(100, 50));

            font = Content.Load<SpriteFont>("DefaultFont");

            base.Initialize();
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            var mousePos = Mouse.GetState(Window).Position;

            var ray = new Ray2D
            {
                Start = rayStart,
                End = mousePos.ToVector2()
            };

            var hit = RectangleCollision.CalculateRayRectangleCollision(ray, testRect);


            var drawColor = hit.DidHit ? Color.Green : Color.Red;
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)testRect, drawColor, 2);

            DrawPrimitivesUtility.DrawPoint(spriteBatch, ray.Start.ToPoint(), Color.Red, 5);
            DrawPrimitivesUtility.DrawPoint(spriteBatch, ray.End.ToPoint(), Color.Red, 5);
            DrawPrimitivesUtility.DrawLine(spriteBatch, ray.Start, ray.End, Color.Blue, 2);

            DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point + hit.Normal * 10, Color.Blue, 2);
            DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point - hit.Penetration, Color.Red, 2);
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            var mouseState = Mouse.GetState(Window);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                rayStart = mouseState.Position.ToVector2();
            }
        }
    }
}