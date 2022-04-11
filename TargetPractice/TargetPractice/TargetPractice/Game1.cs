using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TargetPractice
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D crossText, tankText, squareText;
        Rectangle crossRect, tankRect, squareRect;
        Vector2 screenPos, origin;
        SpriteFont font1;
        int seconds;
        double x, y, degre, squareX, squareY, hype, dx, dy;
        string count;
        float rotationDegrees, rotationRadians;
        bool fire;
        MouseState oldmouse;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            x = 0;
            y = 0;
            dx = 0;
            dy = 0;
            squareX = 395;
            squareY = 246;
            seconds = 0;
            crossRect = new Rectangle((int)x, (int)y, 70, 70);
            tankRect = new Rectangle(400, 240, 100, 100);
            squareRect = new Rectangle((int)squareX,(int)squareY, 10, 10);
            rotationDegrees = 0;
            rotationRadians = 0;
            screenPos = new Vector2();
            origin = new Vector2();
            origin.X = 100;
            origin.Y = 100;
            screenPos.X = 400;
            screenPos.Y = 251;
            count = "";
            degre = 0;
            fire = false;
            hype = 0;
            oldmouse = Mouse.GetState();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            crossText = this.Content.Load<Texture2D>("crossh (1)");
            squareText = this.Content.Load<Texture2D>("White Square");
            tankText = Content.Load<Texture2D>("tank");
            font1 = Content.Load<SpriteFont>("SpriteFont1");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //TIME
            seconds = gameTime.TotalGameTime.Seconds;
            //MOUSE
            MouseState mouse = Mouse.GetState();
            crossRect.X = (int)x-(crossRect.Width/2);
            crossRect.Y = (int)y-(crossRect.Height/2);
            x = mouse.X;
            y = mouse.Y;
            //ROTATION
            rotationRadians = (float)Math.Atan2(y-tankRect.Y, x-tankRect.X);
            rotationDegrees = MathHelper.ToDegrees(rotationRadians);
            degre = rotationDegrees;
            if(degre<=0)
            {
                degre += 360;
            }
            count = Convert.ToString(degre);
            //SHOOTING
            squareRect.X = (int)squareX;
            squareRect.Y= (int)squareY;
            KeyboardState kb = Keyboard.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                if (fire)
                {
                    fire = false;
                    squareX = 395;
                    squareY = 246;
                }
                else
                    fire = true;
            }
            if(fire==true)
            {
                hype = Math.Sqrt(Math.Pow(mouse.X - tankRect.X, 2) * Math.Pow(mouse.Y - tankRect.Y, 2));
                dx = 100 * (mouse.X - tankRect.X) / hype;
                dy = 100 * (mouse.Y - tankRect.Y) / hype;
                squareX += dx;
                squareY += dy;
            }
            oldmouse = mouse;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(squareText, squareRect, Color.White);
            spriteBatch.Draw(crossText, crossRect, Color.White);
            spriteBatch.Draw(tankText, screenPos, null, Color.White, rotationRadians, origin,
                     0.4f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font1, count, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
