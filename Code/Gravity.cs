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

    public class Gravity : Microsoft.Xna.Framework.GameComponent
    {
        public Gravity(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        bool grounded;
        float gravity = 0.4f;
        int timer;
        public float yVel;
        int hor = -1;

        public void Update(Player player, Ground[] ground, Platform[] plat, Tree[] tree, Plant[] plant)
        {
            timer++;
            player.position.Y += yVel;
            yVel += gravity;
            grounded = false;

            for (int i = 0; i < 50; i++)
            {
                if (player.feetRec.Intersects(ground[i].colRec))
                {
                    player.inAir = false;
                    if (yVel > 0)
                    {
                        player.jumpBool = false;
                    }
                    yVel = 0;
                    gravity = 0.0f;
                    grounded = true;
                    player.position.Y = ground[i].colRec.Y - player.rec.Height;//if collision is true then the player is set to ground position
                }

                if (player.feetRec.Intersects(plat[i].rec))
                {
                    player.inAir = false;
                    if (yVel > 0)
                    {
                        player.jumpBool = false;
                    }
                    yVel = 0;
                    gravity = 0.0f;
                    grounded = true;
                    player.position.Y = plat[i].rec.Y - player.rec.Height - 1;

                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (tree[i].direction == hor)
                {
                    if (player.feetRec.Intersects(tree[i].vineRec))
                    {
                        player.inAir = false;
                        if (yVel > 0)
                        {
                            player.jumpBool = false;
                        }
                        yVel = 0;
                        gravity = 0.0f;
                        grounded = true;
                        player.position.Y = tree[i].vineRec.Y - player.rec.Height;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (player.feetRec.Intersects(plant[i].colRec))
                {
                    player.inAir = false;
                    if (yVel > 0)
                    {
                        player.jumpBool = false;
                    }
                    yVel = 0;
                    gravity = 0.0f;
                    grounded = true;
                    player.position.Y = plant[i].colRec.Y - player.rec.Height;
                }
            }

            if (grounded == false)
            {
                gravity = 0.3f;
                player.inAir = true;
            }


            if (timer >= 30)
            {
                if (player.jumpBool == true)
                {
                    if (grounded == true)
                    {
                        yVel = -6;
                        timer = 0;
                    }
                }
            }
            player.position.Y += yVel;
        }

    }
}
