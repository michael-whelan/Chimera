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

    public class Ground : Microsoft.Xna.Framework.GameComponent
    {
        public Ground(Game game)
            : base(game)
        {
        }

        public Texture2D draw,texture1,texture2;
        public Vector2 pos = new Vector2();
        public Vector2 size = new Vector2();
        public Rectangle rec;
        public Rectangle colRec;
        public bool breakable = false;
        int timer;
        public bool fall = false;

        public override void Initialize()
        {
            draw = texture1;
        }

        public void Update()
        {
            
            timer++;

            if (timer >= 60)
            {
                pos.Y += 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (size.Y < 55)
            {
                draw = texture2;
            }
            else
            {
                draw = texture1;
            }
            rec = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            colRec = new Rectangle(rec.X, rec.Y+(rec.Height/10), rec.Width, rec.Height);
            spriteBatch.Draw(draw, rec, color);
        }
    }
}
