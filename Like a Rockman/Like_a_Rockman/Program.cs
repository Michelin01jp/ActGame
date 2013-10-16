using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Like_a_Rockman
{
    /// <summary>
    /// プログラムのエントリポイント
    /// </summary>
    class Program
    {
        /// <summary>
        /// タイトルバーに表示される文字
        /// </summary>
        public const string Title = "Like a Rockman.";
        /// <summary>
        /// ゲームが終わるときFalseになる
        /// </summary>
        public static bool Continue = true;

        public static void Main(string[] args)
        {
            var G = new GameClass();
            var MW = new MyWindow(G);
            G.GameMain(MW);
        }

        public static void End()
        {
            Continue = false;

            return;
        }
    }
}
