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

    public class NPCManager : Microsoft.Xna.Framework.GameComponent
    {
               public NPCManager(Game game)
            : base(game)
        {
        }
         public Guide guide;

        public Bug[] bug = new Bug[10];
        public Texture2D walkL, walkR;
        public SoundEffect bugSnd;

        public Tree[] tree = new Tree[5];
        public Texture2D treeTex;
        public Texture2D vineTexH,vineTexV;
        public SoundEffect treeSnd, vineSnd;
        int vert = 1, hor = -1;

        public Plant[] plant = new Plant[10];
        public Texture2D plantOpen;
        public Texture2D plantClose;
        public SoundEffect plantSnd;

        public Hopper[] hopper = new Hopper[5];
        public Texture2D hopper1;
        public Texture2D hopper2;
        public SoundEffect hopperSnd;

        public override void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {

                bug[i] = new Bug(Game);
                
                bug[i].texBugL = walkL;
                bug[i].texBugR = walkR;
                bug[i].attackL = walkL;
                bug[i].attackR = walkR;
                bug[i].snd = bugSnd;
                bug[i].Initialize(100);

                plant[i] = new Plant(Game);
                plant[i].texClosed = plantClose;
                plant[i].texOpen = plantOpen;
                plant[i].snd = plantSnd;
                plant[i].Initialize();
            }

            for (int i = 0; i < 5; i++)
            {
                tree[i] = new Tree(Game);
                tree[i].texTree = treeTex;
                tree[i].texVineH = vineTexH;
                tree[i].texVineV = vineTexV;
                tree[i].sndTree = treeSnd;
                tree[i].sndVine = vineSnd;
                tree[i].Initialize();

                hopper[i] = new Hopper(Game);
                hopper[i].texHopper1 = hopper1;
                hopper[i].texHopper2 = hopper2;
                hopper[i].snd = hopperSnd;

            }
            Reset();
        }

        public void Reset()
        {
            guide.SetPositions(-1000,-1000);
            for (int i = 0; i < 10; i++)
            {
                bug[i].position = new Vector2(-1000, -1000);
                bug[i].alive = false;
                plant[i].pos = new Vector2(-1000,-1000);
                plant[i].alive = false;
                plant[i].delay = 0;
            }
            for (int i = 0; i < 5; i++)
            {
                tree[i].SetPositions(new Vector2(-1000, -1000), new Vector2(-1000, 1000), 0,0,vert);
                tree[i].activate = false;

                hopper[i].Initialize(new Vector2(-1000, -1000), 100);

            }
        }

        public void Update(int room,Player player)
        {
            guide.Update(room,player);
            for (int i = 0; i < 10; i++)
            {
                bug[i].Update();
                plant[i].Update();
            }
            for (int i = 0; i < 5; i++)
            {
                tree[i].Update();
                hopper[i].Update();

            }
        }

        public void Set(int room)//puts needed ground in room
        {
            if (room == 1)
            {
                guide.timer = 0;
            }
            else if (room == 2)
            {
                guide.timer = 0;
            }
            else if (room == 3)
            {
                SetRoom3();
                guide.timer = 0;
            }
             else if (room == 4)
             {
                 SetRoom4();
                 guide.timer = 0;
             }
             else if (room == 5)
             {
                 SetRoom5();
                 guide.timer = 0;
             }
             else if (room == 6)
             {
                 SetRoom6();
                 guide.timer = 0;
             }
             else if (room == 7)
             {
                 SetRoom7();
                 guide.timer = 0;
             }
             else if (room == 8)
             {
                 SetRoom8();
                 guide.timer = 0;
             }
             else if (room == 9)
             {
                 SetRoom9();
                 guide.timer = 0;
             }
            else if (room == 10)
            {
                SetRoom10();
                tree[0].vineSpeed = 2;
                guide.timer = 0;
            }
            if(room!=10) { tree[0].vineSpeed = 1; }
        }
        //all npcs y is +10.
        public void SetRoom3()
        {
           // bug[0].alive = true;
            bug[0].position = new Vector2(630, 190); bug[0].Initialize(345);
        }

        public void SetRoom4()
        {
            bug[0].position = new Vector2(825, 545); bug[0].Initialize(305);
            plant[0].pos = new Vector2(510,1010);
        }

        public void SetRoom5()
        {
            plant[0].pos = new Vector2(210, 1010);
            plant[1].pos = new Vector2(310, 1010); plant[1].delay = 20;
            plant[2].pos = new Vector2(410, 1010); plant[2].delay = 40;

            bug[0].position = new Vector2(1400, 840); bug[0].Initialize(100);
        }

        public void SetRoom6()
        {
            bug[0].position = new Vector2(410, 490); bug[0].Initialize(770);
            bug[1].position = new Vector2(625, 490); bug[1].Initialize(555);
            bug[2].position = new Vector2(815, 490); bug[2].Initialize(365);

            tree[0].SetPositions(new Vector2(400, 925), new Vector2(300, 1020), 200,30, hor);
        }

        public void SetRoom7()
        {
            bug[0].position = new Vector2(100, 990); bug[0].Initialize(80);

            //tree[0].SetPositions(new Vector2(400, 900), new Vector2(300, 1000), 200, 30, false, hor);
            tree[0].SetPositions(new Vector2(100, 110), new Vector2(1350, 60), 30, 150, vert);
        }

        public void SetRoom8()
        {
            bug[0].position = new Vector2(350, 640); bug[0].Initialize(150);
            bug[1].position = new Vector2(1350, 990); bug[1].Initialize(150);

            tree[0].SetPositions(new Vector2(110, 210), new Vector2(1300, 960), 30, 50, vert);
        }

        public void SetRoom9()
        {
            tree[0].SetPositions(new Vector2(200, 910), new Vector2(100, 1030), 200, 30, hor);

            plant[0].pos = new Vector2(900,610);

            tree[1].SetPositions(new Vector2(20,210),new Vector2(1140,690),30,370,vert);
            tree[2].SetPositions(new Vector2(1250,510),new Vector2(1190,690),30,370,vert);
            tree[3].SetPositions(new Vector2(1250,110),new Vector2(1250,690),30,370,vert);

            hopper[0].Initialize(new Vector2(100,235),200);
            hopper[1].Initialize(new Vector2(830,140),400);
        }

        public void SetRoom10()
        {
            plant[0].pos = new Vector2(600,960);
            plant[1].pos = new Vector2(700,1610);
            plant[2].pos = new Vector2(850, 1710); plant[2].delay = 10;
            plant[3].pos = new Vector2(700, 1810); plant[3].delay = 20;
            plant[4].pos = new Vector2(550, 1910); plant[4].delay = 30;
            plant[5].pos = new Vector2(400, 2010); plant[5].delay = 40;

            tree[0].SetPositions(new Vector2(1350,960),new Vector2(-10,1590),1500,30,hor);

            hopper[0].Initialize(new Vector2(860, 2135), 320);
        }

        public void DrawVines(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 5; i++)
            {
                tree[i].DrawVines(spriteBatch);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color, GameTime gameTime)
        {
            for (int i = 0; i < 10; i++)
            {
                bug[i].Draw(spriteBatch, gameTime);
                plant[i].Draw(spriteBatch, gameTime);
            }
            for (int i = 0; i < 5; i++)
            {
                tree[i].Draw(spriteBatch);
                hopper[i].Draw(spriteBatch, gameTime);
            }
            guide.Draw(spriteBatch,gameTime);
        }
    }
}

