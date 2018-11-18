using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    public static class FileWrite
    {
        // Creates a directory if it does not already exist, writes a file with the given data.
        public static void Write(string path, Data data)
        {
            if (!Directory.Exists(Program.Path + "/Stats/"))
            {
                Directory.CreateDirectory(Program.Path + "/Stats/");
            }

            File.WriteAllBytes(@path, data.ToBytes());
        }

        // Reads the file and returns a data struct.
        public static Data Read(string path)
        {
            return File.ReadAllBytes(path).ToObject<Data>();
        }
    }
}
