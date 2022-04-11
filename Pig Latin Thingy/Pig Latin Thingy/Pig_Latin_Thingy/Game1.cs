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

namespace Pig_Latin_Thingy
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Pig1Text;
        Rectangle Pig1Rect;
        Texture2D Pig2Text;
        Rectangle Pig2Rect;
        String phrase1;
        String phrase2;
        String phrase3;
        SpriteFont Font1;
        String screenText;
        String piggyTexty;
        private int timer;
        private int seconds;
        public string ToPigLatin(string word)
        {
            string vowels = "AEIOUaeiou";
            var firstLetter = word[0];
            var restOfWord = word.Substring(1, word.Length - 1);
            int letter = vowels.IndexOf(firstLetter);
            if (letter == -1)
            {
                return restOfWord + firstLetter + "ay";
            }
            else
            {
                return word + "way";
            }
                

        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            timer = 0;
            seconds = 0;
            Pig1Rect = new Rectangle(0, 200, 200, 200);
            Pig2Rect = new Rectangle(500, 200, 200, 200);
            screenText = "Blank";
            piggyTexty = "Death";
            phrase1 = "cat";
            phrase2 = "stack";
            phrase3 = "ant";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            Pig1Text = this.Content.Load<Texture2D>("Pig 1");
            Pig2Text = this.Content.Load<Texture2D>("Pig 2");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //string pigLatin = ToPigLatin(string screenText);
            timer++;
            if (timer >= 360)
                timer = 0;
            seconds = timer / 60;
            if (seconds < 2)
            {
                screenText = phrase1;
                piggyTexty = ToPigLatin(phrase1);
            }
            else if (seconds < 4)
            {
                screenText = phrase2;
                piggyTexty = ToPigLatin(phrase2);
            }
            else
            {
                screenText = phrase3;
                piggyTexty = ToPigLatin(phrase3);
            } 
            base.Update(gameTime);
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
            spriteBatch.Draw(Pig1Text, Pig1Rect, Color.White);
            spriteBatch.Draw(Pig2Text, Pig2Rect, Color.White);
            spriteBatch.DrawString(Font1, screenText, new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(Font1, piggyTexty, new Vector2(400, 100), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
