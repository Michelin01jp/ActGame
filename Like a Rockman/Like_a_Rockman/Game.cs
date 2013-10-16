using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace Like_a_Rockman
{
    class Game
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
        MyWindow window;

        public Game()
        {
            StepTime = 1000 / Framerate;
            Frame = 0;
        }

        public void GameMain(MyWindow w)
        {
            window = w;
            Thread Main = new Thread(GameLoop);
            Main.Start();
            window.Run();

            return;
        }

        public void GameLoop()
        {
            GameDraw d = new GameDraw(window, this);

            while(Program.Cont)
            {
                Refresh();
                Wait();
                Console.WriteLine(Frame);
            }

            window.Close();
        }

        public void Refresh()
        {
            switch (FuncState)
            {
                case INIT:
                    FuncState = LOAD;
                    break;
                case LOAD:
                    FuncState = MAIN;
                    break;
                case MAIN:
                    break;
            }

            return;
        }

        public void Wait()
        {
            Frame++;
            return;
        }
    }
}
