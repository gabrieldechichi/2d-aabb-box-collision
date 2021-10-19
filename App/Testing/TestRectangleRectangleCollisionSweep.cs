using System.Diagnostics;
using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestRectangleRectangleCollisionSweep : App
    {
        private RectangleF dynamicRect;
        private RectangleF staticRect;

        private float speed = 200;

        private Vector2 velocity = Vector2.Zero;

        protected override void Initialize()
        {
            dynamicRect = new RectangleF(Vector2.Zero, Vector2.One * 20);
            staticRect = new RectangleF(
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) * 0.5f,
                new Vector2(100, 50));

            base.Initialize();
        }

        protected override void UpdateGame(GameTime gameTime)
        {

        }

        private Sweep2D UpdateDynRect(GameTime gameTime)
        {
            velocity = Vector2.Zero;
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = speed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -speed;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y = -speed;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y = speed;
            }

            var delta = velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var sweep = RectangleCollision.RectRectSweep(dynamicRect, delta, staticRect);
            dynamicRect.Center = sweep.Position;

            return sweep;
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            var sweep = UpdateDynRect(gameTime);
            var hit = sweep.Hit;

            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)staticRect, Color.Red, 2);

            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)dynamicRect, Color.Black, 2);

            if (hit.DidHit)
            {
                DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point + hit.Normal * 10, Color.Blue, 2);
                DrawPrimitivesUtility.DrawLine(spriteBatch, hit.Point, hit.Point - hit.Penetration, Color.Red, 2);
            }
        }
    }
}