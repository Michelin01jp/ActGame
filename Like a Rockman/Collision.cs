using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class Collision
    {
        /// <summary>
        /// キャラクター
        /// 地面
        /// 動く床のこと
        /// オブジェクト(弾など)
        /// </summary>
        public enum Type
        {
            Character,
            Ground,
            Floor,
            Object
        }

        public struct AABB
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
            /// 反発係数
            /// </summary>
            public float Coefficient;
            /// <summary>
            /// あたり判定のタイプ
            /// </summary>
            public Type Type;

            public AABB(Vector2 position, Vector2 size, float coeff, Type type)
            {
                Position = position;
                Size = size;
                Coefficient = coeff;
                Type = type;
            }

            /// <summary>
            /// あたり判定の移動が必要になったとき
            /// </summary>
            /// <param name="position">新しい位置</param>
            public void Move(Vector2 position)
            {
                if (Type != Collision.Type.Ground)
                {
                    Position = position;
                }
            }
        }

        public static void Map(ref AABB aabb, ref Vector2 velocity)
        {
            Vector2 Moved_LR = aabb.Position + new Vector2(velocity.X, 0);
            Vector2 Moved_TB = aabb.Position + new Vector2(0, velocity.Y);

            bool Ychanged = false; // Y方向の変更を行ったか
            bool Xchanged = false; // X方向の変更を行ったか

            // 移動前の上下左右の位置
            int GridLeft = (int)(aabb.Position.X / MapData.GridWidth);
            int GridTop = (int)(aabb.Position.Y / MapData.GridHeight);
            int GridRight = (int)((aabb.Position.X + aabb.Size.X) / MapData.GridWidth);
            int GridBottom = (int)((aabb.Position.Y + aabb.Size.Y) / MapData.GridHeight);
            // 移動後の上下左右の位置
            int MovedGridLeft = (int)(Moved_LR.X / MapData.GridWidth);
            int MovedGridTop = (int)(Moved_TB.Y / MapData.GridHeight);
            int MovedGridRight = (int)((Moved_LR.X + aabb.Size.X) / MapData.GridWidth);
            int MovedGridBottom = (int)((Moved_TB.Y + aabb.Size.Y) / MapData.GridHeight);

            #region 定義域を守っているか
            // 上の判定
            if (Moved_TB.Y < 0)
            {
                aabb.Position.Y = 0;
                MovedGridTop = 0;
                velocity.Y = 0;
                Ychanged = true;
            }
            // 下の判定
            if (MovedGridBottom >= MapData.Map[0].Count)
            {
                aabb.Position.Y = (MapData.Map[0].Count - 1) * MapData.GridHeight - (aabb.Size.Y - MapData.GridHeight);
                MovedGridBottom = MapData.Map[0].Count - 1;
                velocity.Y = 0;
                Ychanged = true;
            }
            // 左の判定
            if (Moved_LR.X < 0)
            {
                aabb.Position.X = 0;
                MovedGridLeft = 0;
                velocity.X = 0;
                Xchanged = true;
            }
            // 右の判定
            if (MovedGridRight >= MapData.Map.Count)
            {
                aabb.Position.X = (MapData.Map.Count - 1) * MapData.GridWidth - (aabb.Size.X - MapData.GridWidth);
                MovedGridRight = MapData.Map.Count - 1;
                velocity.X = 0;
                Xchanged = true;
            }
            #endregion

            if (Xchanged && Ychanged)
                return;

            if (Moved_LR.X % MapData.GridWidth == 0 && !Xchanged)
                MovedGridLeft++;
            if (Moved_TB.Y % MapData.GridHeight == 0 && !Ychanged)
                MovedGridTop++;
            if ((Moved_LR.X + aabb.Size.X) % MapData.GridWidth == 0 && !Xchanged)
                MovedGridRight--;
            if ((Moved_TB.Y + aabb.Size.Y) % MapData.GridHeight == 0 && !Ychanged)
                MovedGridBottom--;

            Console.WriteLine("{0}, {1}, {2}, {3}", MovedGridLeft, MovedGridBottom, MovedGridRight, MovedGridTop);

            GamingMath.Direction direction = new GamingMath.Direction();

            // 左上
            if (MapData.IsSolid(MovedGridLeft, MovedGridTop))
            {
                if (MapData.IsSolid(MovedGridLeft, GridTop))
                    direction |= GamingMath.Direction.Left;
                if (MapData.IsSolid(GridLeft, MovedGridTop))
                    direction |= GamingMath.Direction.Top;
            }
            // 右上
            if (MapData.IsSolid(MovedGridRight, MovedGridTop))
            {
                if (MapData.IsSolid(MovedGridRight, GridTop))
                    direction |= GamingMath.Direction.Right;
                if (MapData.IsSolid(GridRight, MovedGridTop))
                    direction |= GamingMath.Direction.Top;
            }
            // 左下
            if (MapData.IsSolid(MovedGridLeft, MovedGridBottom))
            {
                if (MapData.IsSolid(MovedGridLeft, GridBottom))
                    direction |= GamingMath.Direction.Left;
                if (MapData.IsSolid(GridLeft, MovedGridBottom))
                    direction |= GamingMath.Direction.Bottom;
            }
            // 右下
            if (MapData.IsSolid(MovedGridRight, MovedGridBottom))
            {
                if (MapData.IsSolid(MovedGridRight, GridBottom))
                    direction |= GamingMath.Direction.Right;
                if (MapData.IsSolid(GridRight, MovedGridBottom))
                    direction |= GamingMath.Direction.Bottom;
            }

            if (0 != (direction & GamingMath.Direction.Left))
            {
                aabb.Position.X = (MovedGridLeft + 1) * MapData.GridWidth;
                velocity.X = 0;
            }
            if (0 != (direction & GamingMath.Direction.Top))
            {
                aabb.Position.Y = (MovedGridTop + 1) * MapData.GridHeight;
                velocity.Y = 0;
            }
            if (0 != (direction & GamingMath.Direction.Right))
            {
                aabb.Position.X = MovedGridRight * MapData.GridWidth - aabb.Size.X;
                velocity.X = 0;
            }
            if (0 != (direction & GamingMath.Direction.Bottom))
            {
                aabb.Position.Y = MovedGridBottom * MapData.GridHeight - aabb.Size.Y;
                velocity.Y = 0;
            }

            return;
        }

        /// <summary>
        /// 箱と箱が接触しているか調べる
        /// </summary>
        /// <param name="Pos1">箱1の右上の位置</param>
        /// <param name="Size1">箱1の大きさ</param>
        /// <param name="Pos2">箱2の右上の位置</param>
        /// <param name="Size2">箱2の大きさ</param>
        /// <returns>接触</returns>
        public static bool AABBAABB(AABB AABB1, AABB AABB2)
        {
            return
                (AABB1.Position.X + AABB1.Size.X > AABB2.Position.X) &&
                (AABB1.Position.X < AABB2.Position.X + AABB2.Size.X) &&
                (AABB1.Position.Y + AABB1.Size.Y > AABB2.Position.Y) &&
                (AABB1.Position.Y < AABB2.Position.Y + AABB2.Size.Y);
        }
        /// <summary>
        /// 箱と箱の距離を計測する
        /// </summary>
        /// <param name="Pos1">箱1の右上の位置</param>
        /// <param name="Size1">箱1の大きさ</param>
        /// <param name="Pos2">箱2の右上の位置</param>
        /// <param name="Size2">箱2の大きさ</param>
        /// <returns>距離</returns>
        public static Vector2 DistanceAABBAABB(AABB AABB1, AABB AABB2)
        {
            Vector2 distance = new Vector2();

            // Y方向の距離
            if (AABB2.Position.Y > AABB1.Position.Y)
                distance.Y = AABB2.Position.Y - AABB1.Position.Y + AABB1.Size.Y; // 2の頂辺と1の底辺との距離
            else
                distance.Y = AABB2.Position.Y + AABB2.Size.Y - AABB1.Position.Y; // 2の底辺と1の頂辺との距離

            // X方向の距離
            if (AABB2.Position.X > AABB1.Position.X)
                distance.X = AABB2.Position.X - AABB1.Position.X + AABB1.Size.X; // 2の右辺と1の左辺との距離
            else
                distance.X = AABB2.Position.X + AABB2.Size.X - AABB1.Position.X; // 2の左辺と1の右辺との距離
            
            return distance;
        }

        /// <summary>
        /// 接触が判明している時左右が当たっているか判定する
        /// AABBAABB LeftRight
        /// </summary>
        /// <param name="AABB1">判定をする箱</param>
        /// <param name="AABB2">判定先の箱</param>
        /// <param name="Velocity1">移動量</param>
        /// <returns>左右成分の接触かどうか</returns>
        public static bool AABBAABB_LR(AABB AABB1, AABB AABB2, Vector2 Velocity1)
        {
            /* 
             * Velocityによって移動する前にX方向で衝突していなかったのに
             * 移動後は衝突しているというときにX方向の変位により衝突したと判定する
             */
            bool BeforeMove = AABBAABB(AABB1, AABB2); // 移動前に接触しているか
            if (!BeforeMove) // 移動前接触していない時
            {
                Velocity1.Y = 0; // 余分な要素を抜き
                AABB1.Position += Velocity1; // 移動させる
                return AABBAABB(AABB1, AABB2); // 移動後に接触しているか
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 接触が判明している時上下が当たっているか判定する
        /// AABBAABB TopBottom
        /// </summary>
        /// <param name="AABB1">判定をする箱</param>
        /// <param name="AABB2">判定先の箱</param>
        /// <param name="Velocity1">移動量</param>
        /// <returns>上下成分の接触かどうか</returns>
        public static bool AABBAABB_TB(AABB AABB1, AABB AABB2, Vector2 Velocity1)
        {
            bool BeforeMove = AABBAABB(AABB1, AABB2); // 移動前に接触しているか
            if (!BeforeMove) // 移動前接触していない時
            {
                Velocity1.X = 0; // 余分な要素を抜き
                AABB1.Position += Velocity1; // 移動させる 
                return AABBAABB(AABB1, AABB2); // 移動後に接触しているか
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 箱と箱が接触しているか調べる
        /// </summary>
        /// <param name="Pos1">箱1の右上の位置</param>
        /// <param name="Size1">箱1の大きさ</param>
        /// <param name="Pos2">箱2の右上の位置</param>
        /// <param name="Size2">箱2の大きさ</param>
        /// <returns>接触</returns>
        public static bool AABBAABB(Vector2 Pos1, Vector2 Size1, Vector2 Pos2, Vector2 Size2)
        {
            return
                (Pos1.X + Size1.X > Pos2.X) &&
                (Pos1.X < Pos2.X + Size2.X) &&
                (Pos1.Y + Size1.Y > Pos2.Y) &&
                (Pos1.Y < Pos2.Y + Size2.Y);
        }
        /// <summary>
        /// 箱と箱の距離を計測する
        /// </summary>
        /// <param name="Pos1">箱1の右上の位置</param>
        /// <param name="Size1">箱1の大きさ</param>
        /// <param name="Pos2">箱2の右上の位置</param>
        /// <param name="Size2">箱2の大きさ</param>
        /// <returns>距離</returns>
        public static Vector2 DistanceAABBAABB(Vector2 Pos1, Vector2 Size1, Vector2 Pos2, Vector2 Size2)
        {
            Vector2 distance = new Vector2();

            // Y方向の距離
            distance.Y = Pos2.Y - Pos1.Y + Size1.Y; // 2の頂辺と1の底辺との距離
            if (distance.Y < 0 && Math.Abs(distance.Y) > Size1.Y)
                distance.Y = Pos2.Y + Size2.Y - Pos1.Y; // 2の底辺と1の頂辺との距離

            // X方向の距離
            distance.X = Pos2.X - Pos1.X + Size1.X; // 2の右辺と1の左辺との距離
            if (distance.X < 0 && Math.Abs(distance.X) > Size1.X)
                distance.X = Pos2.X + Size2.X - Pos1.X; // 2の左辺と1の右辺との距離

            return distance;
        }
    }
}