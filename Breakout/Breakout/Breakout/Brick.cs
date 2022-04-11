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
    public class Brick
    {
        public Color Color { get; }
        public int X { get;  }
        public int Y { get; }
        public bool Destroyed { get; set; }
        public Rectangle Rect { get; }
        public Brick(char color, int x, int y, GraphicsDeviceManager graphics, int totalX, int totalY)
        {
            switch (color)
            {
                case 'r': Color = Color.Red; break;
                case 'g': Color = Color.Green; break;
                case 'b': Color = Color.Blue; break;
                case 'w': Color = Color.White; break;
                default: Color = Color.Black; break;
            }
            X = x;
            Y = y;
            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;
            Rect = new Rectangle(Y * width / totalX, X * height / totalY, width / totalX, height / totalY);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D text)
        {
            if(Destroyed||Color==Color.Black)
            {
                return;
            }
            spriteBatch.Begin();
            spriteBatch.Draw(text, Rect, Color);
            spriteBatch.End();
        }
    }
}
