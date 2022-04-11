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

namespace LightCycle
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle blueRect, redRect, blueLineRect, redLineRect, gameOverRect;
        Texture2D blueText, redText, blueLineText, redLineText, gameOverText;
        double bX, bY, rX, rY; //, lineX, lineY;
        float rotationDegrees, rotationRadians;
        float rotationDegreesRed, rotationRadiansRed;
        int vX, vY;
        int vXRed, vYRed;
        bool on = false;
        KeyboardState oldKB;

        //BLUE TRAIL STUFF
        List<Rectangle> trails;
        List<Texture2D> images;

        //RED TRAIL STUFF
        List<Rectangle> trailsRed;
        List<Texture2D> imagesRed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            rotationDegrees = 0;
            rotationRadians = 0;
            bX = 50;
            bY = 40;
            rX = 50;
            rY = 420;
            vX = 1;
            vY = 0;
            vXRed = 1;
            vYRed = 0;
            //lineY = 0;
            //lineX = bX + 60;
            blueRect = new Rectangle((int)bX, (int)bY, 60, 40);
            redRect = new Rectangle((int)rX, (int)rY, 60, 40);

            //BLUE TRAIL STUFF
            trails = new List<Rectangle>();
            images = new List<Texture2D>();
            trails.Add(new Rectangle((int)bX-2, (int)bY-2, 5, 5));

            //BLUE TRAIL STUFF
            trailsRed = new List<Rectangle>();
            imagesRed = new List<Texture2D>();
            trailsRed.Add(new Rectangle((int)rX - 2, (int)rY - 2, 5, 5));
            //BALLS
            //front = new Rectangle((int)bX + 60, (int)bY, 5, 5);
            gameOverRect = new Rectangle(-1000, 0, 800, 480);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //BLUE
            blueText = this.Content.Load<Texture2D>("BlueBoke");
            blueLineText = this.Content.Load<Texture2D>("Blue");
            images.Add(blueLineText);
            //RED
            redText = this.Content.Load<Texture2D>("BlueBokeCopy");
            redLineText = this.Content.Load<Texture2D>("Blue");
            imagesRed.Add(redLineText);
            //frontText = this.Content.Load<Texture2D>("Blue");
            gameOverText = this.Content.Load<Texture2D>("gameOVER");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState kb = Keyboard.GetState();
            blueRect.X = (int)bX;
            blueRect.Y = (int)bY;
            redRect.X = (int)rX;
            redRect.Y = (int)rY;

            //front.X = (int)lineX;
            //front.Y = (int)lineY;
            //STARTING MOVEMENT
            bX += vX;
            bY += vY;
            rX += vXRed;
            rY += vYRed;
            //lineX = bX+60;
            //lineY = bY;
            //BLUE RIGHT
            if (kb.IsKeyDown(Keys.D) && !oldKB.IsKeyDown(Keys.D))
            {
                rotationDegrees += 90;
                on = true;
            }
            //BLUE LEFT
            if (kb.IsKeyDown(Keys.A) && !oldKB.IsKeyDown(Keys.A))
            {
                rotationDegrees -= 90;
                on = true;
            }
            //VELOCITIES
            if (rotationDegrees%360==0 && on) //RIGHT
            {
                vX = 1;
                vY = 0;
            }
            if (rotationDegrees %90 == 0 && on && rotationDegrees % 360 != 0
                && rotationDegrees % 180 != 0 && rotationDegrees % 270 != 0
                &&rotationDegrees!=0) //DOWN
            {
                vX = 0;
                vY = 1;
            }
            if (rotationDegrees %180 == 0 && on && rotationDegrees %360!=0 && rotationDegrees != 0) //LEFT
            {
                vX = -1;
                vY = 0;
            }
            if (rotationDegrees %270 == 0 && on && rotationDegrees != 0) //UP
            {
                vX = 0;
                vY = -1;
            }
            if(rotationDegrees>360)
                rotationDegrees = 90;
            if (rotationDegrees < -360)
                rotationDegrees = 270;
            if (rotationDegrees == -90)
                rotationDegrees = 270;
            //CONVERSION
            rotationRadians = MathHelper.ToRadians(rotationDegrees);
            //ELSE




            //REEEEEED
            if (kb.IsKeyDown(Keys.L) && !oldKB.IsKeyDown(Keys.L))
            {
                rotationDegreesRed += 90;
                on = true;
            }
            //BLUE LEFT
            if (kb.IsKeyDown(Keys.J) && !oldKB.IsKeyDown(Keys.J))
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
            oldKB = kb;


            //TRAIL
            if (gameTime.TotalGameTime.Seconds % 1 == 0)
            {
                trails.Add(new Rectangle((int)bX - 2, (int)bY - 2, 5, 5));
                images.Add(blueLineText);
            }
            //TRAIL RED
            if (gameTime.TotalGameTime.Seconds % 1 == 0)
            {
                trailsRed.Add(new Rectangle((int)rX - 2, (int)rY - 2, 5, 5));
                imagesRed.Add(redLineText);
            }
            //INTERSECTION
            for (int j = 0; j < trails.Count; j++)
            {
                for (int k = 0; k < trailsRed.Count; k++)
                {
                    if (trailsRed[k].Intersects(trails[j]))
                    {
                        gameOverRect.X = 0;
                    }
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            Vector2 origin = new Vector2(720, 300);
            float layerDepth = 0.0f;
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            //BLUE
            for (int i = 0; i < trails.Count; i++)
            {
                spriteBatch.Draw(images[i], trails[i], Color.Blue);
            }
            spriteBatch.Draw(blueText, blueRect, null, Color.White, rotationRadians, origin, SpriteEffects.None, layerDepth);
            for (int i = 0; i < trailsRed.Count; i++)
            {
                spriteBatch.Draw(imagesRed[i], trailsRed[i], Color.Red);
            }
            spriteBatch.Draw(redText, redRect, null, Color.Red, rotationRadiansRed, origin, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(gameOverText, gameOverRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
