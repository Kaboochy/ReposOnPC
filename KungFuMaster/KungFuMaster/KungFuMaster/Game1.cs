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

namespace KungFuMaster
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKB;
        Rectangle sourceRect, destRect, backgroundSourceRect, backgroundDestRect,
            projectileRectLeft, projectileRectRight, heartRect, heartRect2, heartRect3, heartRect4, heartRect5, hitbox, gameOverRect;
        Texture2D sailorText, backgroundText, heartTex, projectileLeft, projectileRight, hitboxText, gameOverText;
        List<Rectangle> heartListRect;
        Random rng = new Random();
        bool right, jump, crouch, gameOver, fall;
        int y, frames, ballsCounter, velLeft, velRight;
        int balls = 0;
        double x, sailorX, sailorY, backX;
        SoundEffect chunLi;

        //ENEMIES
        List<enemy> enemies;
        int dir, timer;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1066;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            x = 210;
            y = 30;
            frames = 0;
            sailorX = 400;
            sailorY = 444;
            backX = 400;
            velLeft = 0;
            velRight = 0;
            sourceRect = new Rectangle((int)x, y, 60, 110);
            destRect = new Rectangle((int)sailorX, (int)sailorY, 150, 150);
            backgroundSourceRect = new Rectangle(0, 0, 800, 450);
            backgroundDestRect = new Rectangle(0,200, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height-200);
            projectileRectLeft = new Rectangle(-50, 440, 50, 50);
            projectileRectRight = new Rectangle(1100, 440, 50, 50);
            hitbox = new Rectangle((int)sailorX, (int)sailorY, 120, 135);
            gameOverRect = new Rectangle(-500, 100, 400, 400);
            right = true;
            jump = false;
            gameOver = false;
            fall = false;
            ballsCounter = 4;
            heartListRect = new List<Rectangle>();
            heartRect = new Rectangle(800, 0, 50, 50);
            heartRect2 = new Rectangle(850, 0, 50, 50);
            heartRect3 = new Rectangle(900, 0, 50, 50);
            heartRect4 = new Rectangle(950, 0, 50, 50);
            heartRect5 = new Rectangle(1000, 0, 50, 50);

            //ENEMIES
            enemies = new List<enemy>();
            enemies.Add(new enemy());
            enemy.pPos = new Rectangle((int)sailorX, (int)sailorY, 150, 150);
            dir = 1;
            timer = 1;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sailorText = this.Content.Load<Texture2D>("sailor_spritesheet");
            backgroundText = this.Content.Load<Texture2D>("sidescroller");
            chunLi = Content.Load<SoundEffect>("ThemeOfChunLi");
            hitboxText = this.Content.Load<Texture2D>("hitbox");
            gameOverText = this.Content.Load<Texture2D>("gameOver");
            heartTex = Content.Load<Texture2D>("pinkHeart");
            heartListRect.Add(heartRect);
            heartListRect.Add(heartRect2);
            heartListRect.Add(heartRect3);
            heartListRect.Add(heartRect4);
            heartListRect.Add(heartRect5);
            projectileLeft = this.Content.Load<Texture2D>("Left");
            projectileRight = this.Content.Load<Texture2D>("Right");

            //ENEMIES
            enemy.sprite1 = Content.Load<Texture2D>("enemy-spritesheetL");
            enemy.sprite2 = Content.Load<Texture2D>("enemy-spritesheetR");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            sourceRect.X = (int)x;
            sourceRect.Y = y;
            destRect.X = (int)sailorX-20;
            destRect.Y = (int)sailorY;
            hitbox.X = (int)sailorX;
            hitbox.Y = (int)sailorY;
            backgroundSourceRect.X = (int)backX;
            frames++;
            timer--;
            if(gameOver == false)
            {
                //AUDIO
                /*
                if (frames == 1)
                    chunLi.Play();
                */
                KeyboardState kb = Keyboard.GetState();
                //WALKING RIGHT
                if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A))
                {
                    sailorText = this.Content.Load<Texture2D>("sailor_spritesheet");
                    y = 140;
                    if (right == false)
                        x = 270;
                    right = true;
                    if (frames % 7 == 0)
                    {
                        x += 60.5;
                        if (x >= 860)
                        {
                            x = 270;
                        }
                    }
                    backX += .7;
                    dir = 2;
                }
                //WALKING LEFT
                if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
                {
                    sailorText = this.Content.Load<Texture2D>("sailor_spritesheetLeft");
                    y = 140;
                    if (right)
                        x = 696;
                    right = false;
                    if (frames % 7 == 0)
                    {
                        x -= 62;
                        if (x <= 157)
                        {
                            x = 696;
                        }
                    }
                    backX -= .7;
                    dir = 0;
                }
                //IDLE STANCE + CROUCHING + PUNCHING
                if ((right && !kb.IsKeyDown(Keys.D)) || (right == false && !kb.IsKeyDown(Keys.A)))
                {
                    if (right && !kb.IsKeyDown(Keys.E) && !crouch)
                    {
                        x = 270;
                        y = 30;
                    }
                    if (right && kb.IsKeyDown(Keys.E))
                    {
                        x = 405;
                    }
                    if (!right && !kb.IsKeyDown(Keys.E))
                    {
                        x = 696;
                        y = 30;
                    }
                    if (!right && kb.IsKeyDown(Keys.E))
                    {
                        x = 550;
                        hitbox.X -=30;
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if (enemies[i].Contact(hitbox))
                            {
                                enemies.RemoveAt(i);
                            }
                        }
                    }
                    if (kb.IsKeyDown(Keys.S))
                        crouch = true;
                    else
                    {
                        crouch = false;
                        hitbox.Height = 135;
                        hitbox.Width = 120;
                    }
                    if (right && !kb.IsKeyDown(Keys.E) && crouch)
                    {
                        x = 745;
                        y = 910;
                        hitbox.Height = 75;
                        hitbox.Y = hitbox.Y + 50;
                    }
                    if (!right && !kb.IsKeyDown(Keys.E) && crouch)
                    {
                        x = 210;
                        y = 910;
                        hitbox.Height = 75;
                        hitbox.Y = hitbox.Y + 50;
                    }
                    dir = 1;
                }
                //JUMPING
                /*
                if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyDown(Keys.Space))
                {
                    jump = true;
                }
                else
                {
                    jump = false;
                    sailorY = 444;
                    sourceRect.Width = 60;
                }
                if (jump)
                {
                    sourceRect.Width = 80;
                    sailorY = 400;
                    y = 420;
                    if (right)
                    {
                        sailorText = this.Content.Load<Texture2D>("sailor_spritesheet");
                        x = 370;
                    }
                    if (!right)
                    {
                        sailorText = this.Content.Load<Texture2D>("sailor_spritesheetLeft");
                        x = 480;
                    } 
                }
                */
                //PROJECTILES
                projectileRectLeft.X += velLeft;
                projectileRectRight.X += velRight;
                if (frames % 500 == 0 && velLeft == 0 && velRight == 0)
                {
                    balls = rng.Next(0, 2);
                }
                if (balls == 0)
                {
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
                        velLeft = 4;
                    else
                        velLeft = 3;
                }
                if (balls == 1)
                {
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.D))
                        velRight = -4;
                    else
                        velRight = -3;
                }
                if (projectileRectRight.X <= -50)
                {
                    velRight = 0;
                    projectileRectRight.X = 1100;
                    balls = 100;
                }
                if (projectileRectLeft.X >= 1100)
                {
                    velLeft = 0;
                    projectileRectLeft.X = -50;
                    balls = 100;
                }
                if (projectileRectLeft.Intersects(hitbox) || projectileRectRight.Intersects(hitbox))
                {
                    heartListRect.RemoveAt(ballsCounter);
                    ballsCounter--;
                    projectileRectRight.X = 1100;
                    projectileRectLeft.X = -50;
                }
                oldKB = kb;
                //ENEMIES
                for(int i = 0;i < enemies.Count; i++)
                {
                    enemies[i].Update(dir);
                }
                if (timer == 0)
                {
                    enemies.Add(new enemy());
                    timer = rng.Next(1, 6)*100;
            }
            }
            //HEALTH
            if (ballsCounter == -1 && fall == false)
            {
                gameOver = true;
                x = 10;
                y = 530;
                sourceRect.Width += 35;
                fall = true;
            }
            if(gameOver)
            {
                sailorText = this.Content.Load<Texture2D>("sailor_spritesheet");
                if(fall)
                {
                    if (x <= 510 && frames % 7 == 0)
                    {
                        x += 105;
                    }
                }
                gameOverRect.X = 325;
            }
            //UPDATE
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundText, backgroundDestRect, backgroundSourceRect, Color.White);
            //spriteBatch.Draw(hitboxText, hitbox, Color.White); 
            spriteBatch.Draw(sailorText, destRect, sourceRect, Color.White);
            spriteBatch.Draw(projectileLeft, projectileRectLeft, Color.White);
            spriteBatch.Draw(projectileRight, projectileRectRight, Color.White);
            for (int i = 0; i < heartListRect.Count; i++)
            {
                spriteBatch.Draw(heartTex, heartListRect[i], Color.White);
            }
            spriteBatch.Draw(gameOverText, gameOverRect, Color.White);
            spriteBatch.End();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            base.Draw(gameTime);
        }
    }
}
