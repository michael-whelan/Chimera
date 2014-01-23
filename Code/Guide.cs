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

    public class Guide : Microsoft.Xna.Framework.GameComponent
    {
        public Guide(Game game)
            : base(game)
        {
            animation = new Animation(game); 
        }

        public Texture2D motherTex,motherAnimation;
        public Rectangle rec,colRec;//only col is public 
        public Animation animation;

        public int timer = 0;
        public int timeMax = 40;//the time spent speaking
        public int freezePlayer = 40;//to set the length that player cant move
        public bool active = false;
        public bool col = false;
        public bool followPlayer = false;
        public bool onPlayer = false;//used to set ghost on top of player.

        public SoundEffect room1; SoundEffectInstance room1_; 
        public SoundEffect room2; SoundEffectInstance room2_;
        public SoundEffect room3; SoundEffectInstance room3_;
        public SoundEffect room4; SoundEffectInstance room4_;
        public SoundEffect room5; SoundEffectInstance room5_;
        public SoundEffect room6; SoundEffectInstance room6_;
        public SoundEffect room7; SoundEffectInstance room7_;
        public SoundEffect room8; SoundEffectInstance room8_;
        public SoundEffect room9; SoundEffectInstance room9_;
        public SoundEffect room10; SoundEffectInstance room10_;

        public SoundEffect gotYou, here, worry; SoundEffectInstance gotYou_,here_,worry_;
        Random rand= new Random();
        int soundnum,sndRTimer;

        public override void Initialize()
        {
            gotYou_ = gotYou.CreateInstance();
            here_ = here.CreateInstance();
            worry_ = worry.CreateInstance();
            room1_ = room1.CreateInstance();
            room2_ = room2.CreateInstance();
            room3_ = room3.CreateInstance();
            room4_ = room4.CreateInstance();
            room5_ = room5.CreateInstance();
            room6_ = room6.CreateInstance();
            room7_ = room7.CreateInstance();
            room8_ = room8.CreateInstance();
            room9_ = room9.CreateInstance();
            room10_ = room10.CreateInstance();
        }

        public void SetPositions(int x, int y)
        {
            rec.X = x;
            rec.Y = y;
            rec.Width = 60;
            rec.Height = 80;
           

            colRec.Width = 20;
            colRec.Height = 200;
        }

        public void SetColRec(int room)
        {
            if (room == 1)
            {
                colRec.X = 140;
                colRec.Y = 820;
            }
            else if (room == 2)
            {
                colRec.X = 110;
                colRec.Y = 820;
            }
            else if (room == 3)
            {
                colRec.X = 630;
                colRec.Y = 0;
            }
            else if (room == 4)
            {
                colRec.X = 300;
                colRec.Y = 820;
            }
            else if (room == 5)
            {
                colRec.X = 600;
                colRec.Y = 385;
            }
            else if (room == 6)
            {
                colRec.X = 110;
                colRec.Y =820;
            }
            else if (room == 7)
            {
                colRec.X = 690;
                colRec.Y = 450;
            }
            else if (room == 8)
            {
                colRec.X = 110;
                colRec.Y = 820;
            }
            else if (room == 9)
            {
                colRec.X =680;
                colRec.Y =20;
            }
            else if (room == 10)
            {
                colRec.X = 200;
                colRec.Y = 70;
                colRec.Width = 200;
                colRec.Height = 20;
            }
        }

       // int disTimer;
        public void Disappear(Player player)
        {
            sndRTimer++;

            if (!player.alive)
            {
                if (sndRTimer > 6)
                {
                    soundnum = rand.Next(1, 4);
                }
                sndRTimer = 0;
                onPlayer = true;
            }
            else
            {  onPlayer = false;  }

            if (onPlayer)
            {
                rec.X = player.rec.X;
                rec.Y = player.rec.Y;

              
            }
            if (sndRTimer < 8)
            {
                if (soundnum == 1)
                {
                    gotYou_.Play();

                }
                if (soundnum == 2)
                {

                    here_.Play();
                }
                if (soundnum == 3)
                {
                    worry_.Play();
                }
            }
            else
            {
                worry_.Stop();
                here_.Stop();
                gotYou_.Stop();
            }
        }

        public void Update(int room,Player player)
        {
            animation.SetAnimation(120, 4, 60);
            animation.Place(new Vector2(player.rec.X,player.rec.Y));
            if (player.alive)
            {
                FollowPlayer(player);
            }
            Disappear(player);
            if (col)
            {
                timer++;

                if (room == 1)
                {
                    timeMax = 410;//14 seconds = 410. 1 second = 29(approx)
                    freezePlayer = 40;
                    UpdateRoom1();
                }
                else
                { freezePlayer = 5; }

                if (room == 2)
                {
                    timeMax = 320;
                    UpdateRoom2();
                }
                if (room == 3)
                {
                    timeMax = 440;
                    UpdateRoom3();
                }
                if (room == 4)
                {
                    timeMax = 320;
                    UpdateRoom4();
                }
                if (room == 5)
                {
                    timeMax = 285;
                    UpdateRoom5();
                }
                if (room == 6)
                {
                    timeMax = 300;
                    UpdateRoom6();
                }
                if (room == 7)
                {
                    timeMax = 200;
                    UpdateRoom7();
                }
                if (room == 8)
                {
                    timeMax = 330;
                    UpdateRoom8();
                }
                if (room == 9)
                {
                    timeMax = 425;
                    UpdateRoom9();
                }
                if (room == 10)
                {
                    timeMax = 380;
                    UpdateRoom10();
                }
            }
            if (timer > timeMax)
            {
                col = false;
            }
        }

        public void FollowPlayer(Player player)
        {
            int left = 2, right = -2;
            if (timer > timeMax)
            {
                followPlayer = false;
            }

            if (!followPlayer)
            {
                rec.X = -1000;
                rec.Y = -1000;

            }
            else
            {
                if (player.direction == right)
                {
                    rec.X = player.rec.X - 70;
                    rec.Y = player.rec.Y - 20;
                }
                else if(player.direction==left)
                {
                    rec.X = player.rec.X + 120;
                    rec.Y = player.rec.Y - 20;
                }
            }
        }

        private void UpdateRoom1()
        {
            if (timer < timeMax)
            {
                room1_.Play();
            }
            else
            {
                room1_.Stop();
            }

        }

        private void UpdateRoom2()
        {
            if (timer < timeMax)
            {
                room2_.Play();
            }
            else { room2_.Stop(); }
        }

        private void UpdateRoom3()
        {
            if (timer < timeMax)
            {
                room3_.Play();
            }
            else { room3_.Stop(); }
        }

        private void UpdateRoom4()
        {

            if (timer < timeMax)
            {
                room4_.Play();
            }
            else { room4_.Stop(); }
        }

        private void UpdateRoom5()
        {
            if (timer < timeMax)
            {
                room5_.Play();
            }
            else { room5_.Stop(); }
        }

        private void UpdateRoom6()
        {

            if (timer < timeMax)
            {
                room6_.Play();
            }
            else { room6_.Stop(); }
        }

        private void UpdateRoom7()
        {

            if (timer < timeMax)
            {
                room7_.Play();
            }
            else { room7_.Stop(); }
        }

        private void UpdateRoom8()
        {

            if (timer < timeMax)
            {
                room8_.Play();
            }
            else { room8_.Stop(); }
        }

        private void UpdateRoom9()
        {

            if (timer < timeMax)
            {
                room9_.Play();
            }
            else { room9_.Stop(); }
        }

        private void UpdateRoom10()
        {

            if (timer < timeMax)
            {
                room10_.Play();
            }
            else { room10_.Stop(); }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
           // spriteBatch.Draw(motherTex, colRec, Color.AntiqueWhite);//for testing the position of the voice activator

            if (!onPlayer)
            {
                spriteBatch.Draw(motherTex, rec, Color.AntiqueWhite);
            }
            else
            {
                animation.Draw(gameTime,spriteBatch,Color.AntiqueWhite);
            }
        }
    }
}
