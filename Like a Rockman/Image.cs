using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Like_a_Rockman
{
    /// <summary>
    /// 画像制御クラス
    /// </summary>
    class Image : IDisposable
    {
        private int tex;
        private Bitmap bitmap;

        /// <summary>
        /// 画像ファイルのロード
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>画像呼出のための数値</returns>
        public Image(string fileName)
        {
            if (File.Exists(fileName))
            {
                bitmap = new Bitmap(fileName);
                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.GenTextures(1, out tex);
                GL.BindTexture(TextureTarget.Texture2D, tex);

                GL.TexImage2D(
                    TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb,
                    data.Height, data.Width, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
                
                bitmap.UnlockBits(data);

                return;
            }
            else
            {
                Console.WriteLine("Imagedata is Not Exists.:{0}", fileName);
            }
        }

        /// <summary>
        /// テクスチャの削除
        /// </summary>
        /// <param name="Tex">削除するテクスチャ</param>
        public void Dispose()
        {
            GL.DeleteTexture(Tex);
            bitmap.Dispose();
        }

        public int Tex
        {
            get { return tex; }
        }
    }
}
