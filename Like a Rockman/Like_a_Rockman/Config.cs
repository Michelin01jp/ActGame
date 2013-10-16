using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    public class Config
    {
        static string FileName = "Config.xml";

        public int Device;
        public int Up;
        public int Right;
        public int Down;
        public int Left;
        public int Jump;
        public int Squat;
        public int Attack1;
        public int Attack2;

        public Config()
        {
            if (!File.Exists(FileName))
            {
                Default();
                Save();
            }
        }

        public static Config Load()
        {
            if (File.Exists(FileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));

                using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    return (Config)serializer.Deserialize(fs);
                }
            }

            return new Config();
        }

        public void Save()
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));

            using (var fs = new System.IO.FileStream(FileName, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        public void Default()
        {
            Device = 0;
            Up = 45;
            Down = 46;
            Left = 47;
            Right = 48;
            Jump = 1;
            Squat = 3;
            Attack1 = 106;
            Attack2 = 108;
        }
    }
}
