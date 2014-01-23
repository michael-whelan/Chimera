using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Chimera
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Viewport viewPort;
        int view, viewF = 1, viewP = -1;
        Menu menu;
        CollisionManager colManage;
        DisplayOrientation ori;
        public enum gameState { gamePlay, cutscene, menu, credits };
        CutScene scene;
        gameState state = gameState.menu;
        GroundManager gm;
        NPCManager npcM;
        CreditsControl credCtrl;
        Player player;
        Gravity gravity;
        Background backG;
        int timer;
        int roomNum = 1;

        int pauseTimer;
        bool pause = false;

        bool godMode = false;

        Texture2D marker0TX; Rectangle marker0RX = new Rectangle(656,250,10,10);//temp
        Texture2D marker0TY; Rectangle marker0RY = new Rectangle(327, 400, 10, 10);//temp
        Texture2D darkness;//used to make room 10 dark
        Rectangle darkRec; int darkTimer;
        bool music;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            camera = new Camera(this);
            player = new Player(this);
            backG = new Background(this);
            credCtrl = new CreditsControl(this);
            gravity = new Gravity(this);
            gm = new GroundManager(this);
            npcM = new NPCManager(this);
            colManage = new CollisionManager(this);
            menu = new Menu(this);
            scene = new CutScene(this);
            npcM.guide = new Guide(this);//must be here as it is needs to be called before initialize
            

            //set resolution
            ori = DisplayOrientation.LandscapeRight;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 666;
        }

        protected override void Initialize()
        {
            view = viewP;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            viewPort = GraphicsDevice.Viewport;

            LoadNPC();

            gm.gTex1 = this.Content.Load<Texture2D>("Ground/grass");
            gm.gTex2 = this.Content.Load<Texture2D>("Ground/grass2");
            gm.gTex3 = this.Content.Load<Texture2D>("Ground/grass3");
            gm.wallTex = this.Content.Load<Texture2D>("Ground/wall");
            gm.startEnd = this.Content.Load<Texture2D>("startEnd");
            darkness = this.Content.Load<Texture2D>("darkness");
            LoadBackGround();
            
            marker0TX = this.Content.Load<Texture2D>("green");//temp
            marker0TY = this.Content.Load<Texture2D>("green");
            LoadVirtButtons();
            LoadPlayer();
            LoadMenu();
            credCtrl.btnDwnImg = this.Content.Load<Texture2D>("creditDown");
            credCtrl.btnUpImg = this.Content.Load<Texture2D>("creditUp");
            credCtrl.btnDwnImgP = this.Content.Load<Texture2D>("creditDownP");
            credCtrl.btnUpImgP = this.Content.Load<Texture2D>("creditUpP");
            credCtrl.texture = this.Content.Load<Texture2D>("credits");
            LoadCutScenes();
            LoadGuide();
            scene.Initialize();
            player.Initialize();
           
            gm.Initialize();
            npcM.Initialize();
            npcM.guide.Initialize();
            backG.InitializeSounds();
            backG.Initialize();

            Reset();
            gm.Set(roomNum);
            npcM.Set(roomNum);
            backG.menu_.Play();
        }

        private void LoadBackGround()
        {
            backG.back1 = this.Content.Load<Texture2D>("BackGround/sky1");
            backG.back2 = this.Content.Load<Texture2D>("BackGround/sky2");
            backG.back3 = this.Content.Load<Texture2D>("BackGround/sky3");
            backG.back4 = this.Content.Load<Texture2D>("BackGround/sky1");
            backG.back5 = this.Content.Load<Texture2D>("BackGround/sky2");
            backG.back6 = this.Content.Load<Texture2D>("BackGround/sky3");
            backG.back7 = this.Content.Load<Texture2D>("BackGround/sky1");
            backG.back8 = this.Content.Load<Texture2D>("BackGround/sky2");
            backG.back9 = this.Content.Load<Texture2D>("BackGround/sky3");
            backG.back10 = this.Content.Load<Texture2D>("BackGround/cave");

               backG.menu = this.Content.Load<SoundEffect>("Music/menu");
               backG.gameplay = this.Content.Load<SoundEffect>("Music/gameplay");
               backG.credits = this.Content.Load<SoundEffect>("Music/credits");
        }

        private void LoadPlayer()
        {
            player.standR = this.Content.Load<Texture2D>("Player/standing");
            player.standL = this.Content.Load<Texture2D>("Player/standingL");
            player.snd = this.Content.Load<SoundEffect>("Audio/playerWalk");
            player.sndExhale = this.Content.Load<SoundEffect>("Audio/exhale");
            player.runL = this.Content.Load<Texture2D>("Player/runningL");
            player.runR = this.Content.Load<Texture2D>("Player/runningR");
            player.jump = this.Content.Load<Texture2D>("Player/jump");
            player.airR = this.Content.Load<Texture2D>("Player/air");
            player.airL = this.Content.Load<Texture2D>("Player/airL");
            player.slideL = this.Content.Load<Texture2D>("Player/slideL");
            player.slideR = this.Content.Load<Texture2D>("Player/slideR");
        }

        private void LoadVirtButtons()
        {

            player.btnLeft = this.Content.Load<Texture2D>("GUI/buttonL");
            player.btnRight = this.Content.Load<Texture2D>("GUI/buttonR");
            player.btnJump = this.Content.Load<Texture2D>("GUI/buttonJ");
            player.btnSlide = this.Content.Load<Texture2D>("GUI/buttonS");
            player.btnAction = this.Content.Load<Texture2D>("GUI/buttonA");

            player.btnLeftP = this.Content.Load<Texture2D>("GUI/buttonLP");
            player.btnRightP = this.Content.Load<Texture2D>("GUI/buttonRP");
            player.btnJumpP = this.Content.Load<Texture2D>("GUI/buttonJP");
            player.btnSlideP = this.Content.Load<Texture2D>("GUI/buttonSP");
            player.btnActionP = this.Content.Load<Texture2D>("GUI/buttonAP"); 
        }

        private void LoadNPC()
        {
            npcM.treeTex = this.Content.Load<Texture2D>("NPC/tree");
            npcM.vineTexH = this.Content.Load<Texture2D>("NPC/vine");
            npcM.vineTexV = this.Content.Load<Texture2D>("NPC/vineVert");
            npcM.treeSnd = this.Content.Load<SoundEffect>("Audio/treeSound");
            npcM.vineSnd = this.Content.Load<SoundEffect>("Audio/vineSound");

            npcM.walkL = this.Content.Load<Texture2D>("NPC/walkleft");
            npcM.walkR = this.Content.Load<Texture2D>("NPC/walkright");
            npcM.bugSnd = this.Content.Load<SoundEffect>("Audio/bug");

            npcM.plantClose = this.Content.Load<Texture2D>("NPC/plantClose");
            npcM.plantSnd = this.Content.Load<SoundEffect>("Audio/Plant");
            npcM.plantOpen = this.Content.Load<Texture2D>("NPC/plantOpen");

            npcM.hopper1 = this.Content.Load<Texture2D>("NPC/hopper1");
            npcM.hopper2 = this.Content.Load<Texture2D>("NPC/hopper2");
            npcM.hopperSnd = this.Content.Load<SoundEffect>("Audio/spring");

        }

        private void LoadCutScenes()
        {
            scene.blackSound = this.Content.Load<Texture2D>("CutScene/blank");
            scene.riftPresents = this.Content.Load<Texture2D>("CutScene/riftPresents");
            scene.name = this.Content.Load<Texture2D>("CutScene/name");
            scene.openingSound = this.Content.Load<SoundEffect>("Audio/cry");
            scene.safe = this.Content.Load<SoundEffect>("Audio/youAreSafe");
            scene.upcoming1 = this.Content.Load<Texture2D>("CutScene/upcoming1");
            scene.upcoming2 = this.Content.Load<Texture2D>("CutScene/upcoming2");
            scene.comingSoon = this.Content.Load<Texture2D>("CutScene/comingSoon");
            scene.tapScreen = this.Content.Load<Texture2D>("CutScene/tapScreen");
            scene.closingSound = this.Content.Load<SoundEffect>("Audio/finalLine");
        }

        private void LoadGuide()
        {
            npcM.guide.motherTex = this.Content.Load<Texture2D>("NPC/motherghost");
            npcM.guide.animation.spriteSheet = this.Content.Load<Texture2D>("NPC/ghostVanish");
            npcM.guide.room1 = this.Content.Load<SoundEffect>("Audio/room1Comp"); 
            npcM.guide.room2 = this.Content.Load<SoundEffect>("Audio/room2Comp");
            npcM.guide.room3 = this.Content.Load<SoundEffect>("Audio/room3Comp");
            npcM.guide.room4 = this.Content.Load<SoundEffect>("Audio/room4Comp");
            npcM.guide.room5 = this.Content.Load<SoundEffect>("Audio/room5Comp");
            npcM.guide.room6 = this.Content.Load<SoundEffect>("Audio/room6Comp");
            npcM.guide.room7 = this.Content.Load<SoundEffect>("Audio/room7Comp");
            npcM.guide.room8 = this.Content.Load<SoundEffect>("Audio/room8Comp");
            npcM.guide.room9 = this.Content.Load<SoundEffect>("Audio/room9Comp");
            npcM.guide.room10 = this.Content.Load<SoundEffect>("Audio/room10Comp");

            //extras
            npcM.guide.gotYou = this.Content.Load<SoundEffect>("Audio/gotYou");
            npcM.guide.here = this.Content.Load<SoundEffect>("Audio/here");
            npcM.guide.worry = this.Content.Load<SoundEffect>("Audio/worry"); 
        }

        private void LoadMenu()
        {
            menu.playOn = this.Content.Load<Texture2D>("Menu/Play");
            menu.quitOn = this.Content.Load<Texture2D>("Menu/Quit");
            menu.resumeOn = this.Content.Load<Texture2D>("Menu/resume");
            menu.pauseBack = this.Content.Load<Texture2D>("Menu/pause");
            menu.credits = this.Content.Load<Texture2D>("Menu/Credits");
            menu.menuBack = this.Content.Load<Texture2D>("Menu/menuBack");
            menu.musicOn = this.Content.Load<Texture2D>("Menu/soundOn");
            menu.musicOff = this.Content.Load<Texture2D>("Menu/soundOff");
        }

        protected override void UnloadContent()
        {
        }

        public void Pause()
        {
            if (pauseTimer >= 10)
            {
                if (!pause)
                {
                    pause = true;
                }
                else if (pause)
                {
                    pause = false;
                }
                pauseTimer = 0;
            }
        }


        private void GameStateManager()
        {
            if (state == gameState.menu)
            {
                if (music)
                {
                    backG.menu_.Play();
                }
                pauseTimer++;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                { this.Exit(); }
                foreach (TouchLocation tl in TouchPanel.GetState())
                {
                    if (menu.playRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        backG.menu_.Stop();
                        state = gameState.cutscene;
                        Reset();
                        roomNum = 1;
                        scene.frameNum = 1;
                        gm.Set(roomNum);
                        npcM.Set(roomNum);
                        player.levelStart.X = gm.startBox.X;
                        player.levelStart.Y = gm.startBox.Y;
                        player.position.X = gm.startBox.X;
                        player.position.Y = gm.startBox.Y;
                    }
                    else if (menu.quitRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        if (pauseTimer >= 10)
                        {
                            this.Exit();
                            pauseTimer = 0;
                        }
                    }
                    else if (menu.creditRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        backG.menu_.Stop();
                        if (music)
                        {
                            backG.credits_.Play();
                        } state = gameState.credits;
                        credCtrl.Set((int)this.viewPort.Width);
                    }
                }

                pause = false;
            }

            if (state == gameState.cutscene)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                { state = gameState.menu; scene.openingSound_.Stop(); scene.safe_.Stop(); }

                if (scene.endScene == true)
                {
                    if (roomNum == 1)
                    {
                        state = gameState.gamePlay;
                        scene.endScene = false;
                    }
                    else
                    {
                        backG.gameplay_.Volume = 0.5f;
                        state = gameState.credits;
                        credCtrl.Set((int)this.viewPort.Width);
                        scene.endScene = false;
                    }
                }  
            }

            if (state == gameState.credits)
            {
                if (music)
                {
                    backG.credits_.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        backG.credits_.Stop();
                        
                        state = gameState.menu;
                    }
            }
            if (state == gameState.gamePlay)
            {
                if (music)
                {
                    backG.gameplay_.Play();
                }
                backG.gameplay_.Volume = 1f ;
                if (pause)
                {
                    foreach (TouchLocation tl in TouchPanel.GetState())
                    {
                        pauseTimer = 0;
                        if (menu.playRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                        {
                            pause = false;
                        }
                        if (menu.quitRec.Contains((int)tl.Position.X, (int)tl.Position.Y))
                        {
                            backG.gameplay_.Stop();
                            state = gameState.menu;
                        }
                    }
                }
                if (roomNum == 10)
                {
                    if (player.rec.Intersects(gm.endBox))
                    {
                        state = gameState.cutscene;
                    }
                }
            }
        }

        private void LevelManage()
        {
            if (player.rec.Intersects(gm.endBox))
            {
                roomNum++;
                Reset();
               
                gm.Set(roomNum);
                npcM.Set(roomNum);
                player.levelStart.X = gm.startBox.X;
                player.levelStart.Y = gm.startBox.Y;
                player.position.X = gm.startBox.X;
                player.position.Y = gm.startBox.Y;
                
            }
            if (roomNum == 10)
            {
                if (colManage.drkT)
                {
                    darkTimer++;
                }
                if (darkTimer < 300)
                {
                    darkRec = new Rectangle(0, 0, 1000, 1000);
                }
                else
                {
                    darkRec = new Rectangle(-1000, -1000, 0, 0);
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            timer++;
            pauseTimer++;//for pause
            KeyboardState ks = Keyboard.GetState();

            GameStateManager();

            if (state == gameState.gamePlay)
            {
                    
                    // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                { Pause(); }
                                
                if (!pause)
                {
                    backG.Update(roomNum,player);
                    marker0RX.Y = player.rec.Y;//test
                    marker0RX.X = player.rec.X + 480;//test
                    marker0RY.Y = player.rec.Y + 200;
                    marker0RY.X = player.rec.X;

                    npcM.Update(roomNum,player);

                    gm.Update();
                    LevelManage();

                    Collision();

                    if (view == viewF)
                    {
                        // camera.FreeMatrix();
                        camera.Update(ks);
                    }
                    else
                    {
                        player.Update(viewPort);
                        camera.FollowPlayer(this.GraphicsDevice.Viewport.Height, this.GraphicsDevice.Viewport.Width, player.position.X, player.position.Y, player.direction);
                    }


                    if (ks.IsKeyDown(Keys.LeftShift))
                    {
                        if (timer > 10)
                        {
                            view *= -1;
                            timer = 0;
                        }
                    }
                }
                else if (pause)
                {
                    menu.playRec = new Rectangle(viewPort.Width - 200, viewPort.Height - 320, 100, 50);
                    menu.quitRec = new Rectangle(viewPort.Width - 200, viewPort.Height - 260, 100, 50);
                    menu.Update(pause);
                }
            }
            else if (state == gameState.menu)
            {
                music = menu.music;
                if (music == false)
                {
                    backG.menu_.Stop();
                }
                camera.pos = Vector2.Zero;
                menu.playRec = new Rectangle(100, 100, 100, 50);
                menu.quitRec = new Rectangle(100, 180, 100, 50);
                menu.creditRec = new Rectangle(100, 260, 100, 50);
                menu.Update(pause);
              //  backG.Menu();//uncomment
            }

            else if (state == gameState.cutscene)
            {
                camera.pos = Vector2.Zero;
                scene.Update(roomNum, ks);
                // if (roomNum == 1)
                // { backG.Cut1(); }//uncomment
            }
            else if (state == gameState.credits)
            {
                credCtrl.Update();
                camera.pos = Vector2.Zero;
                  //backG.Credits();//uncomment
            }


            base.Update(gameTime);
        }

        public void Collision()
        {
            colManage.SoundMaker(player, npcM.bug,npcM.plant,npcM.hopper,npcM.tree);//sound
            if (!godMode)
            {
                colManage.Update(player, npcM.bug, npcM.plant, npcM.hopper,backG);
                colManage.CutSceneCol(npcM.guide.timer, player, npcM.guide, gm.startBox, npcM.guide.freezePlayer,roomNum);
            }
            colManage.CamCol(marker0RX, marker0RY, gm.camWallX, gm.camWallY, camera);//passes the two markers and the two walls they hit
            colManage.OutOfBounds(gm.boundsBox, player);
            colManage.ActivateCol(player, npcM.tree);


            for (int i = 0; i < 50; i++)
            {
                if (gm.ground[i].breakable)
                {
                    if (player.feetRec.Intersects(gm.ground[i].rec))
                    {

                        gm.ground[i].fall = true;
                    }
                }

                if (player.rec.Intersects(gm.ground[i].rec) && player.feetRec.Intersects(gm.ground[i].rec))
                {
                    player.Collision();
                }

                else
                {
                    player.EndCollision();
                }
            }

            gravity.Update(player, gm.ground, gm.plat, npcM.tree, npcM.plant);

            for (int i = 0; i < 10; i++)
            {
                if (player.rec.Intersects(gm.wall[i].rec))
                {
                    player.slide = false;
                    if (player.direction == 2)
                    {
                        player.position.X += 5;
                    }
                    else if (player.direction == -2)
                    {
                        player.position.X -= 5;
                    }
                }
            }
        }

        public void Reset()
        {
            backG.Initialize();
            gm.Reset();
            npcM.Reset();
            player.position = new Vector2(-1000, -1000);
            npcM.guide.SetColRec(roomNum);
            npcM.guide.SetPositions(-1000,-1000);            
            gravity.yVel = 0;
            player.slide = false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //sets current viewport to playerCam
            GraphicsDevice.Viewport = viewPort;
            Color color = Color.AntiqueWhite;
            // rec = new Rectangle(0,0,1000,1000);


            if (state == gameState.gamePlay)
            {
                spriteBatch.Begin();
                backG.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin(default(SpriteSortMode), null, null, null, null, null, camera.cameraMatrix);
               // gm.boundsBox.Draw(spriteBatch);
                // spriteBatch.Draw(back,rec,Color.AntiqueWhite);
                npcM.DrawVines(spriteBatch);
                gm.Draw(spriteBatch, color);
                npcM.Draw(spriteBatch, color, gameTime);
                player.Draw(spriteBatch, gameTime,godMode);
               
                //spriteBatch.Draw(marker0T, marker0R, Color.AntiqueWhite);//temp
                spriteBatch.End();
                spriteBatch.Begin();//spritebatch for GUI
                if (pause)
                {
                    menu.Draw(spriteBatch, player);
                }
                if (roomNum == 10)
                {
                    
                    spriteBatch.Draw(darkness, darkRec, Color.AntiqueWhite);
                }
                player.DrawButtons(spriteBatch);

                spriteBatch.End();
            }//gameplay
            else if (state == gameState.menu)
            {
                spriteBatch.Begin();
                menu.Draw(spriteBatch, player);
                spriteBatch.End();
            }
            else if (state == gameState.cutscene)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                scene.Draw(spriteBatch, this.viewPort.Width, this.viewPort.Height);
                spriteBatch.End();
            }
            else if (state == gameState.credits)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                credCtrl.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
