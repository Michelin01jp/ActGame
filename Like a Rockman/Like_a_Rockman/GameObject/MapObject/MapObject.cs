using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Like_a_Rockman
{
    /// <summary>
    /// マップ上のオブジェクト
    /// </summary>
    class MapObject
    {
        public enum ObjectID
        { 
            None = 0,
            SteelBlock = 1
        }

        protected static MyWindow Window;
        protected static Camera Camera;

        /// <summary>
        /// オブジェクトのID
        /// </summary>
        public ObjectID ID;
        /// <summary>
        /// オブジェクトの位置
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// オブジェクトの大きさ
        /// </summary>
        public Vector2 Size;
        /// <summary>
        /// 描画するか
        /// </summary>
        public bool DrawFlag;

        public static void Init(MyWindow window,Camera camera)
        {
            Window = window;
            Camera = camera;

            return;
        }

        public virtual void Draw()
        {
            return;
        }
    }
}