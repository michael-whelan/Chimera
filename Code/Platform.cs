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
    public class Platform : Microsoft.Xna.Framework.GameComponent
    {
        public Platform(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Texture2D texture;
        public Vector2 position = new Vector2();
        Vector2 pos1 = new Vector2();
        Vector2 pos2 = new Vector2();

        public Vector2 size = new Vector2();
        public Rectangle rec;
        int vel = 2;
        int direction, vert = 1, hor = -1;


        public void Initialize(int d, int dir)
        {
            direction = dir;
            pos1 = position;
            if (direction == hor)
            {
                pos2.X = pos1.X + d;
                pos2.Y = pos1.Y;
            }
            else if (direction == vert)
            {
                pos2.X = pos1.X;
                pos2.Y = pos1.Y + d;
            }
        }


        public void Update()
        {
            if (position.X > pos2.X || position.X < pos1.X || position.Y > pos2.Y || position.Y < pos1.Y)
            {
                vel *= -1;
            }


            if (direction == hor)
            {
                position.X += vel;
            }
            else if (direction == vert)
            {
                position.Y += vel;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rec = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, rec, Color.AntiqueWhite);
        }
    }
}
