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

    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        public CollisionManager(Game game)
            : base(game)
        {
        }

        public bool drkT = false;

        public void CamCol(Rectangle markerX,Rectangle markerY,CameraWall camWallX,CameraWall camWallY,Camera camera)
        {
            if (markerX.Intersects(camWallX.rec))
            {
                camera.stopX = true;
            }
            else { camera.stopX = false; }

            if (markerY.Intersects(camWallY.rec))
            {
                camera.stopY = true;
            }
            else { camera.stopY = false; }
        }

        public void OutOfBounds(KillBox boundary,Player player)
        {
            if (!player.rec.Intersects(boundary.rec))
            {
                player.alive = false;
            }
        }

        public void SoundMaker(Player player,Bug[] bug,Plant[] plant,Hopper[] hopper,Tree []tree)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.rec.Intersects(hopper[i].sndDist))
                {
                    hopper[i].playSnd = true;
                }
                else { hopper[i].playSnd = false; }

                if (player.rec.Intersects(tree[i].sndDistT))
                {
                    tree[i].playSndT = true;
                }
                else { tree[i].playSndT = false; }

                if (player.rec.Intersects(tree[i].sndDistV))
                {
                    tree[i].playSndV = true;
                }
                else { tree[i].playSndV = false; }

            }

            for (int i = 0; i < 10; i++)
            {//bug
                if (player.rec.Intersects(bug[i].sndDist))
                {
                    bug[i].playSnd = true;
                }
                else { bug[i].playSnd = false; }
                //plant
                if (player.rec.Intersects(plant[i].sndDist))
                {
                    plant[i].playSnd = true;
                }
                else { plant[i].playSnd = false; }
            }

        }

        public void ActivateCol(Player player, Tree[] tree)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.rec.Intersects(tree[i].vineRec))
                {
                    player.slide = false;
                    if (tree[i].direction == 1)//tree direction checks if the vine is verticle/horizontal
                    {
                        if (player.direction == 2)
                        {
                            player.position.X += 4;
                        }
                        else if (player.direction == -2)
                        {
                            player.position.X -= 4;
                        }
                    }
                }

                if (player.rec.Intersects(tree[i].treeRec))
                {
                    if (player.actionButton)
                    {
                        // npcM.tree[i].Vine();
                        tree[i].activate = true;
                    }
                }

            }
        }

        public void CutSceneCol(int timer, Player player, Guide guide, Rectangle startBox, int speechLen,int roomNum)
        {
            if (player.rec.Intersects(guide.colRec) && !player.rec.Intersects(startBox))
            {
                if (timer < speechLen)
                {
                    player.sceneStop = true;

                    guide.col = true;
                    guide.followPlayer = true;
                    if (player.slide)
                    {
                        player.slide = false;
                    }
                    if (roomNum == 10)
                    {
                       drkT = true;
                    }
                }
                else
                {
                    player.sceneStop = false;

                }
            }
        }

        public void Update(Player player, Bug[] bug, Plant[] plant, Hopper[] hopper, Background backG)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.rec.Intersects(hopper[i].rec))
                {
                    player.alive = false;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (player.rec.Intersects(plant[i].drawRec) && plant[i].close == true)
                {
                    player.alive = false;
                }
                if (player.rec.Intersects(bug[i].rec))
                {
                    player.alive = false;
                }
            }
            if (!player.alive)
            {
                backG.Initialize();
            }
        }
    }
}
