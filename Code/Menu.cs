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
using Microsoft.Xna.Framework.Input.Touch;


namespace Chimera
{
    public class Menu : Microsoft.Xna.Framework.GameComponent
    {
        public Menu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        public Texture2D playOn, quitOn, resumeOn, pauseBack,credits,menuBack;
        public Rectangle playRec, quitRec,creditRec, pauseRec,menuRec;//option1,option2,pause menu back;
        int highLight = 1;
        const int play = 1, quit = 2;
        bool _pause = false;

        public bool music = false;
        int musicTimer;
        public Texture2D musicBtn,musicOn,musicOff; Rectangle musicRec = new Rectangle(600,40,40,40);

        public int HighLight
        {
            get { return highLight; }
            set { highLight = value; }
        }

        public Rectangle PlayRec
        {
            get { return playRec; }
            set { playRec = value; }
        }

        public void Update(bool pause)
        {
            KeyboardState ks = Keyboard.GetState();
            _pause = pause;
            musicTimer++;
            foreach (TouchLocation tl in TouchPanel.GetState())
            {
                if (musicRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                {
                    if (musicTimer > 15)
                    {
                        if (music == true)
                        {
                            music = false;
                        }
                        else if (music == false)
                        {
                            music = true;
                        }
                        musicTimer = 0;
                    }
                }
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                highLight = 1;
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                highLight = 2;
            }
        }

        public void GameOver(SpriteBatch spriteBatch)
        {
            highLight = 2;
            spriteBatch.Draw(quitOn, quitRec, Color.AntiqueWhite);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            pauseRec = new Rectangle(playRec.X - 10, playRec.Y - 10, 140, 200);
            menuRec = new Rectangle(0, 0, 666, 400);
            if (music == true)
            { musicBtn = musicOn; }
            else { musicBtn = musicOff; }

            if (!_pause)
            {
                spriteBatch.Draw(menuBack,menuRec,Color.AntiqueWhite);
                spriteBatch.Draw(playOn, playRec, Color.AntiqueWhite);
                spriteBatch.Draw(quitOn, quitRec, Color.AntiqueWhite);
                spriteBatch.Draw(credits,creditRec,Color.AntiqueWhite);
                spriteBatch.Draw(musicBtn,musicRec,Color.AntiqueWhite);
            }
            else
            {
                spriteBatch.Draw(pauseBack, pauseRec, Color.AntiqueWhite);
                spriteBatch.Draw(resumeOn, playRec, Color.AntiqueWhite);
                spriteBatch.Draw(quitOn, quitRec, Color.AntiqueWhite);
            }
        }
    }
}
