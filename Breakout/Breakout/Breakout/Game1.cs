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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D squareText, startScreen, endScreen;
        Rectangle top, bot, left, right;
        //GameState ENUM
        public State GameState { get; set; }
        Brick[,] bricks;
        Pong pog;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            //SCREEN SIZE
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            top = new Rectangle(0, 0, screenWidth, 5);
            bot = new Rectangle(0, screenHeight, screenWidth, 5);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 0, screenHeight);
            //2D ARRAY
            bricks = LoadLevel(@"Content/google.txt");
            pog = new Pong(top, bot, left, right, bricks);
            //GAME
            GameState = State.start;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            squareText = Content.Load<Texture2D>("Square");
            startScreen = Content.Load<Texture2D>("Start");
            endScreen = Content.Load<Texture2D>("R");
        }
        public int CountLinesReader(string filename)
        {
            var lineCounter = 0;

            using (var reader = new StreamReader(filename))
            {
                while (reader.ReadLine() != null)
                {
                    lineCounter++;
                }
                return lineCounter;
            }
        }
        private Brick[,] LoadLevel(string filename)
        {
            var temp = new List<Brick[]>();
            var reader = new StreamReader(filename);
            int x = 0;
            int totalY = CountLinesReader(filename);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var bricks = new Brick[line.Length];

                for (var i = 0; i < line.Length; i++)
                {
                    bricks[i] = new Brick(line[i], x, i, graphics, line.Length, totalY);
                }

                temp.Add(bricks);
                x++;
            }
            int minorLength = temp[0].Length;
            Brick[,] res = new Brick[temp.Count, minorLength];

            for (int i = 0; i < temp.Count; i++)
            {
                var array = temp[i];
                for (int j = 0; j < minorLength; j++)
                {
                    res[i, j] = array[j];
                }
            }

            return res;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(GameState == State.start)
            {
                MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    GameState = State.level1;
                }
            }
            else
                pog.Update(this);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //START GAME
            if (GameState == State.start)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(startScreen, new Rectangle(0,0,1200,800), Color.White);
                spriteBatch.End();

                return;
            }
            //LEVELS
            for (int r = 0; r < bricks.GetLength(0); r++)
            {
                for (int c = 0; c < bricks.GetLength(1); c++)
                {
                    bricks[r,c].Draw(spriteBatch, squareText);
                }
            }
            pog.Draw(spriteBatch, squareText);
            //GAME END
            if (GameState == State.end)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(endScreen, new Rectangle(0, 0, 1200, 800), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}