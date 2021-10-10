using System;
using ArbitraryCollisionRectangle.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App.Player
{
    public class CharacterMovement2D
    {
        private float gravity = 1000f;
        private float horizontalSpeed = 200.0f;
        private float jumpSpeed = 300.0f;

        private float maxFallSpeed = 10000.0f;

        private Vector2 velocity;

        public RectangleF Rect;

        public void Update(float dt)
        {
            velocity.Y += gravity * dt;
            velocity.Y = Math.Min(maxFallSpeed, velocity.Y);
            velocity.X = 0;
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = horizontalSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -horizontalSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                velocity.Y = -jumpSpeed;
            }

            var movement = velocity * dt;
            Rect.Center += movement;
        }
    }
}