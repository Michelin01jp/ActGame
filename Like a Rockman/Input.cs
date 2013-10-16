using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Like_a_Rockman
{
    /// <summary>
    /// 入力クラス
    /// </summary>
    class Input
    {
        Config Config;

        /// <summary>
        /// 操作内容
        /// </summary>
        public enum Control
        {
            None = 0,
            Up = 1,
            Right = 2,
            Down = 4,
            Left = 8,
            Jump = 16,
            Squat = 32,
            Attack1 = 64,
            Attack2 = 128
        }

        public Input(Config config)
        {
            Config = config;
        }

        public Control control = new Control();

        internal void KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            int key = (int)e.Key;

            if (key == Config.Up)
                control &= ~Control.Up;
            if (key == Config.Down)
                control &= ~Control.Down;
            if (key == Config.Left)
                control &= ~Control.Left;
            if (key == Config.Right)
                control &= ~Control.Right;
            if (key == Config.Squat)
                control &= ~Control.Squat;
            if (key == Config.Jump)
                control &= ~Control.Jump;
            if (key == Config.Attack1)
                control &= ~Control.Attack1;
            if (key == Config.Attack2)
                control &= ~Control.Attack2;
        }

        internal void KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            int key = (int)e.Key;

            if (key == Config.Up)
                control |= Control.Up;
            if (key == Config.Down)
                control |= Control.Down;
            if (key == Config.Left)
                control |= Control.Left;
            if (key == Config.Right)
                control |= Control.Right;
            if (key == Config.Squat)
                control |= Control.Squat;
            if (key == Config.Jump)
                control |= Control.Jump;
            if (key == Config.Attack1)
                control |= Control.Attack1;
            if (key == Config.Attack2)
                control |= Control.Attack2;
        }

        /// <summary>
        /// キーの状態を取得する
        /// </summary>
        /// <param name="key">取得するキー</param>
        /// <returns>キーが押されているか</returns>
        public bool GetKeyState(Control key)
        {
            return 0 != (control & key);
        }
    }
}
