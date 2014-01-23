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

    public class Tree : Microsoft.Xna.Framework.GameComponent
    {
        public Tree(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Texture2D texTree;
        public Rectangle treeRec;
        Vector2 posTree = new Vector2();
        // public bool solid;
        public Texture2D texVineH,texVineV;
        public Rectangle vineRec,vineDraw;
        Vector2 vinePos;//current vine pos
        Vector2 vineSize;

        Vector2 vinePos2;//solid vine pos
        public int vineSpeed = 1;
        int timer; //used to control vine growth speed
        public int direction;
        int vert = 1, hor = -1;

        public SoundEffect sndTree; SoundEffectInstance sndTree_;
        public SoundEffect sndVine; SoundEffectInstance sndVine_;

        public Rectangle sndDistT;//used to only play sound when intersected T for tree V for vine
        public bool playSndT = false; int sndTTimer;


        public Rectangle sndDistV;
        public bool playSndV = false;


        public bool activate = false;

        public override void Initialize()
        {
            sndTree_ = sndTree.CreateInstance();
            sndVine_ = sndVine.CreateInstance();
        }

        public void SetPositions(Vector2 p, Vector2 vP, int vW, int vH, int dir)//pos, vine pos, vine size
        {
            posTree = p;
            vinePos = vP;
            vineSize.X = vW;
            vineSize.Y = vH;
            direction = dir;
            InitializeVine();
        }

        public void InitializeVine()
        {
            if (direction == vert)
            {
                vinePos2.X = vinePos.X;
                vinePos2.Y = vinePos.Y + vineSize.Y+25;
            }
            else if (direction == hor)
            {
                vinePos2.X = vinePos.X + vineSize.X;
                vinePos2.Y = vinePos.Y;
            }
        }

        public void Update()//direction verticle or horizontal
        {
            timer++;
           
            sndDistT = new Rectangle(treeRec.X - 150, treeRec.Y - 150, 300, 300);
            sndDistV = new Rectangle(vineRec.X - 200, vineRec.Y - 200, 400, 400);

            if (activate)
            {
                if (playSndT)
                {
                    sndTTimer++;
                    if (sndTTimer < 80)
                    {
                        sndTree_.Play();
                    }
                    else { sndTree_.Stop(); sndTTimer = 0; }
                }

                if (vinePos.Y < vinePos2.Y)
                {
                    if (playSndV)
                    {
                        sndVine_.Play();
                    }
                    if (timer > 2)
                    {
                        vinePos.Y += vineSpeed;
                        timer = 0;
                    }
                }
                else { sndVine_.Stop(); }
             
      
                if (vinePos.X < vinePos2.X)
                {
                    if (playSndV)
                    {
                        sndVine_.Play();
                    }
                    if (timer > 2)
                    {
                        vinePos.X+=vineSpeed;
                        timer = 0;
                    }
                }
                else { sndVine_.Stop(); }
            }
        }

        public void DrawVines(SpriteBatch spriteBatch)
        {
            if (direction == hor)
            {
                spriteBatch.Draw(texVineH, vineDraw, Color.AntiqueWhite);
            }
            else
            {
                spriteBatch.Draw(texVineV, vineDraw, Color.AntiqueWhite);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            treeRec = new Rectangle((int)posTree.X, (int)posTree.Y, 70, 100);
            vineRec = new Rectangle((int)vinePos.X, (int)vinePos.Y, (int)vineSize.X, (int)vineSize.Y);
            vineDraw = new Rectangle((int)vinePos.X, (int)vinePos.Y-10, (int)vineSize.X, (int)vineSize.Y);

            spriteBatch.Draw(texTree, treeRec, Color.AntiqueWhite);
        }
    }
}
