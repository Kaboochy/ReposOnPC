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

namespace SchoolZenog
{
    class EnemyList
    {
        static Random rng = new Random();
        public List<Ranger> rangers;
        int timer;
        public int speed;

        public EnemyList()
        {
            rangers = new List<Ranger>();
            timer = 0;
            speed = 36;
        }
        public void load()
        {
            rangers.Add(new Ranger(rng.Next(0, 2)));
            timer = 0;
            if (speed > 12)
            {
                speed -= 4;
            }
        }
        public void Update(Rectangle zDest, int move)
        {
            for (int i = 0; i < rangers.Count; i++)
            {
                if (rangers[i].health >= 0)
                {
                    rangers.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < rangers.Count; i++)
            {
                rangers[i].Update(zDest, move);
            }
            if (timer <= speed)
            {
                load();
            }
        }
        public int attack(List<Rectangle> hits)
        {
            int a = 0;
            for (int i = 0; i < rangers.Count; i++)
            {
                if (rangers[i].mis.Hit(hits))
                {
                    if (rangers[i].mis.right)
                    {
                        a++;
                    }
                    else
                    {
                        a--;
                    }
                }
            }
            return a;
        }
        public void attacked(List<Rectangle> hits, int damage)
        {
            for (int i = 0; i < rangers.Count; i++)
            {
                if (rangers[i].Hit(hits))
                {
                    rangers[i].health -= damage;
                }
            }
        }
    }
}