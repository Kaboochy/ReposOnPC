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

namespace BORG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D turretText, tubeText, catText, mouseText, miniMouseText, crosshText, explotionText, greenText, blueText, black1Text, black2Text;
        Rectangle turretRect, tubeRect, catRect, screen, mouseRect, miniMouseRect, crosshRect, explotionRect, greenRect, blueRect, black1Rect, black2Rect;
        Color custom = Color.Green;
        KeyboardState oldKB;
        bool up, down, left, right, mouseUp, mouseDown, mouseRight, mouseLeft, exTimerBool;
        int lsu, lsuTotal, size, exTimer;
        string currentTube = "Tube Up";
        string lsuText;
        Random rng;
        double x, y, locationX, locationY, time, distance, dist, side, distanceSideways, miniX, miniY, crosshX, crosshY, explotionX, explotionY, timer, seconds, greenStretch, blueStretch;
        SpriteFont font1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // TODO: Add your initialization logic here
            x = 362;
            y = 225;
            exTimer = 0;
            locationX = 0;
            locationY = 0;
            timer = 0;
            seconds = 0;
            lsu = 1;
            lsuTotal = 100;
            size = 15;
            miniX = 0;
            miniY = 0;
            crosshX = 0;
            crosshY = 0;
            explotionX = -200;
            explotionY = -200;
            rng = new Random();
            up = false;
            down = false;
            right = false;
            left = false;
            mouseUp = false;
            mouseDown = false;
            mouseRight = false;
            mouseLeft = false;
            exTimerBool = false;
            time = 5;
            string lsuText = "notWorking";
            oldKB = Keyboard.GetState();
            turretRect = new Rectangle(340, 200, 100, 100);
            tubeRect = new Rectangle(340, 200, 100, 100);
            catRect = new Rectangle((int)x, (int)y, size, size);
            mouseRect = new Rectangle((int)locationX, (int)locationY, 100, 100);
            miniMouseRect = new Rectangle((int)miniX, (int)miniY, 30, 30);
            screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            crosshRect = new Rectangle((int)crosshX, (int)crosshY, 50, 50);
            explotionRect = new Rectangle((int)explotionX, (int)explotionY, 150, 150);
            black1Rect = new Rectangle(450, 380, 370, 75);
            black2Rect = new Rectangle(450, 420, 370, 75);
            greenStretch = 20;
            blueStretch = 100;
            blueRect = new Rectangle(570, 390, (int)blueStretch, 50);
            greenRect = new Rectangle(570, 430, (int)greenStretch, 50);
            time = 2;
            distance = 0;
            side = 0;
            distanceSideways = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            turretText = this.Content.Load<Texture2D>("Turret");
            tubeText = this.Content.Load<Texture2D>(currentTube);
            catText = this.Content.Load<Texture2D>("cat1");
            mouseText = this.Content.Load<Texture2D>("mouse1");
            font1 = Content.Load<SpriteFont>("SpriteFont1");
            miniMouseText = this.Content.Load<Texture2D>("mouse2");
            crosshText = this.Content.Load<Texture2D>("crossh");
            explotionText = this.Content.Load<Texture2D>("Explosion with Fragments");
            greenText = this.Content.Load<Texture2D>("GreenRectangle");
            blueText = this.Content.Load<Texture2D>("BlueRectangle");
            black1Text = this.Content.Load<Texture2D>("BlackRectangle1");
            black2Text = this.Content.Load<Texture2D>("BlackRectangle2");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            timer++;
            seconds = timer / 60;
            greenRect.Width = (int)greenStretch;
            blueRect.Width = (int)blueStretch;
            if (timer % 20 == 0 && lsuTotal < 100)
            {
                lsuTotal += 1;
                blueStretch += 10;
            }
            KeyboardState kb = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            catRect.X = (int)x;
            catRect.Y = (int)y;
            mouseRect.X = (int)locationX;
            mouseRect.Y = (int)locationY;
            miniMouseRect.X = (int)miniX;
            miniMouseRect.Y = (int)miniY;
            catRect.Width = size;
            catRect.Height = size;
            explotionRect.X = (int)explotionX;
            explotionRect.Y = (int)explotionY;
            lsuText = lsuTotal.ToString();
            base.Update(gameTime);
            crosshRect.X = (int)crosshX;
            crosshRect.Y = (int)crosshY;
            crosshX = mouse.X;
            crosshY = mouse.Y;
            if (kb.IsKeyDown(Keys.W) || (mouse.Y < 225 && mouse.X > 250 && mouse.X < 400))
            {
                tubeText = this.Content.Load<Texture2D>("Tube Up");
            }
            if (kb.IsKeyDown(Keys.A) || mouse.X < 300)
            {
                tubeText = this.Content.Load<Texture2D>("Tube Left");
            }
            if (kb.IsKeyDown(Keys.S) || (mouse.Y > 225 && mouse.X > 250 && mouse.X < 400))
            {
                tubeText = this.Content.Load<Texture2D>("Tube Down");
            }
            if (kb.IsKeyDown(Keys.D) || mouse.X > 400)
            {
                tubeText = this.Content.Load<Texture2D>("Tube Right");
            }
            if (right)
            {
                x += 5;
                custom = Color.Red;
            }
            if (left)
            {
                x -= 5;
                custom = Color.Red;
            }
            if (up)
            {
                y -= 5;
                custom = Color.Red;
            }
            if (down)
            {
                y += 5;
                custom = Color.Red;
            }
            //LOCATION OF MOUSE
            int distance = rng.Next(0, 140);
            int distanceSideways = rng.Next(0, 270);
            if (seconds % time == 0)
            {
                int side = rng.Next(0, 5);
                if (side == 1)
                {
                    //LEFT
                    locationY = 225;
                    locationX = distanceSideways;
                    miniY = 225;
                    miniX = distanceSideways;
                    mouseRight = true;
                    side = 0;
                }
                if (side == 2)
                {
                    //RIGHT
                    locationY = 225;
                    locationX = 780 - distanceSideways;
                    miniY = 225;
                    miniX = 780 - distanceSideways;
                    mouseLeft = true;
                    side = 0;
                }
                if (side == 3)
                {
                    //UP
                    locationX = 362;
                    locationY = distance;
                    miniX = 362;
                    miniY = distance;
                    mouseDown = true;
                    side = 0;
                }
                if (side == 4)
                {
                    //DOWN
                    locationX = 362;
                    locationY = 450 - distance;
                    miniX = 362;
                    miniY = 450 - distance;
                    mouseUp = true;
                    side = 0;
                }
            }
            //MINI MOUSE LAUNCHER

            //RESET OF CAT
            if (!screen.Contains(catRect) || mouseRect.Contains(catRect))
            {
                x = 362;
                y = 225;
                up = false;
                down = false;
                left = false;
                right = false;
                custom = Color.Green;
            }
            //EXPLOSION
            if (mouseRect.Contains(catRect))
            {
                explotionY = locationY - 30;
                explotionX = locationX - 30;
                locationX = -200;
                locationY = -200;
                miniX = -200;
                miniY = -200;
                exTimerBool = true;
            }
            if (exTimerBool)
            {
                exTimer++;
                if (exTimer == 30)
                {
                    explotionX = -200;
                    explotionY = -200;
                    exTimerBool = false;
                    exTimer = 0;
                }
            }
            //DIRECTION AND LSU
            if (y == 225 && x == 362)
            {
                if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space) && tubeText == this.Content.Load<Texture2D>("Tube Up"))
                {
                    up = true;
                    lsuTotal -= lsu;
                    blueStretch -= lsu * 10;
                }

                if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space) && tubeText == this.Content.Load<Texture2D>("Tube Left"))
                {
                    left = true;
                    lsuTotal -= lsu;
                    blueStretch -= 10;
                }

                if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space) && tubeText == this.Content.Load<Texture2D>("Tube Down"))
                {
                    down = true;
                    lsuTotal -= lsu;
                    blueStretch -= 10;
                }

                if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space) && tubeText == this.Content.Load<Texture2D>("Tube Right"))
                {
                    right = true;
                    lsuTotal -= lsu;
                    blueStretch -= 20;
                }
                if (kb.IsKeyDown(Keys.F1))
                {
                    lsu = 1;
                    size = 15;
                    greenStretch = 40;
                }
                if (kb.IsKeyDown(Keys.F2))
                {
                    lsu = 2;
                    size = 20;
                    greenStretch = 60;
                }
                if (kb.IsKeyDown(Keys.F3))
                {
                    lsu = 3;
                    size = 25;
                    greenStretch = 80;
                }
                if (kb.IsKeyDown(Keys.F4))
                {
                    lsu = 4;
                    size = 30;
                    greenStretch = 100;
                }
                if (kb.IsKeyDown(Keys.F5))
                {
                    lsu = 5;
                    size = 35;
                    greenStretch = 120;
                }
                if (kb.IsKeyDown(Keys.F6))
                {
                    lsu = 6;
                    size = 40;
                    greenStretch = 140;
                }
                if (kb.IsKeyDown(Keys.F7))
                {
                    lsu = 7;
                    size = 45;
                    greenStretch = 160;
                }
                if (kb.IsKeyDown(Keys.F8))
                {
                    lsu = 8;
                    size = 50;
                    greenStretch = 180;
                }
                if (kb.IsKeyDown(Keys.F9))
                {
                    lsu = 9;
                    size = 55; ;
                    greenStretch = 200;
                }
                //MOTION
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 1;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 2;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 3;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 4;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 5;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 6;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 7;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 8;
                if (kb.IsKeyDown(Keys.NumPad1))
                    distance = 9;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(catText, catRect, Color.White);
            spriteBatch.Draw(turretText, turretRect, Color.White);
            spriteBatch.Draw(tubeText, tubeRect, custom);
            spriteBatch.Draw(mouseText, mouseRect, Color.White);
            spriteBatch.Draw(miniMouseText, miniMouseRect, Color.White);
            spriteBatch.DrawString(font1, lsuText, new Vector2(372, 225), Color.Black);
            spriteBatch.Draw(explotionText, explotionRect, Color.White);
            spriteBatch.Draw(black1Text, black1Rect, Color.Black);
            spriteBatch.Draw(black2Text, black2Rect, Color.Black);
            spriteBatch.Draw(greenText, greenRect, Color.White);
            spriteBatch.Draw(blueText, blueRect, Color.Fuchsia);
            spriteBatch.Draw(crosshText, crosshRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
