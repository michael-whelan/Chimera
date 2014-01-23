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
    public class Background : Microsoft.Xna.Framework.GameComponent
    {
        public Background(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        Texture2D draw;
        public Rectangle rec;
        public Texture2D back1, back2, back3, back4, back5, back6, back7, back8, back9, back10;

        int pX, pY, pXOld, pYOld;

       public SoundEffect menu,gameplay,credits; public SoundEffectInstance menu_,gameplay_,credits_;

     

       public void InitializeSounds()
       {

           menu_ = menu.CreateInstance();
           gameplay_ = gameplay.CreateInstance();
           credits_ = credits.CreateInstance();
           menu_.IsLooped = true;
           gameplay_.IsLooped = true;
           credits_.IsLooped = true;
           gameplay_.Volume = 0.7f;

       }

       public override void Initialize()
       {
           draw = back1;
           rec.X = -200;
           rec.Y = -300;
           rec.Width = 1200;
           rec.Height = 800;
       }

        int timer;
        public void Update(int room,Player player)
        {

            timer++;
            pX = player.rec.X;
            pY = player.rec.Y;

            if (timer > 2)
            {
                if (pX > pXOld)
                {
                    rec.X--;
                }
                else if (pX < pXOld)
                {
                    rec.X++;
                }
                if (pY > pYOld)
                {
                    rec.Y--;
                }
                else if (pY < pYOld)
                {
                    rec.Y++;
                }
                timer = 0;
            }
            //draw = back1;

            if (room == 1)
            {
                draw = back1;
            }
            if (room == 2)
            {
                draw = back2;
            }
            if (room == 3)
            {
                draw = back3;
            }
            if (room == 4)
            {
                draw = back4;
            }
            if (room == 5)
            {
                draw = back5;
            }
            if (room == 6)
            {
                draw = back6;
            }
            if (room == 7)
            {
                draw = back7;
            }
            if (room == 8)
            {
                draw = back8;
            }
            if (room == 9)
            {
                draw = back9;
            }
            if (room == 10)
            {
                draw = back10;
            }

            pXOld = pX;
            pYOld = pY;
        }

      /*  public void Menu()
        {
           
            cut1_.Stop(); room1_.Stop(); room4_.Stop();
            room7_.Stop(); room10_.Stop(); credits_.Stop();
            menu_.Play(); 
        }

        public void Cut1()
        {
            menu_.Stop(); room1_.Stop(); room4_.Stop();
            room7_.Stop(); room10_.Stop(); credits_.Stop();
            cut1_.Play();
        }

        public void Room1()
        {
            
            menu_.Stop(); cut1_.Stop();  room4_.Stop();
            room7_.Stop(); room10_.Stop(); credits_.Stop();
            room1_.Play();
        }

        public void Room4()
        {
            
            menu_.Stop(); cut1_.Stop(); room1_.Stop();
            room7_.Stop(); room10_.Stop(); credits_.Stop();
            room4_.Play();
        }

        public void Room7()
        {
           
            menu_.Stop(); cut1_.Stop(); room1_.Stop(); room4_.Stop();
            room10_.Stop(); credits_.Stop();
            room7_.Play();
        }

        public void Room10()
        {
           
            menu_.Stop(); cut1_.Stop(); room1_.Stop(); room4_.Stop();
            room7_.Stop(); credits_.Stop();
            room10_.Play(); 
        }

        public void Credits()
        {
          
            menu_.Stop(); cut1_.Stop(); room1_.Stop(); room4_.Stop();
            room7_.Stop(); room10_.Stop();
            credits_.Play();
        }*/

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(draw,rec,Color.AntiqueWhite);
        }
    }
}
