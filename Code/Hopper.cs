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


namespace Chimera
{

    public class Hopper : Microsoft.Xna.Framework.GameComponent
    {
        public Hopper(Game game)
            : base(game)
        {
            animation = new Animation(Game);
        }

        public Texture2D texHopper1, texHopper2;
        public SoundEffect snd; SoundEffectInstance _snd;
        int sndTimer;
        public Rectangle rec;
        Vector2 pos = new Vector2();
        Vector2 pos1, pos2;//for left right
        int bottom, top;//for jump
        int horDirection = 0;
        int vertDirection = 0;
        int left = 1, right = -1, up = 2;
        public bool col = false;
        Animation animation;

        public Rectangle sndDist;//used to only play sound when intersected
        public bool playSnd = false;


        public void Initialize(Vector2 v, int d)
        {
            horDirection = right;
            vertDirection = up;
            pos = v;
            pos1 = v;
            pos2.X = v.X + d;
            pos2.Y = pos1.Y;
            bottom = (int)pos1.Y;
            top = bottom - 100;
            _snd = snd.CreateInstance();
            _snd.Volume = 0.7f;
        }

        public void Update()
        {
            sndTimer++;
            sndDist = new Rectangle(rec.X - 250, rec.Y - 250, 500, 500);
            animation.Place(pos);

            if (pos.Y < bottom - 5)//touching down
            {
                if (playSnd)
                {
                    if (sndTimer < 10)
                    { _snd.Play(); }
                    else
                    { _snd.Stop(); }
                }
            }
            else { sndTimer = 0; }


            if (!col)
            {
                if (horDirection == right)
                {
                    pos.X += 2;
                }
                else// (horDirection == left)
                {
                    pos.X -= 2;
                }

                if (vertDirection == up)
                {
                    pos.Y -= 2;
                }
                else //(vertDirection == down)
                {
                    pos.Y += 2;
                }
            }
            if (pos.X < pos1.X || pos.X > pos2.X)//flips horizontal direction
            {
                horDirection *= -1;
            }

            if (pos.Y > bottom || pos.Y < top)//flips verticle direction
            {
                vertDirection *= -1;
            }

            //************

            if (col)
            {
            }
            else
            {
                //animation.spriteSheet = texHopper;
            }


        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rec = new Rectangle((int)pos.X, (int)pos.Y, 40, 80);
            // animation.Draw(gameTime,spriteBatch,Color.AntiqueWhite);
            if (pos.Y >= bottom - 5)//touching down
            {
                spriteBatch.Draw(texHopper1, rec, Color.AntiqueWhite);
            }
            else //in air
            {
                spriteBatch.Draw(texHopper2, rec, Color.AntiqueWhite);
            }
        }
    }
}