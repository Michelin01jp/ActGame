using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    /// <summary>
    /// FPS計算クラス
    /// </summary>
    class FramePerSecound
    {
        /// <summary>
        /// 算出用のフレーム
        /// </summary>
        private int PerFrame;
        /// <summary>
        /// 時間
        /// </summary>
        private int Time;
        /// <summary>
        /// フレームパーセカンド
        /// </summary>
        public float FPS;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="framerate">FPS理想値を入れる</param>
        /// <param name="refresh">FPSの更新頻度(0～1000)</param>
        public FramePerSecound()
        {
            Time = System.Environment.TickCount;
        }

        public void FPSCalc()
        {
            int NowTime = Environment.TickCount;

            if (NowTime - Time >= 1000)
            {
                Time = Environment.TickCount;
                FPS = PerFrame;
                PerFrame = 0;
            }

            Console.WriteLine("FPS:{0}",FPS);

            PerFrame++;

            return;
        }
    }
}