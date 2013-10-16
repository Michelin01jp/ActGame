using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;

namespace Like_a_Rockman
{
    /// <summary>
    /// 描画クラス
    /// </summary>
    class Draw
    {
        /// <summary>
        /// 背景の塗りつぶし
        /// </summary>
        /// <param name="color">塗りつぶす色</param>
        public static void Screen(Color color)
        {
            GL.ClearColor(color); // 背景色を指定
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // カラーマスクバッファを初期化        
        }

        /// <summary>
        /// 三角形の描画
        /// </summary>
        /// <param name="pos1">頂点1</param>
        /// <param name="pos2">頂点2</param>
        /// <param name="pos3">頂点3</param>
        /// <param name="color">色</param>
        public static void Triangle(Vector2 pos1, Vector2 pos2, Vector2 pos3, Color color)
        {
            GL.Begin(BeginMode.Triangles);

            // 色の指定
            GL.Color3(color);

            // 頂点の指定
            GL.Vertex2(pos1);
            GL.Vertex2(pos2);
            GL.Vertex2(pos3);

            GL.End();
        }

        /// <summary>
        /// 四角形の描画
        /// </summary>
        /// <param name="pos1">頂点1</param>
        /// <param name="pos2">頂点2</param>
        /// <param name="pos3">頂点3</param>
        /// <param name="pos4">頂点4</param>
        /// <param name="color">色</param>
        public static void Quads(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4, Color color)
        {
            GL.Begin(BeginMode.Quads);

            // 色の指定
            GL.Color3(color);

            // 頂点の指定
            GL.Vertex2(pos1);
            GL.Vertex2(pos2);
            GL.Vertex2(pos3);
            GL.Vertex2(pos4);

            GL.End();
        }

        /// <summary>
        /// 矩形の描画
        /// </summary>
        /// <param name="pos1">始点</param>
        /// <param name="pos2">終点</param>
        /// <param name="color">色</param>
        public static void Rectangle(Vector2 pos1, Vector2 pos2, Color color)
        {
            GL.Begin(BeginMode.Quads);

            // 色の指定
            GL.Color3(color);

            // 頂点の指定
            GL.Vertex2(pos1);
            GL.Vertex2(new Vector2(pos1.X, pos2.Y));
            GL.Vertex2(pos2);
            GL.Vertex2(new Vector2(pos2.X, pos1.Y));

            GL.End();
        }

        public static void Round(Vector2 pos, int r, Color color)
        {
            int polygonnum = 8 + r - 5;
            if (polygonnum > 16)
                polygonnum = 16;

            GL.Begin(BeginMode.Quads);

            // 色の指定
            GL.Color3(color);

            // 頂点の指定
            GL.Vertex2(pos);

            for (int i = 0; i < polygonnum; i++)
            {
                
            }

            GL.End();
        }
    }
}