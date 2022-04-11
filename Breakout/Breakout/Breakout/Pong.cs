using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Breakout
{
    class Pong
    {
        public Rectangle top, bot, left, right, paddleRect, ballRect;
        KeyboardState oldKB;
        public double paddleX, ballSpeedX, ballSpeedY;
        private Brick[,] bricks;
        public Pong(Rectangle top, Rectangle bot, Rectangle left, Rectangle right, Brick[,] bricks)
        {
            ballSpeedX = 2;
            ballSpeedY = -3;
            paddleX = 240;
            ballRect = new Rectangle(500, 740, 20, 20);
            paddleRect = new Rectangle(800, 750, 120, 10);
            this.top = top;
            this.bot = bot;
            this.left = left;
            this.right = right;
            this.bricks = bricks;
        }
        public void Update(Game1 game)
        {
            KeyboardState kb = Keyboard.GetState();
            paddleRect.X = (int)paddleX;
            //ball
            ballRect.X += (int)ballSpeedX;
            ballRect.Y += (int)ballSpeedY;
            if (ballRect.Intersects(top))
                ballSpeedY *= -1;
            if (ballRect.Intersects(bot))
                game.GameState = State.end;
            if (ballRect.Intersects(right))
                ballSpeedX *= -1;
            if (ballRect.Intersects(left))
                ballSpeedX *= -1;
            //PADDLE
            if (kb.IsKeyDown(Keys.A))
            {
                paddleX -= 7;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                paddleX += 7;
            }
            if (ballRect.Intersects(paddleRect))
            {
                ballSpeedY *= -1;
            }
            //BRICK STUFF
            for (int r = 0; r < bricks.GetLength(0); r++)
            {
                for (int c = 0; c < bricks.GetLength(1); c++)
                {
                    Brick brick = bricks[r, c];
                    if (brick.Destroyed||brick.Color==Color.Black)
                    {
                        continue;
                    }
                    if (ballRect.Intersects(brick.Rect))
                    {
                        brick.Destroyed = true;
                        ballSpeedY *= -1;
                    }
                }
            }
            //OTHER
            oldKB = kb;
        }
        public void Draw(SpriteBatch balls, Texture2D text)
        {
            balls.Begin();
            balls.Draw(text, paddleRect, Color.White);
            balls.Draw(text, ballRect, Color.White);
            balls.End();
        }
    }
}