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

    public class GroundManager : Microsoft.Xna.Framework.GameComponent
    {

        public GroundManager(Game game)
            : base(game)
        {
            camWallX = new CameraWall(game);
            camWallY = new CameraWall(game);
            boundsBox = new KillBox(game);
        }

        public KillBox boundsBox;
        public Rectangle startBox;
        public Rectangle endBox;
        public Texture2D startEnd;
        public Ground[] ground = new Ground[50];
        public Texture2D gTex1,gTex2,gTex3;
        public CameraWall camWallX;
        public CameraWall camWallY;
        public Platform[] plat = new Platform[50];
        int vert = 1, hor = -1;

        public Wall[] wall = new Wall[10];
        public Texture2D wallTex;


        public override void Initialize()
        {
            startBox.Height = 70;
            startBox.Width = 50;
            endBox.Height = 70;
            endBox.Width = 50;

            for (int i = 0; i < 50; i++)
            {
                ground[i] = new Ground(Game);
                ground[i].texture1 = gTex1;
                ground[i].texture2 = gTex2;
                ground[i].Initialize();
                plat[i] = new Platform(Game);
                plat[i].texture = gTex3;
            }
            for (int i = 0; i < 10; i++)
            {
                wall[i] = new Wall(Game);
                wall[i].wallTex = wallTex;
            }
            Reset();
            
            base.Initialize();
        }

        public void Reset()//puts all ground out of room
        {
            for (int i = 0; i < 50; i++)
            {
                ground[i].pos = new Vector2(-1000, -1000);
                ground[i].size = new Vector2(100, 25);
                ground[i].breakable = false;
                ground[i].fall = false;
                plat[i].position = new Vector2(-1000, -1000);
                plat[i].size = new Vector2(100, 25);
            }
            for (int i = 0; i < 10; i++)
            {
                wall[i].pos = new Vector2(-1000, -1000);
                wall[i].size = new Vector2(100, 330);
            }
        }

        public void Set(int room)//puts needed ground in room
        {
            if (room == 1)
            {
                SetRoom1();
            }
            else if (room == 2)
            {
                SetRoom2();
            }
            else if (room == 3)
            {
                SetRoom3();
            }
            else if (room == 4)
            {
                SetRoom4();
            }
            else if (room == 5)
            {
                SetRoom5();
            }
            else if (room == 6)
            {
                SetRoom6();
            }
            else if (room == 7)
            {
                SetRoom7();
            }
            else if (room == 8)
            {
                SetRoom8();
            }
            else if (room == 9)
            {
                SetRoom9();
            }
            else if (room == 10)
            {
                SetRoom10();
            }
        }

        private void SetRoom1()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1550; endBox.Y = 1010;
            camWallX.Set(1600, vert);
            camWallY.Set(1200,hor);
            boundsBox.Set(1650,1250);

            //pos                          //x,y                //size             width,height
            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(600, 300);

            ground[4].pos = new Vector2(650, 1050); ground[4].size = new Vector2(200, 300);
            ground[5].pos = new Vector2(750, 1025); ground[5].size.Y = 200;
            ground[6].pos = new Vector2(800, 1000); ground[6].size = new Vector2(50, 200);

            ground[7].pos = new Vector2(900, 1000); 

            ground[8].pos = new Vector2(1075, 1025); ground[8].size = new Vector2(150, 300);

            ground[9].pos = new Vector2(1250, 1025); ground[9].size = new Vector2(175, 300);
            ground[10].pos = new Vector2(1375, 1000); ground[10].size = new Vector2(50, 150);//150-25

            ground[11].pos = new Vector2(1500, 1050); ground[11].size = new Vector2(125, 300);
        }

        private void SetRoom2()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1850; endBox.Y = 1025;
            camWallX.Set(1910, vert);
            camWallY.Set(1275, hor);
            boundsBox.Set(1960, 1325);

            //pos                          //x,y                //size             width,height
            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(300, 200);

            ground[1].pos = new Vector2(350, 950);
            ground[2].pos = new Vector2(500, 900);
            ground[3].pos = new Vector2(650, 850);
            ground[4].pos = new Vector2(800, 800);
            ground[5].pos = new Vector2(950, 750);
            ground[6].pos = new Vector2(800, 700);
            ground[7].pos = new Vector2(650, 650);
            ground[8].pos = new Vector2(500, 600);
            ground[9].pos = new Vector2(350, 550);
            ground[10].pos = new Vector2(500, 500);

            ground[11].pos = new Vector2(650, 450); ground[11].size = new Vector2(600, 50);
            ground[12].pos = new Vector2(900, 400); ground[12].size = new Vector2(100, 100);
            ground[13].pos = new Vector2(1000, 350); ground[13].size = new Vector2(100, 150);
            ground[14].pos = new Vector2(1100, 425); ground[14].size = new Vector2(150, 50);

            ground[15].pos = new Vector2(1300, 425); ground[15].size = new Vector2(200, 50);

            for (int i = 16; i < 21; i++)
            {
                ground[i].size = new Vector2(100, 15);
            }

            ground[16].pos = new Vector2(1445, 565);
            ground[17].pos = new Vector2(1355, 675);
            ground[18].pos = new Vector2(1445, 775);
            ground[19].pos = new Vector2(1355, 875);
            ground[20].pos = new Vector2(1445, 975);

            ground[21].pos = new Vector2(1600, 1075); ground[21].size = new Vector2(100, 200);

            ground[22].pos = new Vector2(1750, 1075); ground[22].size = new Vector2(150, 200);
        }

        private void SetRoom3()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1980; endBox.Y = 1000;
            camWallX.Set(2100, vert);
            camWallY.Set(1250, hor);
            boundsBox.Set(2150, 1350);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(350, 200);

            plat[0].position = new Vector2(300, 600); plat[0].Initialize(300, vert);
            ground[9].pos = new Vector2(450, 600);
           // plat[1].position = new Vector2(450, 400); plat[1].Initialize(250, vert);
            plat[2].position = new Vector2(300, 200); plat[2].Initialize(350, vert);


            ground[2].pos = new Vector2(550, 150); ground[2].size = new Vector2(50, 60);
            ground[3].pos = new Vector2(600, 100); ground[3].size = new Vector2(25, 110);
            ground[4].pos = new Vector2(1000, 150); ground[4].size = new Vector2(50, 60);
            ground[5].pos = new Vector2(1050, 100); ground[5].size = new Vector2(25, 110);
            ground[1].pos = new Vector2(500, 200); ground[1].size = new Vector2(600, 50);
            

            plat[3].position = new Vector2(1200, 200); plat[3].Initialize(225, vert);

            ground[6].pos = new Vector2(1150, 500);

            plat[4].position = new Vector2(1000, 500); plat[4].Initialize(400, vert);

            ground[7].pos = new Vector2(1200, 1000);

            plat[5].position = new Vector2(1300, 1050); plat[5].Initialize(350, hor);

            ground[8].pos = new Vector2(1830, 1050); ground[8].size = new Vector2(200, 300);
        }

        private void SetRoom4()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1550; endBox.Y = 1050;
            camWallX.Set(1600, vert);
            camWallY.Set(1200, hor);
            boundsBox.Set(1650, 1250);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(500, 300);
            ground[1].pos = new Vector2(650, 950);
            ground[2].pos = new Vector2(800, 900);
            ground[3].pos = new Vector2(950, 850);
            ground[4].pos = new Vector2(775, 800);
            ground[5].pos = new Vector2(625, 750);
            ground[6].pos = new Vector2(475, 700);
            ground[7].pos = new Vector2(350, 650);
            ground[8].pos = new Vector2(500, 600);

            ground[10].pos = new Vector2(775, 500); ground[10].size = new Vector2(50, 60);//flip to place behind
            ground[9].pos = new Vector2(650, 550); ground[9].size = new Vector2(500, 50);
            

            plat[0].position = new Vector2(1250, 640); plat[0].Initialize(400, vert);

            ground[11].pos = new Vector2(1450, 1100); ground[11].size = new Vector2(200, 300);

        }

        private void SetRoom5()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1510; endBox.Y = 800;
            camWallX.Set(1560, vert);
            camWallY.Set(1200, hor);
            boundsBox.Set(1610, 1250);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(200, 300);
            // ground[1].pos = new Vector2(550,1000);

            plat[0].position = new Vector2(550, 750); plat[0].Initialize(250, vert);
            plat[1].position = new Vector2(200, 750); plat[1].Initialize(200, hor);
            plat[2].position = new Vector2(80, 500); plat[2].Initialize(225, vert);

            ground[2].pos = new Vector2(200, 540);
            ground[3].pos = new Vector2(350, 565);

            ground[4].pos = new Vector2(500, 555); ground[4].size = new Vector2(200, 50);

            plat[3].position = new Vector2(800, 575); plat[3].Initialize(225, vert);

            ground[5].pos = new Vector2(950, 800);
            ground[6].pos = new Vector2(1100, 800); ground[6].breakable = true;//break

            ground[8].pos = new Vector2(1350, 810); ground[8].size = new Vector2(50, 50);
            ground[7].pos = new Vector2(1300, 850); ground[7].size = new Vector2(250, 300);
           
        }

        private void SetRoom6()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1550; endBox.Y = 650;
            camWallX.Set(1600, vert);
            camWallY.Set(1200, hor);
            boundsBox.Set(1650, 1250);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(500, 300);
            ground[1].pos = new Vector2(700, 1000); ground[1].size = new Vector2(150, 300);

            ground[2].pos = new Vector2(900, 1000);
            ground[3].pos = new Vector2(1050, 950);
            ground[4].pos = new Vector2(850, 900);
            ground[5].pos = new Vector2(700, 850);
            ground[6].pos = new Vector2(550, 800);
            ground[7].pos = new Vector2(400, 750);

            plat[0].position = new Vector2(250, 500); plat[0].Initialize(200, vert);

            ground[8].pos = new Vector2(400, 500); ground[8].size = new Vector2(800, 50);

            plat[1].position = new Vector2(1250, 500); plat[1].Initialize(200, vert);

            ground[9].pos = new Vector2(1400, 700); ground[9].size = new Vector2(200, 50);
        }

        private void SetRoom7()
        {//endBox tested
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1500; endBox.Y = 150;
            camWallX.Set(1550, vert);
            camWallY.Set(1200, hor);
            boundsBox.Set(1600, 1250);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(200, 300);
            ground[1].pos = new Vector2(200, 950);
            ground[2].pos = new Vector2(350, 900);
            ground[3].pos = new Vector2(500, 850);

            plat[0].position = new Vector2(650, 100); plat[0].Initialize(700, vert);

            ground[4].pos = new Vector2(800, 350);
            ground[5].pos = new Vector2(950, 300);
            ground[6].pos = new Vector2(1100, 250);
            ground[7].pos = new Vector2(1250, 200); ground[7].size = new Vector2(300, 200);

            ground[8].pos = new Vector2(500, 100);
            ground[9].pos = new Vector2(350, 150);

            ground[10].pos = new Vector2(0, 200); ground[10].size = new Vector2(300, 80);
        }

        private void SetRoom8()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1500; endBox.Y = 950;
            camWallX.Set(1550, vert);
            camWallY.Set(1200, hor);
            boundsBox.Set(1600, 1250);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(600, 300);

            wall[0].pos = new Vector2(150, 750); wall[0].size = new Vector2(15, 230);
            wall[1].pos = new Vector2(500, 750); wall[1].size = new Vector2(15, 230);

            plat[0].position = new Vector2(650, 650); plat[0].Initialize(300, vert);

            ground[1].pos = new Vector2(800, 650);
            ground[2].pos = new Vector2(950, 650);

            plat[1].position = new Vector2(1100, 650); plat[1].Initialize(300, vert);

            ground[3].pos = new Vector2(1150, 1000); ground[3].size = new Vector2(400, 300);
            wall[2].pos = new Vector2(1300, 10); wall[2].size = new Vector2(15, 970);
            //wall[]//wall to push back

            ground[4].pos = new Vector2(550, 650); ground[4].size = new Vector2(100, 50);
            ground[5].pos = new Vector2(100, 650); ground[5].size = new Vector2(450, 100);

            wall[4].pos = new Vector2(100, 320);//wall under cap

            ground[6].pos = new Vector2(100, 300); ground[6].size = new Vector2(100, 20);//cap on top of wall

            plat[2].position = new Vector2(250, 300); plat[2].Initialize(250, vert);
        }

        private void SetRoom9()
        {
            startBox.X = 1; startBox.Y = 950;
            endBox.X = 1350; endBox.Y = 1000;
            camWallX.Set(1400, vert);
            camWallY.Set(1250, hor);
            boundsBox.Set(1450, 1300);

            ground[0].pos = new Vector2(0, 1000); ground[0].size = new Vector2(300, 300);
            ground[1].pos = new Vector2(500, 1000);

            plat[0].position = new Vector2(650, 200); plat[0].Initialize(750, vert);

            ground[2].pos = new Vector2(800, 1000);
            ground[3].pos = new Vector2(950, 1050); ground[3].size = new Vector2(450, 300);

            wall[0].pos = new Vector2(1300, 680); wall[0].size = new Vector2(40, 300);

            ground[4].pos = new Vector2(800, 600); ground[4].size = new Vector2(100, 80);
            ground[5].pos = new Vector2(1000, 600); ground[5].size = new Vector2(400, 80);
            ground[6].pos = new Vector2(800, 200); ground[6].size = new Vector2(550, 80);

            wall[1].pos = new Vector2(880, 280); wall[1].size = new Vector2(40, 270);

            plat[1].position = new Vector2(300, 300); plat[1].Initialize(200, hor);

            ground[7].pos = new Vector2(0, 300); ground[7].size = new Vector2(400, 80);
        }

        private void SetRoom10()
        {
            startBox.X = 20; startBox.Y = 150;
            endBox.X = 1170; endBox.Y = 2150;
            camWallX.Set(1420, vert);
            camWallY.Set(2500, hor);
            boundsBox.Set(1470, 2550);

            wall[0].pos = new Vector2(-40,0); wall[0].size = new Vector2(40, 2000);
            wall[1].pos = new Vector2(1400, 0); wall[0].size = new Vector2(40, 2000);

            ground[0].pos = new Vector2(0, 200); ground[0].size = new Vector2(250, 50);
            ground[1].pos = new Vector2(230, 250);
            ground[2].pos = new Vector2(300, 350);
            ground[3].pos = new Vector2(200, 450);
            ground[4].pos = new Vector2(300, 550);
            ground[5].pos = new Vector2(200, 650);
            ground[6].pos = new Vector2(300, 750);
            ground[7].pos = new Vector2(450, 850);
            ground[8].pos = new Vector2(750, 1050);

            plat[0].position = new Vector2(900, 1050); plat[0].Initialize(250, hor);

            ground[9].pos = new Vector2(1250, 1050); ground[9].size = new Vector2(200, 100);

            ground[10].pos = new Vector2(550, 1050);
            ground[11].pos = new Vector2(400, 1150);

            ground[12].pos = new Vector2(100, 1150); ground[12].size = new Vector2(250, 30);

            ground[13].pos = new Vector2(0, 1250);
            ground[14].pos = new Vector2(100, 1350);
            ground[15].pos = new Vector2(250, 1400);
            ground[16].pos = new Vector2(400, 1450);
            //vine
            ground[17].pos = new Vector2(550, 1550);

            ground[18].pos = new Vector2(500, 2110); ground[18].size = new Vector2(150, 300);
            ground[19].pos = new Vector2(700, 2200);
            ground[20].pos = new Vector2(850, 2200); ground[20].size = new Vector2(400, 300);
        }

        public void Update()
        {
            for (int i = 0; i < 50; i++)
            {
                plat[i].Update();
                if (ground[i].fall)
                {
                    ground[i].Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            for (int i = 0; i < 50; i++)
            {
                ground[i].Draw(spriteBatch, color);

                plat[i].Draw(spriteBatch);
            }
            for (int i = 0; i < 10; i++)
            {
                wall[i].Draw(spriteBatch, color);
            }
            spriteBatch.Draw(startEnd, startBox, Color.AntiqueWhite);
            spriteBatch.Draw(startEnd, endBox, Color.AntiqueWhite);
        }
    }
}
