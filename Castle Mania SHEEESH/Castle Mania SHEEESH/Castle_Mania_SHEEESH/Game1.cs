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

namespace Castle_Mania_SHEEESH
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundText;
        Rectangle backgroundRect;
        Texture2D dogText;
        Rectangle dogRect;
        double dogX, dogY;
        Texture2D ghostText;
        Rectangle ghostRect;
        double ghostX, ghostY;
        Texture2D knightText;
        Rectangle knightRect;
        double knightX, knightY;
        Texture2D heraldText;
        Rectangle heraldRect;
        double heraldX, heraldY;
        Texture2D featherText;
        Rectangle featherRect;
        double featherX, featherY;
        Texture2D moonText;
        Rectangle moonRect;
        double moonX, moonY;
        Texture2D sunText;
        Rectangle sunRect;
        double sunX, sunY;
        private int timer = 0;
        private int seconds = 0;
        private int cycle = 0;
        int r;
        int g;
        int b;
        SpriteFont Font1;
        String screenText;
        String deezText;
        private Color sky;
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
            screenText = "Who is deez?";
            deezText = "";
            cycle = 0;
            timer = 0;
            seconds = 0;
            r = 200;
            g = 255;
            b = 255;
            dogX = 500;
            dogY = 400;
            ghostX = -100;
            ghostY = 300;
            knightX = 250;
            knightY = 400;
            heraldX = 600;
            heraldY = 400;
            featherX = 150;
            featherY = 400;
            moonX = -50;
            moonY = 20;
            sunX = 400;
            sunY = 0;
            sky = new Color(r, g, b);
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            dogRect = new Rectangle((int)dogX, (int)dogY, 100, 100);
            ghostRect = new Rectangle((int)ghostX, (int)ghostY, 100, 100);
            heraldRect = new Rectangle((int)heraldX, (int)heraldY, 100, 100);
            knightRect = new Rectangle((int)knightX, (int)knightY, 100, 100);
            featherRect = new Rectangle((int)featherX, (int)featherY, 100, 100);
            sunRect = new Rectangle((int)sunX, (int)sunY, 200, 200);
            moonRect = new Rectangle((int)moonX, (int)moonY, 150, 150);
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
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
            backgroundText = this.Content.Load<Texture2D>("Castle");
            dogText = this.Content.Load<Texture2D>("dog");
            ghostText = this.Content.Load<Texture2D>("GhostKnight");
            heraldText = this.Content.Load<Texture2D>("Herald");
            knightText = this.Content.Load<Texture2D>("Knight");
            featherText = this.Content.Load<Texture2D>("KnightFeather");
            moonText = this.Content.Load<Texture2D>("moon");
            sunText = this.Content.Load<Texture2D>("sun");
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
            cycle++;
            if(cycle<520)
            {
                if (seconds < 4)
                {
                    r = r - 1;
                    g = g - 1;
                    b = b - 1;
                    sky = new Color(r, g, b);
                }
                else if (seconds < 5)
                {
                    deezText = "deez nuts";
                    screenText = "";
                    ghostX = 300;
                    heraldX=heraldX+2;
                    r = r + 3;
                    g = g + 3;
                    b = b + 3;
                    sky = new Color(r, g, b);
                }
                else if (seconds < 6)
                {
                    ghostX = -200;
                }
            }
            if(cycle>500)
            {
                ghostX = ghostX + 4;
            }
            if(cycle>660)
            {
                heraldX = heraldX + 3;
            }
            timer = (timer + 1) % 540;
            moonRect.X = (int)moonX;
            moonX = moonX + 2;
            knightRect.X = (int)knightX;
            knightX = knightX - .5;
            featherRect.X = (int)featherX;
            featherX = featherX + .5;
            dogRect.X = (int)dogX;
            dogX--;
            ghostRect.X = (int)ghostX;
            heraldRect.X = (int)heraldX;
            if(featherRect.X>=160)
            {
                featherX = 150;
            }
            if(knightRect.X<=240)
            {
                knightX = 250;
            }    
            seconds = timer / 60;
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
            spriteBatch.Draw(backgroundText, backgroundRect, sky);
            spriteBatch.Draw(dogText, dogRect, sky);
            spriteBatch.Draw(ghostText, ghostRect, sky);
            spriteBatch.Draw(heraldText, heraldRect, sky);
            spriteBatch.Draw(knightText, knightRect, sky);
            spriteBatch.Draw(featherText, featherRect, sky);
            spriteBatch.Draw(sunText, sunRect, sky);
            spriteBatch.Draw(moonText, moonRect, sky);
            spriteBatch.DrawString(Font1, screenText, new Vector2(490, 400), Color.White);
            spriteBatch.DrawString(Font1, deezText, new Vector2((int)ghostX, 300), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
