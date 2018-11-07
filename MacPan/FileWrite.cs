using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    class FileWrite
    {
        public void Write(string path, Data data)
        {
            if (!Directory.Exists(@path + @"/Stats/"))
            {
                Directory.CreateDirectory(@path + @"/Stats/");
            }

            File.WriteAllBytes(@path, data.ToBytes());
        }

        public Data Read(string path)
        {
            return File.ReadAllBytes(path).ToObject<Data>();
        }
    }
}
