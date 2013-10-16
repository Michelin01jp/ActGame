using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class AirBlock : MapObject
    {
        public AirBlock(int x, int y)
        {
            ID = ObjectID.None;
            Position = new Vector2(x, y);
            Size = new Vector2(MapData.GridWidth, MapData.GridHeight);
            DrawFlag = false;
        }
    }
}
