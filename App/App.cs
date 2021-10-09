using ArbitraryCollisionRectangle.App.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArbitraryCollisionRectangle.App
{
    public abstract class App : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override sealed void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateGame(gameTime);
            base.Update(gameTime);
        }

        protected override sealed void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawSprites(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected abstract void UpdateGame(GameTime gameTime);

        protected abstract void DrawSprites(GameTime gameTime);
    }
}
