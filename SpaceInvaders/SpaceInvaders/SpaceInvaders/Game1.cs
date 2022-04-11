using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceInvaders
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int x = 0;
        int y = 0;
        bool right = false;
        Rectangle destRect;
        Invasion invaders;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            destRect = new Rectangle(100, 140, 100, 100);
            invaders = new Invasion(Content.Load<Texture2D>("Space Invaders"));
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            invaders.Update();
            if (x < 450&&right==true)
                x+=5;
            else
            {
                x -= 5;
                right = false;
            }
            if (x < 0)
                right = true;
            if (x == 450 && y < 950 || x == 0 && y<950)
                y = y + 25;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            invaders.Draw(spriteBatch, (float)x, (float)y);
            invaders.Draw(spriteBatch, (float)x + 50, (float)y);
            invaders.Draw(spriteBatch, (float)x + 100, (float)y);
            invaders.Draw(spriteBatch, (float)x + 150, (float)y);
            invaders.Draw(spriteBatch, (float)x + 200, (float)y);
            invaders.Draw(spriteBatch, (float)x + 250, (float)y);
            invaders.Draw(spriteBatch, (float)x + 300, (float)y);
            invaders.Draw(spriteBatch, (float)x + 350, (float)y);
            invaders.Draw(spriteBatch, (float)x + 400, (float)y);
            invaders.Draw(spriteBatch, (float)x + 450, (float)y);
            invaders.Draw(spriteBatch, (float)x + 500, (float)y);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
