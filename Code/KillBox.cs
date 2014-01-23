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

    public class KillBox : Microsoft.Xna.Framework.GameComponent
    {
        public KillBox(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Texture2D back;
        public Rectangle rec;

        public void Set(int w,int h)
        {
            rec.X = -80;
            rec.Y = -80;
            rec.Width = w;
            rec.Height = h;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(back,rec,Color.AntiqueWhite);
        }
    }
}
