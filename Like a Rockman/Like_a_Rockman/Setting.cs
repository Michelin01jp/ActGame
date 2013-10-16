using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Like_a_Rockman
{
    public class Setting
    {
        static string FileName = "Setting.xml";
        
        public int Height;
        public int Width;
        public float Scale;
        public bool FullScreen;

        public Setting()
        {
            if (!File.Exists(FileName))
            {
                Default();
                Save();
            }
        }

        public static Setting Load()
        {
            if (File.Exists(FileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Setting));

                using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    return (Setting)serializer.Deserialize(fs);
                }
            }

            return new Setting();
        }

        public void Save()
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Setting));

            using (var fs = new FileStream(FileName, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        public void Default()
        {
            Height = 360;
            Width = 640;
            Scale = 1.0f;
            FullScreen = false;
        }
    }
}
