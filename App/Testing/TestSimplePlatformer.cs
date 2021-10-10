using ArbitraryCollisionRectangle.App.Player;
using ArbitraryCollisionRectangle.Engine.Physics;
using ArbitraryCollisionRectangle.Engine.Rendering;
using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.App.Testing
{
    public class TestSimplePlatformer : App
    {
        private CharacterMovement2D character;

        private RectangleF ground;

        protected override void Initialize()
        {
            ground = new RectangleF(
                new Vector2(GraphicsDevice.Viewport.Width * 0.5f, GraphicsDevice.Viewport.Height * 0.8f),
                new Vector2(GraphicsDevice.Viewport.Width * 2, 20));

            ground.Size.Y = 100;

            character = new CharacterMovement2D
            {
                Rect = new RectangleF(new Vector2(GraphicsDevice.Viewport.Width * 0.5f, 0), new Vector2(20, 30))
            };
            base.Initialize();
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            character.Update(dt);
            var penetration = RectangleCollision.CalculateRectangleRectanglePenetration(character.Rect, ground);
            character.Rect.Center -= penetration;
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)ground, Color.Black, 2);
            DrawPrimitivesUtility.DrawRectangle(spriteBatch, (Rectangle)character.Rect, Color.Red, 3);
        }
    }
}