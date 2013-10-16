using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Like_a_Rockman
{
    class GameDraw
    {
        MyWindow window;
        Game game;

        public GameDraw(MyWindow w,Game g)
        {
            window = w;
            window.AddDrawEvent(Refresh);
            game = g;
        }

        /// <summary>
        /// 画面の更新を行う
        /// </summary>
        public void Refresh(object sender, OpenTK.FrameEventArgs e)
        {
            GameWindow Window = (GameWindow)sender;

            Draw.Screen(Color.White); // 背景の塗りつぶし

            switch (game.FuncState)
            {
                case Game.INIT:
                    break;
                case Game.LOAD:
                    break;
                case Game.MAIN:

                    break;
            }

            Window.SwapBuffers();
        }
    }
}
