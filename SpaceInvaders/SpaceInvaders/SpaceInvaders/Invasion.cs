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

namespace SpaceInvaders
{
    class Invasion
    {
        Texture2D text;
        Rectangle sourceRect;
        int timer, pos;
        public Invasion(Texture2D text)
        {
            this.text = text;
            this.pos = 0;
            this.sourceRect = new Rectangle(pos, 0, 50, 50);
        }
        public void Update()
        {
            sourceRect.X = pos;
            timer++;
            if (timer % 30 == 0)
            {
                pos = pos > 0 ? 0 : 50;
                sourceRect = new Rectangle(pos, 0, 50, 50);
            }
        }
        public void Draw(SpriteBatch spriteBatch, float x, float y)
        {
            spriteBatch.Draw(text, new Vector2(x, y), sourceRect, Color.White);
        }
    }
}
