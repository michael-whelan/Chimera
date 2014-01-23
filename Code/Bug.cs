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

    public class Bug : Microsoft.Xna.Framework.GameComponent
    {
        public Bug(Game game)
            : base(game)
        {
            animation = new Animation(Game);
        }

        public Texture2D texBugL, texBugR, attackL, attackR;
        public SoundEffect snd; SoundEffectInstance _snd;
        public Rectangle sndDist;//used to only play sound when intersected
        public bool playSnd = false;
        public Vector2 position = new Vector2();
        Vector2 pos1 = new Vector2();
        Vector2 pos2 = new Vector2();
        int timer;
        int direction = 0;
        int left = 1, right = -1;
        Animation animation;
        public Rectangle rec;
        public bool col = false;
        public bool alive = false;//determins if they are silent

        public void Initialize(int d)
        {
            animation.SetAnimation(80, 2, 50);
            direction = right;
            pos1 = position;
            pos2.X = pos1.X + d;
            pos2.Y = pos1.Y;
            _snd = snd.CreateInstance();
            _snd.Volume = 0.2f;
        }

        public void Update()
        {
            sndDist = new Rectangle(rec.X -250,rec.Y-250,500,500);
            if (playSnd)
            {
                _snd.Play();
            }
            else
            {
                _snd.Stop();
            }
            animation.Place(position);
            timer++;
            if (!col)
            {
                if (timer > 2)
                {
                    if (direction == right)
                    {
                        position.X += 2;
                    }
                    else if (direction == left)
                    {
                        position.X -= 2;
                    }
                    timer = 0;
                }
            }
            if (position.X < pos1.X || position.X > pos2.X)
            {
                direction *= -1;
            }
            //************
            if (direction == left)
            {
                if (col)
                {
                    animation.spriteSheet = attackL;
                }
                else
                {
                    animation.spriteSheet = texBugL;
                }
            }
            if (direction == right)
            {
                if (col)
                {
                    animation.spriteSheet = attackR;
                }
                else
                {
                    animation.spriteSheet = texBugR;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rec = new Rectangle((int)position.X, (int)position.Y, 20, 20);

            animation.Draw(gameTime, spriteBatch, Color.AntiqueWhite);

        }
    }
}
