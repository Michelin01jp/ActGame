using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    class CharacterContoroller
    {
        /// <summary>
        /// キャラクターの制御リスト
        /// </summary>
        public static List<Character> Characters = new List<Character>();
        /// <summary>
        /// マップのあたり判定データ
        /// </summary>
        public static List<Collision.AABB> AABBs = new List<Collision.AABB>();

        public static void Step(int StepTime)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].DrawFlag)
                {
                    Characters[i].Step(StepTime);
                }
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].DrawFlag)
                {
                    Characters[i].Draw();
                }
            }
        }

        /// <summary>
        /// キャラクターリストに追加
        /// </summary>
        /// <param name="Item">追加するキャラクター</param>
        /// <param name="Coeff">反発係数</param>
        public static void Add(Character Item1,Collision.AABB Item2)
        {
            Characters.Add(Item1);
            AABBs.Add(Item2);

            return;
        }
    }
}
