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
    public class Player : Microsoft.Xna.Framework.GameComponent
    {
        public Player(Game game)
            : base(game)
        {
            animation = new Animation(game);
            
            //set buttons
            recLeft.Width = btnSize; recLeft.Height = btnSize;
            recRight.Width = btnSize; recRight.Height = btnSize;
            recJump.Width = btnSize; recJump.Height = btnSize;
            recSlide.Width = btnSize; recSlide.Height = btnSize;
            recAction.Width = btnSize; recAction.Height = btnSize;
            direction = left;
        }

        
        public bool alive = true;
        public Animation animation;
        public Texture2D runL, runR;
        public SoundEffect snd; SoundEffectInstance _snd;
        public SoundEffect sndExhale; SoundEffectInstance _sndExhale;
        public Rectangle rec, feetRec;
        public Vector2 position = new Vector2(-1000, -1000);
        int width = 40, height = 60;
        int action = 0;
        int actionRunL = 1, actionRunR = 2, actionJump = 3, actionAir = 4;
        public bool jumpBool = false;
        public bool inAir = false;
        int timer;

        //static images
        public Texture2D staticTex, airR, airL,standR,standL,jump,slideL,slideR;

        public Vector2 levelStart;//used to keep the start of each level for respawn
        bool colL = false, colR = false;
        public bool actionButton;
        TouchLocation oldTouch = new TouchLocation();
        TouchLocation crntTouch = new TouchLocation();
        int x2;//when slide is pressed this is finish position
        public bool slide = false;
        int slideTimer;

        public Texture2D btnLeft, btnRight, btnJump, btnSlide, btnAction, btnLeftP, btnRightP, btnJumpP, btnSlideP, btnActionP;
        public Texture2D drawL, drawR, drawJ, drawS, drawA;
        public Rectangle recLeft, recRight, recJump, recSlide, recAction;
        int btnSize = 70;

        public int direction, left = 2, right = -2;
        public bool sceneStop = false;
        bool touch = false;
        bool jumpTouch = false;
        bool jumpSound = false;
        int soundTimer;//used to stop exhale sound


        public override void Initialize()
        {
            _snd = snd.CreateInstance();
            _sndExhale = sndExhale.CreateInstance();
            _snd.Volume = 0.4f;

           drawL = btnLeft; drawR = btnRight; drawJ = btnJump; drawS = btnSlide; drawA = btnAction;
        }

        public void Update(Viewport vp)
        {
            
            ButtonPos(vp);
            if (!alive)
            {
                Death();
            }
            timer++;
            animation.SetAnimation(80, 9, 40);
            touch = false;//set touch to false so new latest touch can be recorded
            if (!sceneStop)
            {
                drawJ = btnJump; 
                 drawL = btnLeft; 
                   drawR = btnRight; 
                foreach (TouchLocation tl in TouchPanel.GetState())
                {
                    jumpTouch = false;
                    if (recJump.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        crntTouch = tl;
                        jumpTouch = true;
                        action = actionJump;
                        jumpSound = true;
                        drawJ = btnJumpP;
                    }
                    
                  

                    touch = true;
                    if (recLeft.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        crntTouch = tl;
                        direction = left;
                        drawL = btnLeftP;
                        if (!colL)
                        {
                            action = actionRunL;
                            position.X -= 3;
                        }
                    }
                   

                    if (recRight.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        crntTouch = tl;
                        direction = right;
                        drawR = btnRightP;
                        if (!colR)
                        {
                            action = actionRunR;

                            position.X += 3;
                        }
                    }
                 
                }


                foreach (TouchLocation tl in TouchPanel.GetState())
                {
                    if (jumpTouch == true && recJump.Contains((int)oldTouch.Position.X, (int)oldTouch.Position.Y))
                    {
                        jumpBool = true;
                        action = actionAir;
                    }
                }
            }

            
            if (touch == false)//input is null
            { animation.currentFrame = 0; action = 0; }

            animation.Place(position);
            Action();

            if (action == actionRunR)
            {
                animation.spriteSheet = runR;
            }
            else if (action == actionRunL)
            {
                animation.spriteSheet = runL;
            }

            else
            {
                if (!slide)
                {
                    if (direction == right)
                    {
                        animation.spriteSheet = standR;
                    }
                    if (direction == left)
                    {
                        animation.spriteSheet = standL;
                    }
                }
            }

            if (inAir)
            {//sound
                soundTimer++;
                if (soundTimer < 13 && jumpSound)
                { _sndExhale.Play(); }
                else { _sndExhale.Stop(); }
                //draw
                if (direction == right)
                {
                    staticTex = airR;
                }
                else { staticTex = airL; }
            }
            else if (action == actionJump)
            {
                jumpSound = true;
                staticTex = jump;
            }
            else
            {
                jumpSound = false;
                soundTimer = 0;
                if (!slide)
                {
                    if (direction == right)
                    {
                        staticTex = standR;
                    }
                    else if (direction == left)
                    {
                        staticTex = standL;
                    }
                }
            }
            Slide();
            oldTouch = crntTouch;
        }

        int deathTime;
        public void Death()
        {
            deathTime++;
            position = levelStart;
              slide = false;//fixes slide glitch
            if (deathTime > 15)
            {
                alive = true;
                deathTime = 0;
            }
        }

        private void ButtonPos(Viewport vp)
        {
            recLeft.X = vp.Width - 630; recLeft.Y = vp.Height - 70;
            recRight.X = vp.Width - 550; recRight.Y = vp.Height - 70;
            recAction.X = vp.Width - 210; recAction.Y = vp.Height - 70;
            recJump.X = vp.Width - 140; recJump.Y = vp.Height - 110;
            recSlide.X = vp.Width - 70; recSlide.Y = vp.Height - 70;
        }

        public void Slide()
        {
            slideTimer++;
               drawS = btnSlide; 
            foreach (TouchLocation tl in TouchPanel.GetState())
            {
                if (recSlide.Contains((int)tl.Position.X, (int)tl.Position.Y))
                {
                    drawS = btnSlideP;
                    if (!slide)
                    {
                        if (slideTimer > 20)
                        {
                            if (direction == right)
                            {
                                staticTex = slideR;
                                x2 = (int)position.X + 120;
                            }
                            else if (direction == left)
                            {
                                staticTex = slideL;
                                x2 = (int)position.X - 120;
                            }
                            slide = true;

                        }
                    }

                    slideTimer = 0;

                }
             
            }
            if (slide)
            {
               // if (!inAir)
               // {
                    height = 30;
                    width = 65;
                    if (direction == right)
                    {
                        staticTex = slideR;
                    }
                    else if (direction == left)
                    {
                        staticTex = slideL;
                    }
               // }
                if (position.X < x2)
                {
                    position.X += 4;
                }
                if (position.X > x2)
                {
                    position.X -= 4;
                }
            }
            else
            {
                height = 60;
                width = 40;
            }

            if (position.X == x2)
            {
                slide = false;
            }

        }

        public void Action()
        {
            drawA = btnAction;
            foreach (TouchLocation tl in TouchPanel.GetState())
            {
                if (recAction.Contains((int)tl.Position.X, (int)tl.Position.Y))
                {
                    drawA = btnActionP;
                    actionButton = true;
                }
                else { actionButton = false;  }
            }
        }

        public void Collision()
        {
            if (action == actionRunL)
            {
                colL = true;
                // position.X++;
                //  EndCollision();
            }
            else if (action == actionRunR)
            {
                colR = true;
                // position.X--;
                // EndCollision();
            }
        }

        public void EndCollision()
        {
            colR = false;
            colL = false;
        }

        public void DrawButtons(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(drawL, recLeft, Color.AntiqueWhite);
            spriteBatch.Draw(drawR, recRight, Color.AntiqueWhite);
            spriteBatch.Draw(drawA, recAction, Color.AntiqueWhite);
            spriteBatch.Draw(drawJ, recJump, Color.AntiqueWhite);
            spriteBatch.Draw(drawS, recSlide, Color.AntiqueWhite);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool godMode)
        {

            rec = new Rectangle((int)position.X, (int)position.Y, width, height);
           // if (!slide)
           // {
                feetRec = new Rectangle((int)position.X + 15,rec.Y+(rec.Height-5), 15, 10);// (int)position.Y + 55
           // }
           // else { feetRec = new Rectangle((int)position.X + 15, (int)position.Y + 15, 15, 10); }
            if (alive)
            {
                if (action != actionRunL && action != actionRunR)
                {
                    spriteBatch.Draw(staticTex, rec, Color.AntiqueWhite);
                    _snd.Stop();
                }

                else
                {
                    if (!inAir&&!slide)
                    {
                        animation.Draw(gameTime, spriteBatch, Color.AntiqueWhite);
                        _snd.Play();
                    }
                    else 
                        spriteBatch.Draw(staticTex, rec, Color.AntiqueWhite);
                }
            }
        }
    }
}
