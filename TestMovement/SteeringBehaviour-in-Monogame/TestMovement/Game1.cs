using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TestMovement
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite;

        public BoidSwarmBehaviour boidSwarmBehaviour;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            targetSprite = Content.Load<Texture2D>("whiteDot");
            boidSwarmBehaviour = new BoidSwarmBehaviour(_spriteBatch, Content.Load<Texture2D>("Boid"));
            boidSwarmBehaviour.CreateBoids();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            boidSwarmBehaviour.BoidsFollowMouse();
            boidSwarmBehaviour.BoidsAvoidBoids();
            boidSwarmBehaviour.MoveBoidSwarm();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(targetSprite, new Vector2(150, 150), Color.White);
            boidSwarmBehaviour.DrawBoidSwarm();
            UtilDraw.DrawLineBetween(_spriteBatch, new Vector2(15, 150), new Vector2(350, 200), 7, Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}