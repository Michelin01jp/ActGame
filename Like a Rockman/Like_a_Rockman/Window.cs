using System;

using OpenTK;
using OpenTK.Graphics;

namespace Like_a_Rockman
{
    /// <summary>
    /// 画面管理
    /// </summary>
    class MyWindow : GameWindow
    {
        GameClass Game;

        /// <summary>
        /// 画面のコンストラクタ
        /// </summary>
        public MyWindow(GameClass game)
            : base((int)(game.Setting.Width * game.Setting.Scale), (int)(game.Setting.Height * game.Setting.Scale), GraphicsMode.Default, Program.Title, GameWindowFlags.Default)
        {
            Game = game;
            this.VSync = VSyncMode.On;
            this.WindowBorder = WindowBorder.Fixed; // 画面の大きさを固定する

            // ウィンドウをフルスクリーンにするかどうか
            if (Game.Setting.FullScreen)
                this.WindowState = WindowState.Fullscreen;
            else
                this.WindowState = WindowState.Normal;

            // ウィンドウのクローズイベントを追記する
            this.Closed += (object sender, EventArgs e) =>
            {
                Program.End(); // プログラムの終了
            };

            return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Keyboard.KeyUp += Game.Input.KeyUp;
            Keyboard.KeyDown += Game.Input.KeyDown;
        }

        public void AddDrawEvent(EventHandler<FrameEventArgs> e)
        {
            this.RenderFrame += e;
        }
    }
}