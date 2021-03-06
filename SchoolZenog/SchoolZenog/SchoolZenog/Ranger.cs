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
/*
namespace SchoolZenog
{
    class Ranger : AnimatedSprite
    {
        Rectangle dest, green;
        static Texture2D tex;
        Color col = Color.White;
        public int health = 100;
        int timer = 0;
        public Projectile mis;
        public static new Animations anime = new Animations();

        public Ranger(int i)
        {
            if (i == 0)
            {
                dest = new Rectangle(2880, 820, 200, 200);
            }
            else if (i == 1)
            {
                dest = new Rectangle(-960, 820, 200, 200);
            }
            else
            {
                dest = new Rectangle(2880, 820, 200, 200);
            }
            green = new Rectangle(dest.X, dest.Y + 15, ((dest.Width * health) / 100), 15);
            mis = new Projectile();
        }
        public static void SetRTex(Texture2D t)
        {
            tex = t;
            Projectile.tex = t;
        }
        public static void prepAnime(Texture2D t)
        {
            for (int i = 0; i < 2; i++)
            {
                anime.attack11.Add(new Animation(t, i, 0, 50, @"Content/Ranger/Ranger_Attack11.txt"));
            }
            for (int i = 0; i < 4; i++)
            {
                anime.walk.Add(new Animation(t, i, 1, 50, @"Content/Ranger/Ranger_Walk.txt"));
            }
            for (int i = 0; i < 3; i++)
            {
                anime.idle.Add(new Animation(t, i, 2, 50, @"Content/Ranger/Ranger_Idle.txt"));
            }
            /*
            for (int i = 0; i < 3; i++)
            {
                anime.attack21.Add(new Animation(t, i, 3, 50, @"Content/Ranger/Ranger_Attack21.txt"));
            }
            */
            for (int i = 0; i < 1; i++)
            {
                Projectile.anime.attack11.Add(new Animation(t, i, 4, 50, @"Content/Ranger/Ranger_Projectile.txt"));
            }
        }
        public void Update(Rectangle zDest, int move)
        {
            timer++;
            //idle
            if (stop == 0)
            {
                currentAnime = Animated.idle;
            }
            //side
            if (zDest.X < dest.X)
            {
                right = false;
            }
            if (zDest.X > dest.X)
            {
                right = true;
            }
            //screen movement
            if (!(move == 0))
            {
                if (move == -2)
                {
                    dest.X += 5;
                }
                if (move == -1)
                {
                    dest.X += 2;
                }
                if (move == 1)
                {
                    dest.X -= 2;
                }
                if (move == 2)
                {
                    dest.X -= 5;
                }
            }
            //movement
            if (!right && stop == 0 && !(dest.X < 520 && dest.X > 240))
            {
                currentAnime = Animated.walk;
                dest.X += 3;
            }
            if (right && stop == 0 && !(dest.X < 1480 && dest.X > 1200))
            {
                currentAnime = Animated.walk;
                dest.X -= 3;
            }
            //attack
            if (stop == 0 && ((dest.X < 1480 && dest.X > 1200) || (dest.X < 520 && dest.X > 240)) && timer >= 55)
            {
                currentAnime = Animated.attack11;
                stop = 1;
                timer = 0;
                mis.fire(right);
            }
            //frame update
            up();
            if (stop == 1 && currentFrame + 1 == Number())
            {
                stop = 0;
            }
            if (timer >= 55)
            {
                mis.end();
            }
            green = new Rectangle(dest.X, dest.Y + 15, ((dest.Width * health) / 100), 15);
            mis.Update(move);
        }
    }

    class Projectile : AnimatedSprite
    {
        Rectangle dest;
        public static Texture2D tex;
        public Boolean isFired = false;
        public static new Animations anime = new Animations();
        public Projectile()
        {
            dest = new Rectangle(0, 0, 150, 150);
        }
        public void Update(int move)
        {
            if (isFired)
            {
                if (!(move == 0))
                {
                    if (move == -2)
                    {
                        dest.X += 5;
                    }
                    if (move == -1)
                    {
                        dest.X += 2;
                    }
                    if (move == 1)
                    {
                        dest.X -= 2;
                    }
                    if (move == 2)
                    {
                        dest.X -= 5;
                    }
                }
                if (right)
                {
                    dest.X -= 10;
                }
                else
                {
                    dest.X += 10;
                }
            }
            else
            {
                dest = new Rectangle(0, 0, 150, 150);
            }
        }
        public void fire(Boolean Right)
        {
            isFired = true;
            right = Right;
        }
        public void end()
        {
            isFired = false;
        }
    }
}
*/