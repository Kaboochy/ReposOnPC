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

namespace KungFuMaster
{
    class enemy
    {
        Rectangle pos, iLoc;
        public static Texture2D sprite1, sprite2;
        public static Rectangle pPos;
        Texture2D sprite;
        static Random rng = new Random();
        int timer, ptimer, state;
        bool let;
        public enemy()
        {
            if (0 == 0)
            {
                pos = new Rectangle(-60, 420, 90, 165);
                iLoc = new Rectangle(5, 0, 60, 110);
                sprite = sprite1;
                let = true;
            }
            else
            {
                pos = new Rectangle(1126, 444, 90, 165);
                iLoc = new Rectangle(0, 0, 60, 110);
                sprite = sprite2;
                let = false;
            }
            timer = 0;
            ptimer = 0;
            state = 1;
        }

        public void Update(int dic)
        {
            if (let)
            {
                timer++;
                if (dic == 0)
                {
                    if (pos.X + pos.Width < pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X += 1;
                    }
                    else if (pos.X + pos.Width > pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X -= 1;
                    }
                    else if (pos.X + pos.Width == pPos.X - 20)
                    {
                        if (timer % 10 == 0)
                            state--;
                    }
                }
                else if (dic == 1)
                {
                    if (pos.X + pos.Width < pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X += 1;
                    }
                    else if (pos.X + pos.Width > pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X += 1;
                    }
                    else if (pos.X + pos.Width == pPos.X - 20)
                    {
                        if (timer % 10 == 0)
                            state = 0;
                    }
                }
                else if (dic == 2)
                {
                    if (pos.X + pos.Width < pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X += 1;
                    }
                    else if (pos.X + pos.Width > pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X -= 1;
                    }
                    else if (pos.X + pos.Width == pPos.X - 20)
                    {
                        if (timer % 10 == 0)
                            state++;
                    }
                }
                if (state == 0)
                {
                    iLoc = new Rectangle(375, 0, 55, 110);
                }
                if (state == 1)
                {
                    iLoc = new Rectangle(315, 0, 55, 110);
                }
                if (state == 2)
                {
                    iLoc = new Rectangle(255, 0, 55, 110);
                }
                if (state == 3)
                {
                    iLoc = new Rectangle(195, 0, 55, 110);
                }
                if (state == 4)
                {
                    iLoc = new Rectangle(135, 0, 55, 110);
                }
                if (state == 5)
                {
                    iLoc = new Rectangle(75, 0, 55, 110);
                }
                if (state == 7)
                {
                    iLoc = new Rectangle(15, 0, 55, 110);
                }
            }
            else
            {
                timer++;
                if (dic == 0)
                {
                    if (pos.X > pPos.X + pPos.Width + 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X -= 2;
                    }
                    else if (pos.X < pPos.X + pPos.Width + 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X += 1;
                    }
                    else if (pos.X == pPos.X + pPos.Width + 20)
                    {
                        if (timer % 10 == 0)
                            state--;
                    }
                }
                else if (dic == 1)
                {
                    if (pos.X > pPos.X + pPos.Width + 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X += 1;
                    }
                    else if (pos.X < pPos.X + pPos.Width + 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X -= 2;
                    }
                    else if (pos.X == pPos.X + pPos.Width + 20)
                    {
                        if (timer % 10 == 0)
                            state--;
                    }
                }
                else if (dic == 2)
                {
                    if (pos.X + pos.Width < pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state++;
                        if (state == 7)
                            state = 0;
                        pos.X -= 2;
                    }
                    else if (pos.X + pos.Width > pPos.X - 20)
                    {
                        if (timer % 5 == 0)
                            state--;
                        if (state == 0)
                            state = 6;
                        pos.X += 1;
                    }
                    else if (pos.X + pos.Width == pPos.X - 20)
                    {
                        if (timer % 10 == 0)
                            state++;
                    }
                }
                if (state == 0)
                {
                    iLoc = new Rectangle(0, 0, 60, 110);
                }
                if (state == 1)
                {
                    iLoc = new Rectangle(60, 0, 60, 110);
                }
                if (state == 2)
                {
                    iLoc = new Rectangle(120, 0, 60, 110);
                }
                if (state == 3)
                {
                    iLoc = new Rectangle(180, 0, 60, 110);
                }
                if (state == 4)
                {
                    iLoc = new Rectangle(240, 0, 60, 110);
                }
                if (state == 5)
                {
                    iLoc = new Rectangle(300, 0, 60, 110);
                }
                if (state == 7)
                {
                    iLoc = new Rectangle(360, 0, 60, 110);
                }
            }
        }
        public bool Contact(Rectangle rect)
        {
            return pos.Intersects(rect);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(sprite1, pos, iLoc, Color.White);
            sb.End();
        }
    }
}
