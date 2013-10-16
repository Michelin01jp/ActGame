using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Like_a_Rockman
{
    class SteelBlock : MapObject
    {
        public SteelBlock(int x, int y)
        {
            ID = ObjectID.SteelBlock;
            Position = new Vector2(x, y);
            Size = new Vector2(MapData.GridWidth, MapData.GridHeight);
            DrawFlag = true;
        }

        public override void Draw()
        {
            Vector2 WindowSize = new Vector2(Window.Width, Window.Height);

            Like_a_Rockman.Draw.Rectangle(
                GamingMath.ToPercentPosition(Position, WindowSize, Camera), 
                GamingMath.ToPercentPosition(Position + Size, WindowSize, Camera), 
                Color.White);
        }
    }
}
