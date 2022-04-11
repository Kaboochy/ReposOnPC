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

namespace StarField
{
    class Star
    {
        public static Texture2D text;
        private Color myColor;
        public static Random rng = new Random();
        double x, y;
        int vX, vY;

        public Star()
        {
            this.x = rng.Next(800);
            this.y = rng.Next(480);
            this.vX = rng.Next(-5, 6);
            this.vY = rng.Next(-5, 6);
            myColor = generateRandomColor();
        }
        private Color generateRandomColor()
        {
            return new Color(rng.Next(256), rng.Next(256), rng.Next(256));
        }
        public void Update()
        {
            x += vX;
            y += vY;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(text, new Vector2((float)x, (float)y), myColor);
            spriteBatch.End();
        }
    }
}