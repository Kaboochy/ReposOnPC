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

namespace CANIPUTMYBALLSINYOURJAW
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle redRect, redRect, blueLineRect, redLineRect;//, front;
        Texture2D redText, redText, redLineText, redLineText;//, frontText;
        double rX, rY, rX, rY; //, lineX, lineY;
        float rotationDegreesRed, rotationRadiansRed;
        int vXRed, vYRed;
        bool on = false;
        KeyboardState oldKB;

        //BLUE TRAIL STUFF
        List<Rectangle> trailsRed;
        List<Texture2D> imagesRed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            rotationDegreesRed = 0;
            rotationRadiansRed = 0;
            rX = 50;
            rY = 40;
            rX = 700;
            rY = 420;
            vXRed = 1;
            vYRed = 0;
            //lineY = 0;
            //lineX = rX + 60;
            redRect = new Rectangle((int)rX, (int)rY, 60, 40);
            redRect = new Rectangle((int)rX, (int)rY, 60, 40);

            //BLUE TRAIL STUFF
            trailsRed = new List<Rectangle>();
            imagesRed = new List<Texture2D>();
            trailsRed.Add(new Rectangle((int)rX - 2, (int)rY - 2, 5, 5));

            //BALLS
            //front = new Rectangle((int)rX + 60, (int)rY, 5, 5);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //BLUE
            redText = this.Content.Load<Texture2D>("BlueBoke");
            redLineText = this.Content.Load<Texture2D>("Blue");
            imagesRed.Add(redLineText);
            //RED
            redText = this.Content.Load<Texture2D>("BlueBoke");
            redLineText = this.Content.Load<Texture2D>("Blue");
            //FRONT SHIT
            //frontText = this.Content.Load<Texture2D>("Blue");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState kb = Keyboard.GetState();
            redRect.X = (int)rX;
            redRect.Y = (int)rY;
            redRect.X = (int)rX;
            redRect.Y = (int)rY;

            //front.X = (int)lineX;
            //front.Y = (int)lineY;
            //STARTING MOVEMENT
            rX += vXRed;
            rY += vYRed;
            //lineX = rX+60;
            //lineY = rY;
            //BLUE RIGHT
            if (kb.IsKeyDown(Keys.J) && !oldKB.IsKeyDown(Keys.J))
            {
                rotationDegreesRed += 90;
                on = true;
            }
            //BLUE LEFT
            if (kb.IsKeyDown(Keys.L) && !oldKB.IsKeyDown(Keys.L))
            {
                rotationDegreesRed -= 90;
                on = true;
            }
            //VELOCITIES
            if (rotationDegreesRed % 360 == 0 && on) //RIGHT
            {
                vXRed = 1;
                vYRed = 0;
            }
            if (rotationDegreesRed % 90 == 0 && on && rotationDegreesRed % 360 != 0
                && rotationDegreesRed % 180 != 0 && rotationDegreesRed % 270 != 0
                && rotationDegreesRed != 0) //DOWN
            {
                vXRed = 0;
                vYRed = 1;
            }
            if (rotationDegreesRed % 180 == 0 && on && rotationDegreesRed % 360 != 0 && rotationDegreesRed != 0) //LEFT
            {
                vXRed = -1;
                vYRed = 0;
            }
            if (rotationDegreesRed % 270 == 0 && on && rotationDegreesRed != 0) //UP
            {
                vXRed = 0;
                vYRed = -1;
            }
            if (rotationDegreesRed > 360)
                rotationDegreesRed = 90;
            if (rotationDegreesRed < -360)
                rotationDegreesRed = 270;
            if (rotationDegreesRed == -90)
                rotationDegreesRed = 270;
            //CONVERSION
            rotationRadiansRed = MathHelper.ToRadians(rotationDegreesRed);
            //ELSE
            oldKB = kb;


            //TRAIL
            if (gameTime.TotalGameTime.Seconds % 1 == 0)
            {
                trailsRed.Add(new Rectangle((int)rX - 2, (int)rY - 2, 5, 5));
                imagesRed.Add(redLineText);
            }

            //INTERSECTION
            /*
            for (int j = 0; j < trailsRed.Count; j++)
            {
                if (front.Intersects(trailsRed[j]))
                {
                    redText= this.Content.Load<Texture2D>("Blue"); 
                }
            }
            */
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            Vector2 origin = new Vector2(720, 300);
            float layerDepth = 0.0f;
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            //BLUE
            for (int i = 0; i < trailsRed.Count; i++)
            {
                spriteBatch.Draw(imagesRed[i], trailsRed[i], Color.Blue);
            }
            spriteBatch.Draw(redText, redRect, null, Color.Red, rotationRadiansRed, origin, SpriteEffects.None, layerDepth);
            //spriteBatch.Draw(frontText, front, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
