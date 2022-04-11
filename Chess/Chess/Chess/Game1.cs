using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Chess
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D board;
        Rectangle screen;
        Texture2D[] pieceImages;
        string[] pieceNames = { "Black Castle", "Black Knight", "Black Bishop", "Black Queen", "Black King", "Black Bishop", "Black Knight", "Black Castle", 
            "Black Pawn", "Black Pawn", "Black Pawn", "Black Pawn", "Black Pawn", "Black Pawn", "Black Pawn", "Black Pawn",
            "White Castle", "White Knight", "White Bishop", "White Queen", "White King", "White Bishop", "White Knight", "White Castle",
            "White Pawn", "White Pawn", "White Pawn", "White Pawn", "White Pawn", "White Pawn", "White Pawn", "White Pawn", };
        //RECTANGLE STUFF
        Rectangle[] rects;
        Texture2D[] pieces;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            screen = new Rectangle(0, 0, 1000, 1000);
            pieceImages = new Texture2D[32];
            //RECTANGLE STUFF
            rects = new Rectangle[32];
            pieces = new Texture2D[32];
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board = Content.Load<Texture2D>("Chess Board");
            for (int i = 0; i < pieceNames.Length; i++)
            {
                pieceImages[i] = Content.Load<Texture2D>(pieceNames[i]);
            }
            //RECTANGLE STUFF
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = j * 8 + i;
                    //Console.WriteLine(index);
                    //Console.WriteLine(pieceImages.Length);
                    Texture2D tex = pieceImages[index];
                    rects[index] = new Rectangle(screen.Width/8*i,
                        (screen.Height / 8 * (j < 2 ? j : 9 - j)),
                        tex.Width,tex.Height);
                }
            }
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(board, screen, Color.White);
            for(int i = 0; i < 32; i++)
            {
                Rectangle rect = rects[i];
                Vector2 destRect = new Vector2(rect.X, rect.Y);
                spriteBatch.Draw(pieceImages[i], destRect, Color.White);
                //Console.WriteLine(rect);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
