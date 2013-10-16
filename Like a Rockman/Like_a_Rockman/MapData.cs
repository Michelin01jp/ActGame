using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    static class MapData
    {
        /// <summary>
        /// 地形のリストデータ
        /// </summary>
        public static List<List<MapObject>> Map = new List<List<MapObject>>();

        public const int GridHeight = 32;
        public const int GridWidth = 32;

        /// <summary>
        /// グリッド上の座標を入れてその位置にオブジェクトが存在するかを判定する
        /// </summary>
        /// <param name="GridX">横方向</param>
        /// <param name="GridY">縦方向</param>
        /// <returns>存在しているか</returns>
        public static bool IsSolid(int GridX, int GridY)
        {
            // 定義域を守っていなかった場合
            if (GridX >= Map.Count || GridY >= Map[0].Count || GridX < 0 || GridY < 0)
                return false;

            return Map[GridX][GridY].ID != MapObject.ObjectID.None;
        }

        /// <summary>
        /// 地形情報の読み込み
        /// </summary>
        public static void Load()
        {
            var db = FileIO.TextLoad("Map.dat");

            for (int i = 0; i < db.Length; i++)
            {
                var line = db[i].Split(',');

                for (int j = 0; j < line.Length; j++)
                {
                    MapObject.ObjectID id = (MapObject.ObjectID)int.Parse(line[j]);

                    switch (id)
                    {
                        case MapObject.ObjectID.None:
                            Add(j, i, new AirBlock(j * 32, i * 32));
                            break;
                        case MapObject.ObjectID.SteelBlock:
                            Add(j, i, new SteelBlock(j * 32, i * 32));
                            break;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// 地形の描画
        /// </summary>
        public static void Draw()
        {
            for (int i = 0; i < Map.Count; i++)
            {
                for (int j = 0; j < Map[i].Count; j++)
                {
                    if (Map[i][j].DrawFlag)
                    {
                        Map[i][j].Draw();
                    }
                }
            }

            return;
        }

        public static void Add(int X, int Y, MapObject Item)
        {
            while (Map.Count <= X)
                Map.Add(new List<MapObject>());

            Map[X].Add(Item);
        }
    }
}
