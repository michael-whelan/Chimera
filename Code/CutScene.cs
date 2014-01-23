using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace Chimera
{

    public class CutScene : Microsoft.Xna.Framework.GameComponent
    {
        public CutScene(Game game)
            : base(game)
        {
           
        }

        public Texture2D riftPresents, name, blackSound, upcoming1, upcoming2, comingSoon;
        public Texture2D draw,tapScreen;
        Rectangle drawRec,tapRec;
        public int frameNum = 1;
        int timer = 0;
        public bool endScene = false;
        public SoundEffect openingSound; public SoundEffectInstance openingSound_;
        public SoundEffect safe; public SoundEffectInstance safe_;

        public SoundEffect closingSound; SoundEffectInstance closingSound_;

        public override void Initialize()
        {
            openingSound_ = openingSound.CreateInstance();
            safe_ = safe.CreateInstance();
            closingSound_ = closingSound.CreateInstance();
            safe_.IsLooped = false;

        }

        private void DrawScene1(int room, KeyboardState ks)
        {
            if (timer > 50)
            {
                tapRec.X = 260;
                tapRec.Y = 300;
                foreach (TouchLocation tl in TouchPanel.GetState())
                {
                    if (tl.State == TouchLocationState.Pressed)
                    {
                        if (frameNum < 3)
                        {
                            frameNum++;
                        }
                        else
                        {
                            endScene = true;
                            frameNum = 1;
                        }
                        timer = 0;
                    }
                }
            }
            else
            {
                tapRec.X = -1000;
                tapRec.Y = -1000;
            }

            if (frameNum == 1)
            {
                if (timer > 80)
                { frameNum++; timer = 0; }
            }

            if (frameNum == 2)
            {
                openingSound_.Play();
                if (timer > 670)
                { frameNum++; timer = 0; }
            }
            else if (frameNum == 3)
            {
                openingSound_.Stop();

                safe_.Play();
                if (timer > 30)
                {
                    endScene = true;
                    frameNum = 1;
                }
            }

            if (endScene)
            {
                safe_.Stop();
            }
        }

        private void DrawScene2(int room, KeyboardState ks)
        {
            if (timer > 50)
            {
                tapRec.X = 260;
                tapRec.Y = 340;
                foreach (TouchLocation tl in TouchPanel.GetState())
                {
                    if (tl.State == TouchLocationState.Pressed)
                    {
                        if (frameNum < 3)
                        {
                            frameNum++;
                        }
                        else
                        {
                            endScene = true;
                        }
                        timer = 0;
                    }
                }
                if (frameNum == 2)
                {
                    closingSound_.Play();
                }
                else { closingSound_.Stop(); }
            }
            else
            {
                tapRec.X = -1000;
                tapRec.Y = -1000;
            }

            if (frameNum == 1)
            {
                if (timer > 80)
                {
                    frameNum++;
                    timer = 0;
                }
            }
            if (frameNum == 2)
            {
                if (timer > 250)
                {
                    frameNum++;
                    timer = 0;
                }
            }
            if (frameNum == 3)
            {
                if (timer > 70)
                {
                    endScene = true;
                    frameNum = 1;
                    timer = 0;
                }
            }
        }

        public void Update(int room, KeyboardState ks)
        {
            timer++;
            ControlDrawImage(room);
            if (room == 1)
            {
                DrawScene1(room, ks);
            }
            else if (room == 10)
            {
                DrawScene2(room, ks);
            }
        }

        public void ControlDrawImage(int room)
        {
            if (room == 1)
            {
                if (frameNum == 1)
                {
                    draw = riftPresents;
                }
                else if (frameNum == 2)
                {
                    draw = name;
                }
                else if (frameNum == 3)
                {
                    draw = blackSound;
                }
            }
            else if (room == 10)
            {
                if (frameNum == 1)
                {
                    draw = upcoming1;
                }
                else if (frameNum == 2)
                {
                    draw = upcoming2;
                }
                else if (frameNum == 3)
                {
                    draw = comingSoon;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, float viewWidth, float viewHeight)
        {
            drawRec = new Rectangle(0, 0, (int)viewWidth, (int)viewHeight);
            tapRec.Width = 200;
            tapRec.Height = 40;
           
            spriteBatch.Draw(draw, drawRec, Color.AntiqueWhite);
            spriteBatch.Draw(tapScreen, tapRec, Color.AntiqueWhite);
        }
    }
}
