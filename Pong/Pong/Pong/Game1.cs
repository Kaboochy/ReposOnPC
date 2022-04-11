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

namespace Pong
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ballTex, squaresText, paddleLeftText, paddleRightText;
        Rectangle ballRect, top, bottom, left, right, squaresRect, paddleRightRect, paddleLeftRect;
        KeyboardState oldKB;
        SpriteFont Font1;
        string endText, leftText, rightText;
        bool gameEnd;
        double leftY, rightY, ballSpeedX, ballSpeedY, seconds, leftScore, rightScore;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            top = new Rectangle(0, 0, screenWidth, 5);
            bottom = new Rectangle(0, screenHeight, screenWidth, 20);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 0, screenHeight);
            ballRect = new Rectangle(50, 50, 20, 20);
            squaresRect = new Rectangle(350, 4, 100, 480);
            ballSpeedX = 2;
            ballSpeedY = 3;
            leftY = 240;
            rightY = 240;
            paddleRightRect = new Rectangle(770, (int)rightY, 20, 120);
            paddleLeftRect = new Rectangle(30, (int)rightY, 20, 120);
            gameEnd = false;
            endText = "";
            leftScore = 0;
            rightScore = 0;
            leftText = "0";
            rightText = "0";
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTex = Content.Load<Texture2D>("pingpongsquare");
            squaresText = Content.Load<Texture2D>("squares");
            paddleLeftText = Content.Load<Texture2D>("pingpongsquare");
            paddleRightText = Content.Load<Texture2D>("pingpongsquare");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState kb = Keyboard.GetState();
            paddleLeftRect.Y = (int)leftY;
            paddleRightRect.Y = (int)rightY;
            seconds = gameTime.TotalGameTime.Seconds;
            //ball
            ballRect.X += (int)ballSpeedX;
            ballRect.Y += (int)ballSpeedY;
            if (ballRect.Intersects(top))
                ballSpeedY *= -1;
            if (ballRect.Intersects(bottom))
                ballSpeedY *= -1;
            //LEFT PADDLE
            if (kb.IsKeyDown(Keys.W))
                leftY -= 7;
            if (kb.IsKeyDown(Keys.S))
                leftY += 7;
            if (ballRect.Intersects(paddleLeftRect))
                ballSpeedX *= -1;
            //RIGHT PADDLE
            if (kb.IsKeyDown(Keys.I))
                rightY -= 7;
            if (kb.IsKeyDown(Keys.K))
                rightY += 7;
            if (ballRect.Intersects(paddleRightRect))
                ballSpeedX *= -1;
            //DONE
            if (ballRect.Intersects(left))
            {
                gameEnd = true;
                rightScore += 1;
                rightText = Convert.ToString(rightScore);
                ballSpeedX *= -1;
            }
            if (ballRect.Intersects(right))
            {
                gameEnd = true;
                leftScore += 1;
                leftText = Convert.ToString(leftScore);
            }
            if (gameEnd)
            {
                ballRect.X = 50;
                ballRect.Y = 50;
                gameEnd = false;
            }
            oldKB = kb;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(squaresText, squaresRect, Color.Blue);
            spriteBatch.Draw(paddleLeftText, paddleLeftRect, Color.Blue);
            spriteBatch.Draw(paddleRightText, paddleRightRect, Color.Blue);
            spriteBatch.Draw(ballTex, top, Color.White);
            spriteBatch.Draw(ballTex, ballRect, Color.White);
            spriteBatch.DrawString(Font1, leftText, new Vector2(300, 10), Color.Blue);
            spriteBatch.DrawString(Font1, rightText, new Vector2(450, 10), Color.Blue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
