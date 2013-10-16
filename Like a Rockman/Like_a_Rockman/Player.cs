using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using OpenTK;

namespace Like_a_Rockman
{
    class Player : Character
    {
        /// <summary>
        /// 入力データ
        /// </summary>
        private Input Input;
        /// <summary>
        /// 移動速度の加速度
        /// </summary>
        private float Spd = 0;
        /// <summary>
        /// 移動速度の限界
        /// </summary>
        private float SpdCapacity = 3;
        private float Jump;
        private int JumpCount;

        public Player(Vector2 pos, Input input)
            :base (pos)
        {
            Input = input;
            Spd = 0.25f; // 移動の加速度
            Jump = 5f; // ジャンプの初速
            JumpCount = 0; // ジャンプ回数
        }

        /// <summary>
        /// １フレーム分の処理
        /// </summary>
        /// <param name="Time">１フレームの時間</param>
        public override void Step(int Time)
        {
            Vector2 addAcceleration = new Vector2(); // 加算する加速度

            #region プレイヤーによる操作

            Console.WriteLine(JumpCount);

            if (Input.GetKeyState(Input.Control.Right)) // 右に移動するとき
            {
                addAcceleration.X = 1.0f * Spd;
            }
            else if (Input.GetKeyState(Input.Control.Left)) // 左に移動するとき
            {
                addAcceleration.X = -1.0f * Spd;
            }

            if (Input.GetKeyState(Input.Control.Jump) && !GetState(State.Jump) && JumpCount < 2) // ジャンプ
            {
                addAcceleration.Y = -Jump;
                PhysicalObject.Velocity.Y = 0;
                SetState(State.Jump);
            }
            
            if(addAcceleration.X == 0) // 移動しない時
            {

                if (GetState(State.Stand)) // 立っている時
                {
                    // スライディングをして急停止する条件
                    if (Input.GetKeyState(Input.Control.Down))
                    {
                        if (Math.Abs(PhysicalObject.Velocity.X) < Spd) // 減速すると反対方向に進行してしまう場合
                        {
                            PhysicalObject.Velocity.X = 0; // 0にする
                        }
                        else
                        {
                            if (PhysicalObject.Velocity.X > 0) // 右方向に進行している時
                                PhysicalObject.Velocity.X -= Spd; // 速度分減算する
                            else // 左方向に進行している時
                                PhysicalObject.Velocity.X += Spd;
                        }
                    }
                    else if (PhysicalObject.Velocity.X != 0) // 急停止しない場合の減速
                    {
                        float brake = Spd / 2; // 減速比を半分にする

                        if (Math.Abs(PhysicalObject.Velocity.X) < brake) // 減速すると反対方向に進行してしまう場合
                        {
                            PhysicalObject.Velocity.X = 0; // 0にする
                        }
                        else
                        {
                            if (PhysicalObject.Velocity.X > 0) // 右方向に進行している時
                                PhysicalObject.Velocity.X -= brake; // 速度分減算する
                            else // 左方向に進行している時
                                PhysicalObject.Velocity.X += brake;
                        }
                    }
                }
            }
            #endregion

            if (PhysicalObject.Velocity.Y > 0)
                SetState(State.Fall);
            if (PhysicalObject.Velocity.Y == 0)
                SetState(State.Stand);

            // 加速度に加算する条件
            // Velocityの絶対値が一定範囲内　もしくは　加速の向きが移動方向の反対
            // 一定範囲内のみプレイヤーの操作を受け付け　もし外部からの力によりそれ以上に加速した場合でも減速することならできる
            if (Math.Abs(PhysicalObject.Velocity.X) < SpdCapacity || addAcceleration.X * PhysicalObject.Velocity.X < 0)
                PhysicalObject.Acceleration += addAcceleration; // 加速度を追加
            
            base.Step(Time);
            
            //Console.WriteLine("{0}, {1}",PhysicalObject.Velocity.X,PhysicalObject.Velocity.Y);
            //Console.WriteLine("State:{0}", state);

            return;
        }

        /// <summary>
        /// 描画
        /// </summary>
        public override void Draw()
        {
            Vector2 WindowSize = new Vector2(Window.Width, Window.Height); // 画面サイズを２次元ベクトルにしておく

            Like_a_Rockman.Draw.Rectangle(
                GamingMath.ToPercentPosition(PhysicalObject.Position, WindowSize, Camera),
                GamingMath.ToPercentPosition(PhysicalObject.Position + PhysicalObject.Size, WindowSize, Camera),
                Color.Red); // プレイヤーの位置は赤い正方形で示す(仮)

            return;
        }

        /// <summary>
        /// プレイヤーの状態を取得する
        /// </summary>
        /// <param name="s">取得する状態</param>
        /// <returns></returns>
        public bool GetState(State s)
        {
            return 0 != (state & s);
        }

        /// <summary>
        /// プレイヤーの状態の指定する
        /// </summary>
        /// <param name="s">指定する状態</param>
        public void SetState(State s)
        {
            if (!GetState(s))
            {
                switch (s) // それぞれの状態の固有処理
                {
                    case State.Jump: // ジャンプ
                        DeleteState(State.Fall); // 落下中でなくし
                        DeleteState(State.Stand); // 立っていない状態とする
                        JumpCount++; // ジャンプ回数を増やす
                        break;
                    case State.Fall: // 落下
                        DeleteState(State.Stand); // 立ってない状態とする

                        if (GetState(State.Jump)) // もしジャンプ時だったら
                            DeleteState(State.Jump); // ジャンプを消し
                        else // そうでなかったら
                            JumpCount++; // ジャンプ回数を増やす

                        break;
                    case State.Stand: // 立つ
                        DeleteState(State.Jump); // ジャンプを消し
                        DeleteState(State.Fall); // 落下中でもなくす
                        JumpCount = 0; // ジャンプ回数を初期化する
                        break;
                }

                state |= s;
            }

            return;
        }

        /// <summary>
        /// プレイヤーの状態を削除する
        /// </summary>
        /// <param name="s">削除する状態</param>
        public void DeleteState(State s)
        {
            state &= ~s;

            return;
        }
    }
}