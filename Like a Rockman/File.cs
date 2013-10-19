using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Like_a_Rockman
{
    class FileIO
    {
        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="FileName">読み込むファイル名</param>
        /// <returns>ファイルの行ごとの内容</returns>
        public static List<String> TextLoad(string FileName)
        {
            //防御的if文
            //条件式を反転させて、例外処理のif文にした方が見やすい
            if (!File.Exists(FileName))
            {
                //「ファイルが見つからない」というのは本来ならあり得ない状態なので、例外を投げて処理を止めた方が良いかも？
                //例外を返さずに空のコレクションを返してもいいが、その場合は発生した例外を何も処理せずにスルーしてるだけなのでよろしくはない
                throw new FileNotFoundException("ファイルが見つかりません", FileName);


            }

            var list = new List<String>();

            using (var read = new StreamReader(FileName, Encoding.GetEncoding("Shift_JIS")))
            {
                string line;

                while ((line = read.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }

    

        public static bool TextOutput(string FileName, string Content)
        {
            using (var str = new StreamWriter(FileName, false, Encoding.GetEncoding("Shift_JIS")))
            {
                str.WriteLine(Content);
                return true;
            }
        }
    }
}
