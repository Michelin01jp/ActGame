using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class Camera
    {
        private Vector2 panning;
        private float scale = 1.0f;

        public Vector2 Panning
        {
            get { return panning; }
            set { panning = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
    }
}