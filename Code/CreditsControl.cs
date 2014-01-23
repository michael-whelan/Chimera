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
using Microsoft.Xna.Framework.Input.Touch;


namespace Chimera
{
    public class CreditsControl : Microsoft.Xna.Framework.GameComponent
    {
        public CreditsControl(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Texture2D texture;
        Rectangle rec;

        public Texture2D btnUpImg, btnDwnImg, btnUpImgP, btnDwnImgP;
        Rectangle btnUpRec, btnDwnRec;
        bool top = false,bottom=false;
        int btnW = 50, btnH = 80;
        bool up=false, down = false;

        public void Set(int w)//viewport width
        {
            rec.X = 0;
            rec.Y = -1;
            rec.Width = w;
            rec.Height = 2000;
            btnUpRec.X = 550;
            btnUpRec.Y = 50;
            btnUpRec.Width = btnW;
            btnUpRec.Height = btnH;

            btnDwnRec.X = 550;
            btnDwnRec.Y = 270;   
            btnDwnRec.X = 550;
            btnDwnRec.Width = btnW;
            btnDwnRec.Height = btnH;
        }

        public void Update()
        {
            down = false; up = false; 
            foreach (TouchLocation tl in TouchPanel.GetState())
            {
                if (btnUpRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                {
                    if (top != true)
                    {
                        rec.Y += 6;
                    }
                    up = true;
                }
                if (btnDwnRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                {
                    if (bottom != true)
                    {
                        rec.Y -= 5;
                    }
                    down = true;
                }
            }//foreach touch
              

            if (bottom != true)
            {
                rec.Y--;
            }

            if (rec.Y > 0)
            {
                rec.Y = 0;
                top = true;
            }
            else if (rec.Y < -1200)
            {
                rec.Y = -1200;
                bottom = true;
            }
            else { bottom = false; top = false; }

        }//update

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, Color.AntiqueWhite);
            if (!up)
            {
                spriteBatch.Draw(btnUpImg, btnUpRec, Color.AntiqueWhite);
            }
            else { spriteBatch.Draw(btnUpImgP, btnUpRec, Color.AntiqueWhite); }
            if (!down)
            {
                spriteBatch.Draw(btnDwnImg, btnDwnRec, Color.AntiqueWhite);
            }
            else { spriteBatch.Draw(btnDwnImgP, btnDwnRec, Color.AntiqueWhite); }
        }
    }
}
