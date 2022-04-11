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

namespace Memory
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D startText, crossText, endT;
        Rectangle startRect, crossRect, endR;
        State gameState;
        MouseState oldmouse;
        SpriteFont font;
        List<Card> deck = new List<Card>(24);
        Rectangle[] rects;
        int counter, seconds, a, b;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Card.textures = new Texture2D[13];
            deck = new List<Card>(24);
            crossRect = new Rectangle(0, 0, 35, 35);
            startRect = new Rectangle(0, 0, 1200, 800);
            endR = new Rectangle(0, -800, 1200, 800);
            rects = new Rectangle[24];
            counter = -1;
            a = -1;
            b = -1;
            for (int i = 0; i < 6; i++)
            {
                for (int q = 0; q < 4; q++)
                {
                    rects[(i * 4) + q] = new Rectangle(20 + (130 * i), 20 + (130 * q), 130, 130);
                }
            }
            Shuffle(rects);
            for (int i = 1; i < 13; i++)
            {
                for (int q = 0; q < 2; q++)
                {
                    deck.Add(new Card(i, rects[((i - 1) * 2) + q]));
                }
            }
            oldmouse = Mouse.GetState();
            base.Initialize();
        }
        //SHUFFLE
        public static Random rng = new Random();
        public void Shuffle(Rectangle[] rects)
        {
            int n = rects.Length;
            while (n > 1)
            {
                n--;
                int p = rng.Next(n + 1);
                Rectangle value = rects[p];
                rects[p] = rects[n];
                rects[n] = value;
            }
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startText = this.Content.Load<Texture2D>("Memory_Card_Game_Start_Screen");
            crossText = this.Content.Load<Texture2D>("crossh");
            endT = this.Content.Load<Texture2D>("R");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            for (int i = 0; i < Card.textures.Length; i++)
            {
                Card.textures[i] = Content.Load<Texture2D>(i.ToString());
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //MOUSE
            MouseState mouse = Mouse.GetState();
            crossRect.X = mouse.X - (crossRect.Width / 2);
            crossRect.Y = mouse.Y - (crossRect.Height / 2);
            //START GAME
            if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released
                && gameState == State.start)
            {
                gameState = State.play;
            }
            //ACTUAL UPDATE STUFF MY G WITH THE CLICKY
            if (deck.Count == 0)
                gameState = State.end;
            if (gameState == State.play)
            {
                startRect.Y = -800;
                seconds++;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    counter++;
                    for (int i = 0; i < deck.Count; i++)
                    {
                        if (deck[i].Contains(crossRect.X, crossRect.Y) && (counter < 3) && deck[i].isFaceUp == false)
                        {
                            deck[i].FlipCard(true);
                            if (counter == 1)
                            {
                                a = i;
                            }
                            else if (counter == 2)
                            {
                                b = i;
                            }
                        }
                    }
                    if (counter == 3)
                    {
                        if (/*deck[a].textIndex==deck[b].textIndex*/deck[a].Equals(deck[b]))
                        {
                            if (a > b)
                            {
                                deck.RemoveAt(a);
                                deck.RemoveAt(b);
                                counter = 0;
                            }
                            if (b > a)
                            {
                                deck.RemoveAt(b);
                                deck.RemoveAt(a);
                                counter = 0;
                            }
                        }
                        else if (/*deck[a].textIndex != deck[b].textIndex*/!deck[a].Equals(deck[b]))
                        {
                            deck[b].FlipCard(false);
                            deck[a].FlipCard(false);
                            counter = 0;
                        }
                        a = -1;
                        b = -1;
                    }
                }
            }
            if (gameState == State.end)
            {
                endR.Y = 0;
            }
            oldmouse = mouse;
            if(seconds%60==0)
            {
                Console.WriteLine("counter is "+counter);
                Console.WriteLine("a is " + a);
                Console.WriteLine("b is " + b);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Card c in deck)
            {
                c.Draw(spriteBatch);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(startText, startRect, Color.White);
            spriteBatch.Draw(crossText, crossRect, Color.White);
            spriteBatch.Draw(endT, endR, Color.White);
            if (gameState == State.end)
            {
                spriteBatch.DrawString(font, "Completed in " + seconds/60+" seconds", new Vector2(50, 50), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}