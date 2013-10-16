using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

using OpenTK;

namespace Like_a_Rockman
{
    /// <summary>
    /// ゲームの制御をするクラス
    /// </summary>
    class GameClass
    {
        public int FuncState = 0;
        public int Framerate = 60;
        public int StepTime;
        public int Frame;
        public const int INIT = 0;
        public const int LOAD = 1;
        public const int STARTMENU = 2;
        public const int GAMELOAD = 49;
        public const int MAIN = 50;
        private MyWindow Window;
        private Camera Camera;
        private Player player;
        private Input input;
        private Setting setting;
        private Config config;
        private FramePerSecound FPS;

        public GameClass()
        {
            StepTime = 1000 / Framerate;
            Frame = 0;
            Setting = Setting.Load();
            Config = Config.Load();
            Input = new Input(Config);
            FPS = new FramePerSecound();
        }

        public void GameMain(MyWindow window)
        {
            Window = window;
            Init();
            Thread Main = new Thread(GameLoop);
            Main.Start();
            GameDraw d = new GameDraw(Window, this);
            Window.Run();

            return;
        }

        public void GameLoop()
        {
            while(Program.Continue)
            {
                Refresh();
                Wait();
            }

            Window.Close();
        }

        public void Refresh()
        {
            switch (FuncState)
            {
                case INIT:
                    FuncState = LOAD;
                    break;
                case LOAD:
                    MapData.Load();
                    FuncState = MAIN;
                    break;
                case MAIN:
                    CharacterContoroller.Step(StepTime);
                    break;
            }

            return;
        }

        public void Init()
        {
            Camera = new Camera();
            MapObject.Init(Window, Camera);
            Player = new Player(new Vector2(0, 0), Input);
            Player.Init(Window, Camera);
        }

        public Player Player
        {
            get { return player; }
            set { player = value;}
        }

        public Input Input
        {
            get { return input; }
            set { input = value; }
        }
        public Setting Setting
        {
            get { return setting; }
            set { setting = value; }
        }
        public Config Config
        {
            get { return config; }
            set { config = value; }
        }

        public void Wait()
        {
            Thread.Sleep(StepTime);
            FPS.FPSCalc();
            Frame++;
            return;
        }
    }
}
