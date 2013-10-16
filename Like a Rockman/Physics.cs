using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class Physics
    {
        /// <summary>
        /// 物理の影響下にある物体に必ず入れる
        /// </summary>
        public struct PhysicalObject
        {
            /// <summary>
            /// 位置
            /// </summary>
            public Vector2 Position;
            /// <summary>
            /// 大きさ
            /// </summary>
            public Vector2 Size;
            /// <summary>
            /// 運動
            /// </summary>
            public Vector2 Velocity;
            /// <summary>
            /// 加速度
            /// </summary>
            public Vector2 Acceleration;
            /// <summary>
            /// 方向
            /// </summary>
            public GamingMath.Direction Direction;
        }

        /// <summary>
        /// 重力加速度
        /// </summary>
        public static float GravityAcceleration = 10.0f;

        /// <summary>
        /// 一フレーム分の運動を計算する
        /// </summary>
        /// <param name="Time">時間</param>
        /// <param name="AABB">衝突</param>
        /// <param name="physicalObject">構造体</param>
        public static void Step(int Time, ref PhysicalObject physicalObject, ref Collision.AABB AABB)
        {
            physicalObject.Acceleration.Y +=  GravityAcceleration / 1000 * Time;

            // 落下時に一定以上加速させないためのキャパシティ
            if (physicalObject.Velocity.Y * (1000 / Time) > 480)
                physicalObject.Acceleration.Y = 0; // 落下の加速を0にする

            physicalObject.Velocity += physicalObject.Acceleration;

            // 現在のグリッド上の位置でのあたり判定を行う
            Collision.Map(ref AABB, ref physicalObject.Velocity);

            Console.WriteLine("{0}, {1}", physicalObject.Position.X, physicalObject.Position.Y);
            Console.WriteLine("{0}, {1}", physicalObject.Velocity.X, physicalObject.Velocity.Y);

            physicalObject.Position += physicalObject.Velocity;

            if (physicalObject.Velocity != new Vector2(0, 0))
                AABB.Move(physicalObject.Position);

            physicalObject.Acceleration = new Vector2();

            return;
        }

        /// <summary>
        /// 一フレーム分の運動を計算する
        /// </summary>
        /// <param name="Time">時間</param>
        /// <param name="physicalObject">構造体</param>
        public static void Step(int Time, ref PhysicalObject physicalObject)
        {
            physicalObject.Acceleration.Y +=  GravityAcceleration / 1000 * Time;

            // 落下時に一定以上加速させないためのキャパシティ
            if (physicalObject.Velocity.Y * (1000 / Time) > 350)
                physicalObject.Acceleration.Y = 0;

            physicalObject.Velocity += physicalObject.Acceleration;
            physicalObject.Position += physicalObject.Velocity;

            physicalObject.Acceleration = new Vector2();

            return;
        }
    }
}
