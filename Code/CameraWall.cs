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

    public class CameraWall : Microsoft.Xna.Framework.GameComponent
    {
        public CameraWall(Game game)
            : base(game)
        {
        }

        public Texture2D txt2D;
        public Rectangle rec;
        int hor=-1, vert=1;

        public void Set(int var, int dir)
        {
            if (dir == vert)
            {
                rec.X = var;
                rec.Y = 0;
                rec.Width = 800;
                rec.Height = 3000;
            }
            else if (dir == hor)
            {
                rec.X = 0;
                rec.Y = var;
                rec.Width = 3000;
                rec.Height = 800;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txt2D,rec,Color.AntiqueWhite);
        }
    }
}
