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

    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Camera(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Vector2 pos;
        public Matrix cameraMatrix;
        int camSpeed = 3;
        int left = 2, right = -2;
        int xPlus, yPlus = 0, tempX;//used as the extras to set screen relative to player
        float matX;
        float matY;
        float maxX = 1000, maxY=1100;//max x and y the cam can be placed at

        public bool stopX = false;//temp
        public bool stopY = false;

        public void Initialize(float pX,float pY)
        {
            matX = -(pX + 100);
            matY = -(pY + 100);
        }

        public void Update(KeyboardState ks)
        {//opposite to move world away from cam
            if (ks.IsKeyDown(Keys.Up))
            {
                pos.Y += camSpeed;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                pos.Y -= camSpeed;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                pos.X += camSpeed;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                pos.X -= camSpeed;
            }
        }

        public void ChangeMax(int room)//changes the max pos of the camera depending on room
        {
            if (room == 1)
            {
                maxX = 1000;
                maxY = 1100;
            }
            else if (room == 2)
            { }
            else if (room == 3)
            { }
            else if (room == 4)
            { }
            else if (room == 5)
            { }
            else if (room == 6)
            { }
            else if (room == 7)
            { }
            else if (room == 8)
            { }
            else if (room == 9)
            { }
            else if (room == 10)
            { }
        }

        public void FollowPlayer(float h, float w, float pX, float pY, int direction)
        {
          
            if (direction == right)
            {
                tempX = 160;
            }
            else
            {
                tempX = -160;
            }

            if (xPlus < tempX)
            {
                xPlus += 40;
            }
            else if (xPlus > tempX)
            {
                xPlus -= 40;
            }

            //X
            if ((pX < 500 && direction == left) || (pX < 400 && direction == right))//180 is smoother but causes a slight jump out of world
            {
                matX = 0;//if furthest left screen is set
            }
            else
            {
                if(!stopX)
                matX = -(pX + xPlus) + (w / 2);//sets camera positions on player
            }

            //Y
            if (pY < 200)
            {
                matY = 0;//if furthest up screen is set
            }
            else{
                if(!stopY)
                matY = -(pY + yPlus) + (h / 2);//as variables for change
            }
            cameraMatrix = Matrix.CreateTranslation(new Vector3(matX, matY, 0));
        }

        public void FreeMatrix()//float h,float w)
        {
            float matX = (pos.X);// +(float)(w / 2);//sets camera positions on player
            float matY = (pos.Y);// +(h);//as variables for change
            //stops the camera if the player is too close to the edge
            //gives camera 1-D movement
            /* if (pos.X <= xMinCam)
             {
                 matX = 0;
             }
             if (pos.Y <= yMinCam)
             {
                 matY = 0;
             }
             if (pos.X >= xMaxCam)
             {
                 matX = -1200;
             }
             if (pos.Y >= yMaxCam)
             {
                 matY = -1520;
             }*/
            //if the player is away from the edge the matrix is complete and the camera has 2-D movement
            cameraMatrix = Matrix.CreateTranslation(new Vector3(matX, matY, 0));
        }

    }
}
