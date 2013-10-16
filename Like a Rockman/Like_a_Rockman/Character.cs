using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class Character
    {
        public enum State
        { 
            None = 0,
            Stand = 0x1,
            Squat = 0x2,
            Jump = 0x4,
            Fall = 0x8,
            Sliding = 0x10
        }

        public Physics.PhysicalObject PhysicalObject;
        public Collision.AABB AABB;
        public State state;
        protected static MyWindow Window;
        protected static Camera Camera;
        public bool DrawFlag;

        public Character(Vector2 pos)
        {
            PhysicalObject = new Physics.PhysicalObject();
            PhysicalObject.Position = pos;
            PhysicalObject.Size = new Vector2(24, 48);
            DrawFlag = true;
            state = State.None;

            AABB = new Collision.AABB(PhysicalObject.Position, PhysicalObject.Size, 0, Collision.Type.Character);

            CharacterContoroller.Add(this, AABB); // 制御リストに自分を入れる
        }

        public virtual void Step(int Time)
        {
            Physics.Step(Time, ref PhysicalObject, ref AABB);
        }

        public static void Init(MyWindow window, Camera camera)
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
