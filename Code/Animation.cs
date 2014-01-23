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

    public class Animation : Microsoft.Xna.Framework.GameComponent
    {
        public Animation(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public float timer = 0f;
        public float aSpeed;               //Animation length
        public int currentFrame = 1;  //First frame
        public int sheetW;           //Width of sprite sheet
        public int sheetH;          //Height of sprite sheet
        public int noOfSprites;  //Number of images in the sheet
        public float xPos;                    //xPos of animation
        public float yPos;                    //yPos  "   "
        public int singleSpriteWidth;          //Width of a single sprite
        public Texture2D spriteSheet;
        public bool alive = false;       //decides if it should be drawn or not


        public void SetAnimation(float speed, int totalFrames, int frameWidth)
        {
            aSpeed = speed;
            noOfSprites = totalFrames;
            singleSpriteWidth = frameWidth;
        }

        public void Place(Vector2 playerPos)
        {
            xPos = playerPos.X;
            yPos = playerPos.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {

            sheetH = spriteSheet.Height;
            sheetW = spriteSheet.Width;

            singleSpriteWidth = sheetW / noOfSprites; //Calculates the width of each image

            Rectangle sourceRect = new Rectangle(currentFrame * singleSpriteWidth, 0, singleSpriteWidth, sheetH); //Rectangle to draw it in
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //Advances timer


            if (timer > aSpeed)
            {
                //Show next frame
                currentFrame++;
                //Reset timer
                timer = 0f;
            }
            if (currentFrame >= noOfSprites)
            {
                currentFrame = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                currentFrame = 0;
            }
            spriteBatch.Draw(spriteSheet, new Vector2(xPos, yPos), sourceRect, color, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
        }
    }
}
