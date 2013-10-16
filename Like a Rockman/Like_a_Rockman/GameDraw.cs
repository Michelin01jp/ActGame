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
    /// <summary>
    /// ゲームの描画をするクラス
    /// </summary>
    class GameDraw
    {
        MyWindow Window;
        GameClass Game;

        public GameDraw(MyWindow window, GameClass game)
        {
            Window = window;
            Game = game;

            Window.AddDrawEvent(Refresh);
        }

        /// <summary>
        /// 画面の更新を行う
        /// </summary>
        public void Refresh(object sender, OpenTK.FrameEventArgs e)
        {
            GameWindow window = (GameWindow)sender;

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            
            Draw.Screen(Color.Black); // 背景の塗りつぶし

            MapData.Draw();
            CharacterContoroller.Draw();
 
            switch (Game.FuncState)
            {
                case GameClass.INIT:
                    break;
                case GameClass.LOAD:
                    break;
                case GameClass.MAIN:

                    break;
            }

            window.SwapBuffers();
        }
    }
}
