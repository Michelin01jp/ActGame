using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    class FramePerSecond
    {
        /// <summary>
        /// 経過時間
        /// </summary>
        int Time1;
        /// <summary>
        /// 計算周期
        /// </summary>
        int Time2;
        /// <summary>
        /// 一秒間の更新回数
        /// </summary>
        int Step;
        /// <summary>
        /// 計算結果
        /// </summary>
        float FPS;
        /// <summary>
        /// 最後に計算してからのフレーム数(SpendFrame)
        /// </summary>
        int SFrame;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="step">FPSの理想値</param>
        /// <param name="RefreshTime">FPSの更新時間</param>
        public FramePerSecond(int step, int RefreshTime)
        {
            Step = step;
            FPS = Step;
            Time2 = RefreshTime;
            Time1 = 0;

            // 定義域
            if (Time2 < 1000 / step)
                Time2 = 1000 / step;
            if (Time2 > 1000)
                Time2 = 1000;
        }

        /// <summary>
        /// FPSの計算を行う
        /// </summary>
        public void Frame()
        {
        }
    }
}
