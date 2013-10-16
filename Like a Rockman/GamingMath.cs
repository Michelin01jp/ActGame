using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Like_a_Rockman
{
    /// <summary>
    /// ゲームに高頻度で使う数学のクラス
    /// </summary>
    class GamingMath
    {
        public enum Direction
        {
            Left = 0x1,
            Right = 0x2,
            Top = 0x4,
            Bottom = 0x8,
            TB = Top | Bottom,
            LR = Left | Right,
            TBLR = TB | LR
        }

        public static Vector2 ToPercentPosition(Vector2 position, Vector2 windowsize, Camera camera)
        {
            Vector2 Center = new Vector2(windowsize.X / 2, windowsize.Y / 2); // 中心座標
            Vector2 Relative = position - Center - camera.Panning; // 相対座標

            return new Vector2((Relative.X / Center.X) * camera.Scale, (-Relative.Y / Center.Y) * camera.Scale);
        }

        public static Vector2 ToPercentPosition(Vector2 position, Vector2 windowsize)
        {
            Vector2 Center = new Vector2(windowsize.X / 2, windowsize.Y / 2); // 中心座標
            Vector2 Relative = position - Center; // 相対座標

            return new Vector2(Relative.X / Center.X, -Relative.Y / Center.Y);
        }
    }
}