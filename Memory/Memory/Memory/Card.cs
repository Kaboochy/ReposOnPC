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
    public class Card
    {
        public static Texture2D[] textures = new Texture2D[13];
        public int textIndex;
        private Rectangle posRect;
        public bool isFaceUp = false;
        public Card(int textIndex, Rectangle rect)
        {
            this.textIndex = textIndex;
            this.posRect = rect;

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (isFaceUp)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures[textIndex], posRect, Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures[0], posRect, Color.White);
                spriteBatch.End();
            }

        }
        public bool Contains(int x, int y)
        {
            if (posRect.Contains(x, y))
            {
                return true;
            }
            else
                return false;
        }
        public bool Equals(Card other)
        {
            if (textIndex == other.textIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FlipCard(bool ahh)
        {
            isFaceUp = ahh;
        }
    }
}
