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
    public class Wall : Microsoft.Xna.Framework.GameComponent
    {
        public Wall(Game game)
            : base(game)
        {
        }
        public Texture2D wallTex;
        public Rectangle rec;
        public Vector2 pos = new Vector2();
        public Vector2 size = new Vector2();


        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            rec = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(wallTex, rec, color);
        }
    }
}