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
    public class Plant : Microsoft.Xna.Framework.GameComponent
    {
        public Plant(Game game)
            : base(game)
        {
           // _snd = snd.CreateInstance();
        }

        public Texture2D texOpen, texClosed;
        public SoundEffect snd; SoundEffectInstance _snd;
        public bool close = false;
        public Vector2 pos = new Vector2();
        Vector2 posDraw = new Vector2();
        Vector2 sizeCol = new Vector2(100, 50);
        Vector2 sizeDraw = new Vector2(100, 100);
        int timer;
        int closeTime;
        public Rectangle colRec, drawRec;
        public int delay = 0;//used to delay the time it takes to snap shut
        public bool alive = false;

        public Rectangle sndDist;//used to only play sound when intersected
        public bool playSnd = false;

        public override void Initialize()
        {
            _snd = snd.CreateInstance();
        }

        public void Update()
        {
            sndDist = new Rectangle(drawRec.X - 250, drawRec.Y - 250, 500, 500);//sound barrier

            posDraw = pos;
            posDraw.Y = pos.Y - 40;


            if (!close)
            {
                timer++;
                if (timer >= (200 + delay) - delay / 2)
                {
                    close = true;
                    timer = 0;
                }
            }

            if (close)
            {
                closeTime++;
                if (playSnd)
                {
                    if (playSnd)
                    {
                        if (closeTime < 6)
                        { _snd.Play(); }
                    }

                }
                else { _snd.Stop(); }

                if (closeTime >= 20)
                {
                    close = false;
                    closeTime = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            colRec = new Rectangle((int)pos.X, (int)pos.Y, (int)sizeCol.X, (int)sizeCol.Y);
            drawRec = new Rectangle((int)posDraw.X, (int)posDraw.Y, (int)sizeDraw.X, (int)sizeDraw.Y);

            if (!close)
            {
                spriteBatch.Draw(texOpen, drawRec, Color.AntiqueWhite);
            }
            else
            {
                spriteBatch.Draw(texClosed, drawRec, Color.AntiqueWhite);
            }
        }
    }
}
