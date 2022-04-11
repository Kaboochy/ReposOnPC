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

namespace Poppers
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle window;
        Texture2D unpoppedTex;
        Texture2D poppedTex;

        List<Rectangle> kernels;
        List<Vector2> velocities;
        List<Texture2D> images;
        List<int> timers;

        Random rng = new Random();
        int randomVelX = 0;
        int randomVelY = 0;
        int randomLocX = 0;
        int randomLocY = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            window = new Rectangle(0, 0, screenWidth, screenHeight);

            kernels = new List<Rectangle>();
            velocities = new List<Vector2>();
            images = new List<Texture2D>();
            timers = new List<int>();
            //STARTING KERNAL 1
            kernels.Add(new Rectangle(70, 50, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);
            //STARTING KERNAL 2
            kernels.Add(new Rectangle(70, 110, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            unpoppedTex = Content.Load<Texture2D>("unpopped");
            poppedTex = Content.Load<Texture2D>("popped");
            //STARTING KERNAL 1
            images.Add(unpoppedTex);
            //STARTING KERNAL 2
            images.Add(unpoppedTex);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            for (int i = 0; i < kernels.Count; i++)
            {
                //BOUNCING
                int x = kernels[i].X + (int)velocities[i].X;
                int y = kernels[i].Y + (int)velocities[i].Y;
                kernels[i] = new Rectangle(x, y, kernels[i].Width, kernels[i].Height);
                if (kernels[i].Y + kernels[i].Height >= window.Bottom)
                {
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }
                if (kernels[i].Y + kernels[i].Height <= window.Top)
                {
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }
                if (kernels[i].X + kernels[i].Width <= window.Left)
                {
                    velocities[i] = new Vector2(velocities[i].Y, velocities[i].X * -1);
                }
                if (kernels[i].X + kernels[i].Width >= window.Right)
                {
                    velocities[i] = new Vector2(velocities[i].Y, velocities[i].X * -1);
                }
                //INTERSECTS
                for (int j = 0; j < kernels.Count; j++)
                {
                    if (kernels[i].Intersects(kernels[j]) && kernels[i] != kernels[j] && timers[i] == 0)
                    {
                        timers.Remove(i);
                        timers.Insert(i, 45);
                        images[i] = poppedTex;
                    }
                }
                //REMOVING
                for (int k = kernels.Count-1; k >= 0; k--)
                {
                    if (timers[k] == 1)
                    {
                        kernels.RemoveAt(k);
                        velocities.RemoveAt(k);
                        images.RemoveAt(k);
                        timers.RemoveAt(k);
                    }
                    if (timers[k] > 1)
                    {
                        timers[k] = timers[k] - 1;
                    }
                }
            }
            //RNG
            randomVelX = rng.Next(-3, 3);
            randomVelY = rng.Next(-3, 3);
            randomLocX = rng.Next(10, 790);
            randomLocY = rng.Next(10, 470);
            //ADDING
            if (gameTime.TotalGameTime.Milliseconds==0 && gameTime.TotalGameTime.Seconds%3==0)
            {
                kernels.Add(new Rectangle(randomLocX, randomLocY, 15, 15));
                velocities.Add(new Vector2(randomVelX, randomVelY));
                images.Add(unpoppedTex);
                timers.Add(0);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int i = 0; i < kernels.Count; i++)
            {
                spriteBatch.Draw(images[i], kernels[i], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}