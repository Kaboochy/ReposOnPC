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

namespace huffpuff
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKB;
        Rectangle sourceRect, destRect, featherSourceRect, featherDestRect, screen;
        Texture2D featherText, kidText;
        bool left, right, movingAnimation, fall, hit, gameEnd;
        int x, frames, rectDir, featherX, fDirection;
        double kidPos, seconds, kidVert, fPosX, fPosY;
        Random rng;
        Color mycolor;
        string endText;
        SpriteFont Font1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            featherX = 0;
            fDirection = 1;
            fPosY = 10;
            fPosX = 400;
            rng = new Random();
            x = 0;
            gameEnd = false;
            mycolor = Color.CornflowerBlue;
            rectDir = 35;
            frames = 0;
            kidPos = 400;
            kidVert = 385;
            sourceRect = new Rectangle(x, 0, 25, rectDir);
            destRect = new Rectangle((int)kidPos, (int)kidVert, 100, 100);
            featherSourceRect = new Rectangle(featherX, 70, 25, 35);
            featherDestRect = new Rectangle((int)fPosX, (int)fPosY, 100, 100);
            screen = new Rectangle(0, 490, GraphicsDevice.Viewport.Width, 2);
            left = false;
            hit = false;
            movingAnimation = true;
            right = false;
            endText = "";
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            kidText = this.Content.Load<Texture2D>("HuffNPuff");
            featherText = this.Content.Load<Texture2D>("HuffNPuff");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //INPUT FOR LEFT AND RIGHT
            sourceRect.X = x;
            featherSourceRect.X = featherX;
            destRect.X = (int)kidPos;
            destRect.Y = (int)kidVert;
            featherDestRect.X = (int)fPosX;
            featherDestRect.Y = (int)fPosY;
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.D))
            {
                kidPos += 4;
                right = true;
                left = false;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                kidPos -= 4;
                left = true;
                right = false;
            }
            oldKB = kb;
            //WALKING ANIMATION
            sourceRect.Y = rectDir;
            seconds = gameTime.TotalGameTime.Seconds;
            if(gameEnd==false)
            { 
            frames++;
            if (right)
                rectDir = 35;
            if (left)
                rectDir = 0;
            if (frames % 20 == 0 && movingAnimation == true)
            {
                x += 25;
                featherX += 25;
                if (x >= 100)
                {
                    x = 25;
                    featherX = 25;
                }
                kidVert = 385;
            }
            //JUMPING ANIMATION
            if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyDown(Keys.Space))
                movingAnimation = false;
            if (movingAnimation == false)
            {
                x = 100;
                kidVert = 360;
                if (frames % 10 == 0)
                {
                    movingAnimation = true;
                }
            }
            //FEATHER
            if (fPosY <= 100)
            {
                fall = true;
                hit = false;
            }
            if (fall)
                fPosY += 2;
            //FEATHER DIRECTION
            if (fDirection == 1)
                fPosX -= 2;
            if (fDirection == 2)
                fPosX += 2;
            //FEATHER BORDERS
            if (fPosX <= 5)
                fPosX += 2;
            if (fPosX >= 750)
                fPosX -= 2;
            //FEATHER HIT
            if (movingAnimation == false && featherDestRect.Intersects(destRect))
            {
                hit = true;
                fall = false;
                fDirection = rng.Next(0, 3);
            }
            if (hit)
                fPosY -= 5;
        }
            //GAME END
            if (featherDestRect.Intersects(screen))
                gameEnd = true;
            if(gameEnd)
            {
                mycolor = Color.Black;
                endText = "GAME OVER - PRESS R TO RESET";
                if (kb.IsKeyDown(Keys.R))
                {
                    gameEnd = false;
                    fPosY = 10;
                    mycolor = Color.CornflowerBlue;
                    endText = "";
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(mycolor);
            spriteBatch.Begin();
            spriteBatch.Draw(kidText, destRect, sourceRect, Color.White);
            spriteBatch.Draw(featherText, featherDestRect, featherSourceRect, Color.White);
            spriteBatch.DrawString(Font1, endText, new Vector2(400, 70), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
